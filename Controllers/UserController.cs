using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizplusApi.Models;
using QuizplusApi.Models.Others;
using QuizplusApi.Models.User;
using QuizplusApi.Services;
using QuizplusApi.ViewModels.Helper;
using QuizplusApi.ViewModels.Payment;
using QuizplusApi.ViewModels.User;

namespace QuizplusApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController:ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;
        private readonly ISqlRepository<Users> _userRepo;
        private readonly ISqlRepository<UserRole> _userRoleRepo;
        private readonly ISqlRepository<LogHistory> _logHistoryRepo;

        public UserController(IConfiguration config,
                                AppDbContext context, 
                                ISqlRepository<Users> userRepo,
                                ISqlRepository<UserRole> userRoleRepo,
                                ISqlRepository<LogHistory> logHistoryRepo)
        {
            _config=config;
            _context = context;
            _userRepo = userRepo;
            _userRoleRepo=userRoleRepo;
            _logHistoryRepo=logHistoryRepo;
        }

        ///<summary>
        ///Get Log in Detail
        ///</summary>
        [AllowAnonymous]
        [HttpGet("{email}/{password}")]      
        public ActionResult GetLoginInfo(string email, string password)
        {
            try
            {
                var user=(from u in _context.Users join r in _context.UserRoles on u.UserRoleId
                equals r.UserRoleId 
                join b in _context.BillingPayments on u.PaymentId equals b.BillingPaymentId into ps 
                from b in ps.DefaultIfEmpty()
                where u.IsActive.Equals(true) && u.Email.Equals(email) && u.Password.Equals(password)
                select new {u.UserId,r.UserRoleId,r.RoleName,r.DisplayName,u.FullName,u.Mobile,u.Email,
                u.ImagePath,u.Password,u.Address,u.DateOfBirth,u.AddedBy,u.PaymentId,
                Plan=(b==null?"n/a":b.Title),
                ExpiryDate=(b==null?"n/a":b.EndDate.ToString("yyyy/M/dd",CultureInfo.InvariantCulture)),
                IsExpired=(b==null?"n/a":(DateTime.UtcNow.Date>b.EndDate?"true":"false"))}).FirstOrDefault();
                if(user!=null)
                {
                    UserInfo userInfo=new UserInfo{UserId=user.UserId,UserRoleId=user.UserRoleId,RoleName=user.RoleName,DisplayName=user.DisplayName,
                    Email=user.Email,Password=user.Password,FullName=user.FullName,Mobile=user.Mobile,Address=user.Address,
                    ImagePath=user.ImagePath,DateOfBirth=user.DateOfBirth,AddedBy=user.AddedBy,PaymentId=user.PaymentId,
                    PlanName=user.Plan,PlanExpiryDate=user.ExpiryDate,IsPlanExpired=user.IsExpired};
                    var token=GenerateJwtToken(userInfo); 
                    return Ok(new LogInResponse{Token=token,Obj=userInfo});
                }
                return NoContent();              
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation{Status="error",ResponseMsg=ex.Message});           
            }
        }

        ///<summary>
        ///Get User Info for Forget password option
        ///</summary>
        [AllowAnonymous]
        [HttpGet("{email}")]      
        public ActionResult GetUserInfoForForgetPassword(string email)
        {
            try
            {
                var user=_context.Users.SingleOrDefault(q=>q.Email==email);
                if(user!=null)
                {
                    return Ok(user);
                }
                else
                {
                    return Accepted(new Confirmation{Status="error",ResponseMsg="thereisnouserforthisemail"});
                }
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation{Status="error",ResponseMsg=ex.Message});           
            }
        }

        ///<summary>
        ///Admin Registration
        ///</summary>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult AdminRegistration(Users model)
        {
            try
            {
                bool duplicateRegistration=false;
                if(model.StripeSessionId!="")
                {
                    var chkDuplicateSessionId=_context.Users.SingleOrDefault(p=>p.StripeSessionId==model.StripeSessionId);
                    if(chkDuplicateSessionId!=null)
                    {
                        duplicateRegistration=true;
                    }
                }
                
                var chkDuplicate=_context.Users.SingleOrDefault(p=>p.Email==model.Email);
                if(duplicateRegistration)
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "pleasepayfirstthisisaninvalidsession" });
                }
                else if(chkDuplicate==null)
                {
                    using var transaction = _context.Database.BeginTransaction();

                    model.UserRoleId=1;
                    model.AddedBy=1;
                    model.DateAdded=DateTime.Now;
                    model.IsActive=true;
                    if(model.StripeSessionId!="")
                    {
                        model.PaymentMode="Online";
                        model.TransactionDetail=model.StripeSessionId;
                    }
                    else
                    {
                        model.PaymentMode="N/A";
                    }
                    var obj=_userRepo.Insert(model);

                    if(model.StripeSessionId!="")
                    {
                        var objBilling=_context.BillingPayments.SingleOrDefault(p=>p.StripeSessionId==model.StripeSessionId);
                        objBilling.UserEmail=model.Email;
                        objBilling.AddedBy=model.UserId;
                        _context.SaveChanges();

                        var objUpdatePayment=_context.Users.FirstOrDefault(q=>q.UserId==obj.UserId);
                        objUpdatePayment.PaymentId=objBilling.BillingPaymentId;
                        _context.SaveChanges();
                    }
                    else
                    {
                        BillRegister objBillRegister=new BillRegister();
                        objBillRegister.TransactionEmail=model.Email;
                        objBillRegister.AddedBy=obj.UserId;
                        objBillRegister.TransactionDetail=model.TransactionDetail;
                        objBillRegister.StripeSessionId="";
                        objBillRegister.PaymentMode=model.PaymentMode;
                        objBillRegister.Price=_context.BillingPlans.FirstOrDefault(q=>q.BillingPlanId==model.BillingPlanId).Price;

                        PaymentService objPaymentService=new PaymentService(_context);
                        int paymentId=objPaymentService.InsertBillingPayment(objBillRegister);
                        if(paymentId>0)
                        {
                            var objUpdatePayment=_context.Users.FirstOrDefault(q=>q.UserId==obj.UserId);
                            objUpdatePayment.PaymentId=paymentId;
                            _context.SaveChanges();
                        }
                    }

                    SiteSettings objSettings=new SiteSettings();
                    objSettings.Port=587;
                    objSettings.Host="smtp.gmail.com";
                    objSettings.Currency="usd";
                    objSettings.CurrencySymbol="$";
                    objSettings.IsActive=true;
                    objSettings.DateAdded=DateTime.Now;
                    objSettings.IsMigrationData=false; 
                    objSettings.AddedBy=obj.UserId;
                    _context.SiteSettings.Add(objSettings);
                    _context.SaveChanges();

                    transaction.Commit();
                    return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullySaved" });
                }
                else
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "thisemailalreadyhaveauser" });
                }
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Student Registration
        ///</summary>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult StudentRegistration(Users model)
        {
            try
            {               
                var chkDuplicate=_context.Users.SingleOrDefault(p=>p.Email==model.Email);
                var chkParticipant=_context.QuizParticipants.FirstOrDefault(p=>p.Email==model.Email && p.AddedBy==model.AddedBy);
                if(chkParticipant==null)
                {
                    return Accepted(new Confirmation { Status = "error", ResponseMsg = "thisemailnotallowedtoregister" });
                }
                else if(chkDuplicate==null)
                {
                    model.UserRoleId=2;
                    model.DateAdded=DateTime.Now;
                    model.IsActive=true;
                    _userRepo.Insert(model);
                    return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullySaved" });
                }
                else
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "thisemailalreadyhaveauser" });
                }
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Create History Log
        ///</summary>
        [AllowAnonymous]
        [HttpPost]       
        public ActionResult CreateLoginHistory(LogHistory model)
        {
            try
            {  
                model.LogDate=DateTime.Now;    
                model.LogInTime=DateTime.Now;
                model.LogCode=Guid.NewGuid().ToString();   
                model.AdminId=_context.Users.SingleOrDefault(q=>q.UserId==model.UserId).AddedBy;     
                _logHistoryRepo.Insert(model);
                return Ok(new Confirmation { Status = "success", ResponseMsg = model.LogCode });              
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Update Login History
        ///</summary>
        [AllowAnonymous]
        [HttpGet("{logCode}")]       
        public ActionResult UpdateLoginHistory(string logCode)
        {
            try
            {
                var objLogHistory=_context.LogHistories.SingleOrDefault(opt=>opt.LogCode==logCode);
                objLogHistory.LogOutTime=DateTime.Now;
                _context.SaveChanges();
                return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullySaved" });
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Get Browsing List
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin")]    
        [HttpGet]        
        public ActionResult GetBrowseList()
        {
            try
            {
                var browsingList=_context.LogHistories.Join(_context.Users,
                log=>log.UserId,
                user=>user.UserId,
                (log,user)=>new{    
                    FullName=user.FullName,
                    Email=user.Email,
                    LogInTime=log.LogInTime,
                    LogOutTime=log.LogOutTime,
                    Ip=log.Ip,
                    Browser=log.Browser,
                    BrowserVersion=log.BrowserVersion,
                    Platform=log.Platform
                }).OrderByDescending(e=>e.LogInTime);
                return Ok(browsingList);
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Get User List
        ///</summary>
        [HttpGet("{userId}")]
        [Authorize(Roles="Admin,SuperAdmin")]
        public ActionResult GetUserList(int userId)
        {
            try
            {
                var objRole=_context.Users.SingleOrDefault(q=>q.UserId==userId);
                if(objRole.UserRoleId==3)
                {
                    var userList=(from u in _context.Users join r in _context.UserRoles on 
                    u.UserRoleId equals r.UserRoleId join p in _context.BillingPlans on u.BillingPlanId equals p.BillingPlanId
                    join b in _context.BillingPayments on u.PaymentId equals b.BillingPaymentId into ps 
                    from b in ps.DefaultIfEmpty()
                    where u.AddedBy.Equals(userId) && u.UserId!=userId orderby u.DateAdded descending
                    select new {u.UserId,u.UserRoleId,u.FullName,r.RoleName,r.DisplayName,u.Mobile,u.Email,u.Password,u.DateOfBirth,
                    u.Address,u.ImagePath,u.PaymentMode,u.TransactionDetail,u.IsActive,p.BillingPlanId,p.Title,p.Interval,u.PaymentId,
                    ExpireyDate=(b==null?"n/a":b.EndDate.ToString("yyyy/M/dd",CultureInfo.InvariantCulture))});
                    return Ok(userList);
                }
                else
                {
                    var userList=(from u in _context.Users join r in _context.UserRoles on 
                    u.UserRoleId equals r.UserRoleId where u.AddedBy.Equals(userId) && u.UserId!=userId orderby u.DateAdded descending
                    select new {u.UserId,u.UserRoleId,u.FullName,r.RoleName,r.DisplayName,u.Mobile,u.Email,u.Password,u.DateOfBirth,
                    u.Address,u.ImagePath,u.AddedBy, u.PositionCode, u.PositionName, u.JobCode, u.JobName, u.DepartmentCode, u.DepartmentName, u.RegionCode, u.RegionName, u.IsActive});
                    return Ok(userList);
                }
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Get Single User by id
        ///</summary>
        [HttpGet("{id}")]
        [Authorize(Roles="Admin,SuperAdmin,Student")]
        public ActionResult GetSingleUser(int id)
        {
            try
            {
                var singleUser=_userRepo.SelectById(id);
                return Ok(singleUser);
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Delete User by User Id
        ///</summary>
        [HttpDelete("{id}")]
        [Authorize(Roles="Admin,SuperAdmin")]
        public ActionResult DeleteSingleUser(int id)
        {
            try
            {
                var chkMigration=_context.Users.FirstOrDefault(q=>q.UserId==id);
                if(chkMigration.IsMigrationData)
                {
                    return Accepted(new Confirmation { Status = "error", ResponseMsg = "notallowedtodeletethisondemoversion" });
                }
                else
                {
                    _userRepo.Delete(id);
                    return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullyDeleted" });
                }
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Create User
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin")]
        [HttpPost]
        public ActionResult CreateUser(Users model)
        {
            try
            {
                var chkDuplicate=_context.Users.SingleOrDefault(p=>p.Email.ToLower()==model.Email.ToLower());
                if(chkDuplicate==null)
                {  
                    using var transaction = _context.Database.BeginTransaction();

                    model.DateAdded=DateTime.Now;
                    model.IsActive=true;
                    var obj=_userRepo.Insert(model);

                    if(model.UserRoleId==1)
                    {
                        BillRegister objBillRegister=new BillRegister();
                        objBillRegister.TransactionEmail=model.Email;
                        objBillRegister.AddedBy=obj.UserId;
                        objBillRegister.TransactionDetail=model.TransactionDetail;
                        objBillRegister.StripeSessionId="";
                        objBillRegister.PaymentMode=model.PaymentMode;
                        objBillRegister.Price=_context.BillingPlans.FirstOrDefault(q=>q.BillingPlanId==model.BillingPlanId).Price;
                        
                        PaymentService objPaymentService=new PaymentService(_context);
                        int paymentId=objPaymentService.InsertBillingPayment(objBillRegister);
                        if(paymentId>0)
                        {
                            var objUpdatePayment=_context.Users.FirstOrDefault(q=>q.UserId==obj.UserId);
                            objUpdatePayment.PaymentId=paymentId;
                            _context.SaveChanges();
                        }                 

                        SiteSettings objSettings=new SiteSettings();
                        objSettings.Port=587;
                        objSettings.Host="smtp.gmail.com";
                        objSettings.Currency="usd";
                        objSettings.CurrencySymbol="$";
                        objSettings.IsActive=true;
                        objSettings.DateAdded=DateTime.Now;
                        objSettings.IsMigrationData=false; 
                        objSettings.AddedBy=obj.UserId;
                        _context.SiteSettings.Add(objSettings);
                        _context.SaveChanges();
                    }

                    transaction.Commit();
                    return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullySaved" });
                }
                else
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "thisemailalreadyhaveauser" });
                }
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Update User
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin")]
        [HttpPut]
        public ActionResult UpdateUser(Users model)
        {
            try
            {
                var objUser=_context.Users.SingleOrDefault(p=>p.UserId==model.UserId);
                var objCheck=_context.Users.SingleOrDefault(p=>p.Email.ToLower()==model.Email.ToLower());
                if(objCheck!=null && objCheck.Email.ToLower()!=objUser.Email.ToLower())
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "thisemailalreadyhaveauser" });
                }
                else
                {
                    objUser.UserRoleId=model.UserRoleId;
                    objUser.FullName=model.FullName;
                    objUser.Mobile=model.Mobile;
                    objUser.Email=model.Email;
                    objUser.DateOfBirth=model.DateOfBirth;
                    objUser.Address=model.Address;
                    objUser.Password=model.Password;
                    objUser.ImagePath=model.ImagePath;
                    objUser.LastUpdatedBy=model.LastUpdatedBy;
                    objUser.LastUpdatedDate=DateTime.Now;
                    objUser.IsActive = model.IsActive;
                    objUser.DepartmentCode=model.DepartmentCode;
                    objUser.DepartmentName=model.DepartmentName;
                    objUser.PositionCode=model.PositionCode;
                    objUser.PositionName=model.PositionName;
                    objUser.JobCode=model.JobCode;
                    objUser.JobName = model.JobName;
                    objUser.RegionCode=model.RegionCode;
                    objUser.RegionName = model.RegionName;
                    _context.SaveChanges();
                    return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullyUpdated" });
                }
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Update User Profile
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin,Student")]
        [HttpPut]       
        public ActionResult UpdateUserProfile(UserInfo model)
        {
            try
            {
                var objUser=_context.Users.SingleOrDefault(opt=>opt.UserId==model.UserId);
                var objCheck=_context.Users.SingleOrDefault(p=>p.Email.ToLower()==model.Email.ToLower());
                if(objCheck!=null && objCheck.Email.ToLower()!=objUser.Email.ToLower())
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "thisemailalreadyhaveauser" });
                }
                else
                {
                    objUser.FullName=model.FullName;
                    objUser.Email=model.Email;
                    objUser.Mobile=model.Mobile;
                    objUser.Address=model.Address;
                    objUser.DateOfBirth=model.DateOfBirth;
                    objUser.ImagePath=model.ImagePath;
                    objUser.LastUpdatedBy=model.LastUpdatedBy;
                    objUser.LastUpdatedDate=DateTime.Now;
                    _context.SaveChanges();
                    return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullyUpdated" });
                }                                       
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Change User Password
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin,Student")]
        [HttpPut]       
        public ActionResult ChangeUserPassword(UserInfo model)
        {
            try
            {
                var objUser=_context.Users.SingleOrDefault(opt=>opt.UserId==model.UserId);
                objUser.Password=model.Password;           
                _context.SaveChanges();
                return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullyUpdated" });                          
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Profile picture upload
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin,Student")]   
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "ProfileImages");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0 && file.ContentType.StartsWith("image/"))
                {
                    var fileName = Guid.NewGuid().ToString()+"_"+ ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return Accepted(new Confirmation { Status = "error", ResponseMsg = "notanimage" });
                }
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });               
            }
        }

        ///<summary>
        ///Get Role List
        ///</summary>
        [Authorize(Roles="SuperAdmin,Admin")]     
        [HttpGet]        
        public ActionResult GetUserRoleList()
        {
            try
            {
                var userRoleList=_userRoleRepo.SelectAll();
                return Ok(userRoleList);
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Delete Role by id
        ///</summary>
        [HttpDelete("{id}")]
        [Authorize(Roles="SuperAdmin")]
        public ActionResult DeleteSingleRole(int id)
        {
            try
            {
                if(id==1 || id==2)
                {
                    return Accepted(new Confirmation { Status = "restricted", ResponseMsg = "rolerestricted" });
                }
                else
                {
                    _userRoleRepo.Delete(id);
                    return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullyDeleted" });
                }          
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Create User Role
        ///</summary>
        [Authorize(Roles="SuperAdmin")]
        [HttpPost]       
        public ActionResult CreateUserRole(UserRole model)
        {
            try
            {                  
                var objCheck=_context.UserRoles.SingleOrDefault(opt=>opt.RoleName.ToLower()==model.RoleName.ToLower());
                if(objCheck==null)
                {
                    model.DateAdded=DateTime.Now;
                    model.IsActive=true;
                    _userRoleRepo.Insert(model);
                    return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullySaved" });                  
                }
                else
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "duplicateRolename" });
                }                    
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Update User Role
        ///</summary>
        [Authorize(Roles="SuperAdmin")]
        [HttpPut]       
        public ActionResult UpdateUserRole(UserRole model)
        {
            try
            {
                var objUserRole=_context.UserRoles.SingleOrDefault(opt=>opt.UserRoleId==model.UserRoleId);
                var objCheck=_context.UserRoles.SingleOrDefault(opt=>opt.RoleName.ToLower()==model.RoleName.ToLower());

                if(objCheck!=null && objCheck.RoleName.ToLower()!=objUserRole.RoleName.ToLower())
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "duplicateRolename" });
                }
                else if(model.UserRoleId==1 || model.UserRoleId==2)
                {
                    objUserRole.DisplayName=model.DisplayName;
                    objUserRole.RoleDesc=model.RoleDesc;
                    objUserRole.LastUpdatedBy=model.LastUpdatedBy;
                    objUserRole.LastUpdatedDate=DateTime.Now;
                    _context.SaveChanges();
                    return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullyUpdated" });
                }
                else
                {
                    objUserRole.RoleName=model.RoleName;
                    objUserRole.DisplayName=model.DisplayName;
                    objUserRole.RoleDesc=model.RoleDesc;
                    objUserRole.LastUpdatedBy=model.LastUpdatedBy;
                    objUserRole.LastUpdatedDate=DateTime.Now;
                    _context.SaveChanges();
                    return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullyUpdated" });
                }                                             
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        string GenerateJwtToken(UserInfo userInfo)
        {
            var securityKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserId.ToString()),
                new Claim("fullName", userInfo.FullName.ToString()),
                new Claim("role",userInfo.RoleName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(360),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}