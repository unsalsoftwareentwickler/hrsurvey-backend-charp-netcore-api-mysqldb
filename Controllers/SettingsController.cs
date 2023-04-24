using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizplusApi.Models;
using QuizplusApi.Models.Others;
using QuizplusApi.Models.Quiz;
using QuizplusApi.Services;
using QuizplusApi.ViewModels.Email;
using QuizplusApi.ViewModels.Helper;
using QuizplusApi.ViewModels.Payment;
using QuizplusApi.ViewModels.User;

namespace QuizplusApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SettingsController:ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ISqlRepository<Faq> _faqRepo;
        private readonly ISqlRepository<Instruction> _instructionRepo;
        private readonly ISqlRepository<Contacts> _contactRepo;
        private readonly ISqlRepository<BillingPlan> _planRepo;
        private readonly ISqlRepository<BillingPayment> _billingRepo;
        private readonly IMailService _mailService;

        public SettingsController(AppDbContext context,
                                ISqlRepository<Faq> faqRepo,
                                ISqlRepository<Instruction> instructionRepo,
                                ISqlRepository<Contacts> contactRepo,
                                ISqlRepository<BillingPlan> planRepo,
                                ISqlRepository<BillingPayment> billingRepo,
                                IMailService mailService)
        {
            _context=context;
            _faqRepo=faqRepo;
            _instructionRepo=instructionRepo;
            _contactRepo=contactRepo;
            _planRepo=planRepo;
            _billingRepo=billingRepo;
            _mailService=mailService;
        }

        ///<summary>
        ///Sent Password Email
        ///</summary>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendPasswordMail(ForgetPassword request)
        {
            try
            {
                await _mailService.SendPasswordEmailAsync(request);
                return Ok(new Confirmation { Status = "success", ResponseMsg = "pleasecheckyourEmail"});
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }              
        }

        ///<summary>
        ///Sent Welcome Email
        ///</summary>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendWelcomeMail(WelcomeRequest request)
        {
            try
            {
                await _mailService.SendWelcomeEmailAsync(request);
                return Ok(new Confirmation { Status = "success", ResponseMsg = "emailsentSuccessful"});
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Sent Email to checked students
        ///</summary>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SentEmailToCheckedStudents(ReExamRequest obj)
        {
            try
            {              
                await _mailService.SendReportEmailAsync(obj); 
                return Ok();           
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Sent Invitation Email
        ///</summary>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendInvitationMail(List<Invitation> listOfAddress)
        {
            try
            {
                await _mailService.SendInvitationEmailAsync(listOfAddress);
                return Ok(new Confirmation { Status = "success", ResponseMsg = "emailsentSuccessful"});
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Get Site Settings by User id
        ///</summary>
        [HttpGet("{userId}")]
        [AllowAnonymous]
        public ActionResult GetSiteSettingsByUserId(int userId)
        {
            try
            {              
                var siteSettings=_context.SiteSettings.FirstOrDefault(q=>q.AddedBy==userId);
                return Ok(siteSettings);           
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Update Settings for Admin
        ///</summary>
        [Authorize(Roles="Admin")]
        [HttpPut]       
        public ActionResult UpdateSettingsAdmin(PaymentSettings model)
        {
            try
            {
                var objSettings=_context.SiteSettings.FirstOrDefault(opt=>opt.AddedBy==model.AddedBy);
                objSettings.StripeSecretKey=model.StripeSecretKey;
                objSettings.ClientUrl=model.ClientUrl;
                objSettings.LastUpdatedBy=model.AddedBy;
                objSettings.LastUpdatedDate=DateTime.Now;
                _context.SaveChanges();
                return Ok(objSettings);                                           
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Get Quiz Payments by User id
        ///</summary>
        [HttpGet("{adminId}")]
        [AllowAnonymous]
        public ActionResult GetQuizPaymentsByUserId(int adminId)
        {
            try
            {
                var data=from p in _context.QuizPayments join q in _context.QuizTopics on p.QuizTopicId
                equals q.QuizTopicId join u in _context.Users on p.AddedBy equals u.UserId
                where p.AdminId.Equals(adminId) orderby p.DateAdded descending
                select new {q.QuizTitle,q.QuizPrice,p.Amount,p.Email,p.Currency,p.SessionId,u.FullName,p.DateAdded};
                return Ok(data);           
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }
        ///<summary>
        ///Get Site Settings
        ///</summary>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetSiteSettings()
        {
            try
            {              
                var siteSettings=_context.SiteSettings.OrderBy(q=>q.SiteSettingsId).FirstOrDefault();
                return Ok(siteSettings);           
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Update General Settings
        ///</summary>
        [Authorize(Roles="SuperAdmin")]
        [HttpPut]       
        public ActionResult UpdateGeneralSetting(SiteSettings model)
        {
            try
            {
                var objSettings=_context.SiteSettings.SingleOrDefault(opt=>opt.SiteSettingsId==model.SiteSettingsId);
                objSettings.SiteTitle=model.SiteTitle;
                objSettings.WelComeMessage=model.WelComeMessage;
                objSettings.CopyRightText=model.CopyRightText;
                objSettings.AllowWelcomeEmail=model.AllowWelcomeEmail;
                objSettings.AllowFaq=model.AllowFaq;
                objSettings.Version=model.Version;
                objSettings.LogoPath=model.LogoPath;
                objSettings.FaviconPath=model.FaviconPath;             
                objSettings.LastUpdatedBy=model.LastUpdatedBy;
                objSettings.LastUpdatedDate=DateTime.Now;
                _context.SaveChanges();
                return Ok(objSettings);                                           
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Update Exam Settings
        ///</summary>
        [Authorize(Roles="SuperAdmin")]
        [HttpPut]       
        public ActionResult UpdateExamSetting(SiteSettings model)
        {
            try
            {
                var objSettings=_context.SiteSettings.SingleOrDefault(opt=>opt.SiteSettingsId==model.SiteSettingsId);
                objSettings.EndExam=model.EndExam;
                objSettings.LogoOnExamPage=model.LogoOnExamPage;
                objSettings.LastUpdatedBy=model.LastUpdatedBy;
                objSettings.LastUpdatedDate=DateTime.Now;
                _context.SaveChanges();
                return Ok(objSettings);                                           
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Update Payment Settings
        ///</summary>
        [Authorize(Roles="SuperAdmin")]
        [HttpPut]       
        public ActionResult UpdatePaymentSetting(SiteSettings model)
        {
            try
            {
                var objSettings=_context.SiteSettings.SingleOrDefault(opt=>opt.SiteSettingsId==model.SiteSettingsId);             
                objSettings.PaidRegistration=model.PaidRegistration;
                objSettings.RegistrationPrice=model.RegistrationPrice==null?0:model.RegistrationPrice;
                objSettings.Currency=model.Currency;
                objSettings.CurrencySymbol=model.CurrencySymbol;
                objSettings.StripePubKey=model.StripePubKey;
                objSettings.StripeSecretKey=model.StripeSecretKey;
                objSettings.LastUpdatedBy=model.LastUpdatedBy;
                objSettings.LastUpdatedDate=DateTime.Now;
                _context.SaveChanges();
                return Ok(objSettings);                                           
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Update Email Settings
        ///</summary>
        [Authorize(Roles="SuperAdmin")]
        [HttpPut]       
        public ActionResult UpdateEmailSetting(SiteSettings model)
        {
            try
            {
                var objSettings=_context.SiteSettings.SingleOrDefault(opt=>opt.SiteSettingsId==model.SiteSettingsId);
                objSettings.DefaultEmail=model.DefaultEmail;
                objSettings.Password=model.Password;
                objSettings.Host=model.Host;
                objSettings.Port=model.Port;
                objSettings.LastUpdatedBy=model.LastUpdatedBy;
                objSettings.LastUpdatedDate=DateTime.Now;
                _context.SaveChanges();
                return Ok(objSettings);                                           
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Update Color Settings
        ///</summary>
        [Authorize(Roles="SuperAdmin")]
        [HttpPut]       
        public ActionResult UpdateColorSetting(SiteSettings model)
        {
            try
            {
                var objSettings=_context.SiteSettings.SingleOrDefault(opt=>opt.SiteSettingsId==model.SiteSettingsId);           
                objSettings.AppBarColor=model.AppBarColor;
                objSettings.HeaderColor=model.HeaderColor;
                objSettings.FooterColor=model.FooterColor;
                objSettings.BodyColor=model.BodyColor;
                objSettings.LastUpdatedBy=model.LastUpdatedBy;
                objSettings.LastUpdatedDate=DateTime.Now;
                _context.SaveChanges();
                return Ok(objSettings);                                           
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Update Landing Settings
        ///</summary>
        [Authorize(Roles="SuperAdmin")]
        [HttpPut]       
        public ActionResult UpdateLandingSetting(SiteSettings model)
        {
            try
            {
                var objSettings=_context.SiteSettings.SingleOrDefault(opt=>opt.SiteSettingsId==model.SiteSettingsId);           
                objSettings.HomeHeader1=model.HomeHeader1;
                objSettings.HomeDetail1=model.HomeDetail1;
                objSettings.HomeHeader2=model.HomeHeader2;
                objSettings.HomeDetail2=model.HomeDetail2;
                objSettings.HomeBox1Header=model.HomeBox1Header;
                objSettings.HomeBox1Detail=model.HomeBox1Detail;
                objSettings.HomeBox2Header=model.HomeBox2Header;
                objSettings.HomeBox2Detail=model.HomeBox2Detail;
                objSettings.HomeBox3Header=model.HomeBox3Header;
                objSettings.HomeBox3Detail=model.HomeBox3Detail;
                objSettings.HomeBox4Header=model.HomeBox4Header;
                objSettings.HomeBox4Detail=model.HomeBox4Detail;
                objSettings.Feature1Header=model.Feature1Header;
                objSettings.Feature1Detail=model.Feature1Detail;
                objSettings.Feature2Header=model.Feature2Header;
                objSettings.Feature2Detail=model.Feature2Detail;
                objSettings.Feature3Header=model.Feature3Header;
                objSettings.Feature3Detail=model.Feature3Detail;
                objSettings.Feature4Header=model.Feature4Header;
                objSettings.Feature4Detail=model.Feature4Detail;
                objSettings.RegistrationText=model.RegistrationText;
                objSettings.ContactUsText=model.ContactUsText;
                objSettings.ContactUsTelephone=model.ContactUsTelephone;
                objSettings.ContactUsEmail=model.ContactUsEmail;
                objSettings.FooterText=model.FooterText;
                objSettings.FooterFacebook=model.FooterFacebook;
                objSettings.FooterTwitter=model.FooterTwitter;
                objSettings.FooterLinkedin=model.FooterLinkedin;
                objSettings.FooterInstagram=model.FooterInstagram;
                objSettings.LastUpdatedBy=model.LastUpdatedBy;
                objSettings.LastUpdatedDate=DateTime.Now;
                _context.SaveChanges();
                return Ok(objSettings);                                           
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Update Email Text Settings
        ///</summary>
        [Authorize(Roles="SuperAdmin")]
        [HttpPut]       
        public ActionResult UpdateEmailTextSetting(SiteSettings model)
        {
            try
            {
                var objSettings=_context.SiteSettings.SingleOrDefault(opt=>opt.SiteSettingsId==model.SiteSettingsId);           
                objSettings.ForgetPasswordEmailSubject=model.ForgetPasswordEmailSubject;
                objSettings.ForgetPasswordEmailHeader=model.ForgetPasswordEmailHeader;
                objSettings.ForgetPasswordEmailBody=model.ForgetPasswordEmailBody;
                objSettings.WelcomeEmailSubject=model.WelcomeEmailSubject;
                objSettings.WelcomeEmailHeader=model.WelcomeEmailHeader;
                objSettings.WelcomeEmailBody=model.WelcomeEmailBody;
                objSettings.InvitationEmailSubject=model.InvitationEmailSubject;
                objSettings.InvitationEmailHeader=model.InvitationEmailHeader;
                objSettings.InvitationEmailBody=model.InvitationEmailBody;
                objSettings.ReportEmailSubject=model.ReportEmailSubject;
                objSettings.ReportEmailHeader=model.ReportEmailHeader;
                objSettings.ReportEmailBody=model.ReportEmailBody;
                objSettings.LastUpdatedBy=model.LastUpdatedBy;
                objSettings.LastUpdatedDate=DateTime.Now;
                _context.SaveChanges();
                return Ok(objSettings);                                           
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Update Settings
        ///</summary>
        [Authorize(Roles="SuperAdmin")]
        [HttpPut]       
        public ActionResult UpdateSettings(SiteSettings model)
        {
            try
            {
                var objSettings=_context.SiteSettings.SingleOrDefault(opt=>opt.SiteSettingsId==model.SiteSettingsId);
                objSettings.SiteTitle=model.SiteTitle;
                objSettings.WelComeMessage=model.WelComeMessage;
                objSettings.CopyRightText=model.CopyRightText;
                objSettings.Version=model.Version;
                objSettings.EndExam=model.EndExam;
                objSettings.LogoOnExamPage=model.LogoOnExamPage;
                objSettings.PaidRegistration=model.PaidRegistration;
                objSettings.RegistrationPrice=model.RegistrationPrice==null?0:model.RegistrationPrice;
                objSettings.Currency=model.Currency;
                objSettings.CurrencySymbol=model.CurrencySymbol;
                objSettings.StripePubKey=model.StripePubKey;
                objSettings.StripeSecretKey=model.StripeSecretKey;
                objSettings.DefaultEmail=model.DefaultEmail;
                objSettings.Password=model.Password;
                objSettings.Host=model.Host;
                objSettings.Port=model.Port;
                objSettings.LogoPath=model.LogoPath;
                objSettings.FaviconPath=model.FaviconPath;
                objSettings.AppBarColor=model.AppBarColor;
                objSettings.FooterColor=model.FooterColor;
                objSettings.BodyColor=model.BodyColor;
                objSettings.AllowWelcomeEmail=model.AllowWelcomeEmail;
                objSettings.AllowFaq=model.AllowFaq;
                objSettings.LastUpdatedBy=model.LastUpdatedBy;
                objSettings.LastUpdatedDate=DateTime.Now;
                _context.SaveChanges();
                return Ok(objSettings);                                           
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Update Client Url
        ///</summary>
        [AllowAnonymous]
        [HttpPut]       
        public ActionResult UpdateClientUrl(UserInfo model)
        {
            try
            {
                var objSettings=_context.SiteSettings.SingleOrDefault();
                objSettings.ClientUrl=model.DisplayName;
                _context.SaveChanges();
                return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullySaved" });
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }
        ///<summary>
        ///Site Logo upload
        ///</summary>
        [Authorize(Roles="SuperAdmin")]   
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadLogo()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Logo");
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
        ///Site Favicon upload
        ///</summary>
        [Authorize(Roles="SuperAdmin")]   
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadFavicon()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Favicon");
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
        ///Get FAQ List
        ///</summary>
        [HttpGet]
        [Authorize(Roles="Admin,SuperAdmin,Student")]
        public ActionResult GetFaqList()
        {
            try
            {              
                var faqList=_faqRepo.SelectAll();
                return Ok(faqList);           
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Delete FAQ by Id
        ///</summary>
        [HttpDelete("{id}")]
        [Authorize(Roles="SuperAdmin")]
        public ActionResult DeleteFaq(int id)
        {
            try
            {      
                _faqRepo.Delete(id);
                return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullyDeleted" });                                             
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Create Faq
        ///</summary>
        [Authorize(Roles="SuperAdmin")]
        [HttpPost]       
        public ActionResult CreateFaq(Faq model)
        {
            try
            {                  
                var objCheck=_context.Faqs.SingleOrDefault(opt=>opt.Title.ToLower()==model.Title.ToLower());
                if(objCheck==null)
                {
                    model.DateAdded=DateTime.Now;
                    model.IsActive=true;
                    _faqRepo.Insert(model);
                    return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullySaved" });                  
                }
                else
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "duplicateFaq" });
                }                    
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Update Faq
        ///</summary>
        [Authorize(Roles="SuperAdmin")]
        [HttpPut]       
        public ActionResult UpdateFaq(Faq model)
        {
            try
            {
                var objFaq=_context.Faqs.SingleOrDefault(opt=>opt.FaqId==model.FaqId);
                var objCheck=_context.Faqs.SingleOrDefault(opt=>opt.Title.ToLower()==model.Title.ToLower());

                if(objCheck!=null && objCheck.Title.ToLower()!=objFaq.Title.ToLower())
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "duplicateFaq" });
                }
                else
                {
                    objFaq.Title=model.Title;
                    objFaq.Description=model.Description;
                    objFaq.LastUpdatedBy=model.LastUpdatedBy;
                    objFaq.LastUpdatedDate=DateTime.Now;
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
        ///Get Instructions
        ///</summary>
        [HttpGet("{quizId}")]
        [Authorize(Roles="Admin,Student")]
        public ActionResult GetInstructions(int quizId)
        {
            try
            {              
                var list=_context.Instructions.Where(q=>q.QuizTopicId==quizId).ToList();
                return Ok(list);           
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Delete Instruction by Id
        ///</summary>
        [HttpDelete("{id}")]
        [Authorize(Roles="Admin")]
        public ActionResult DeleteInstruction(int id)
        {
            try
            {      
                _instructionRepo.Delete(id);
                return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullyDeleted" });                                             
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Create Instruction
        ///</summary>
        [Authorize(Roles="Admin")]
        [HttpPost]       
        public ActionResult CreateInstruction(Instruction model)
        {
            try
            {                  
                var objCheck=_context.Instructions.SingleOrDefault(opt=>opt.Description.ToLower()==model.Description.ToLower() && opt.QuizTopicId==model.QuizTopicId);
                if(objCheck==null)
                {
                    model.DateAdded=DateTime.Now;
                    model.IsActive=true;
                    _instructionRepo.Insert(model);
                    return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullySaved" });                  
                }
                else
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "duplicateFaq" });
                }                    
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Update Instruction
        ///</summary>
        [Authorize(Roles="Admin")]
        [HttpPut]       
        public ActionResult UpdateInstruction(Instruction model)
        {
            try
            {
                var objInstruction=_context.Instructions.SingleOrDefault(opt=>opt.InstructionId==model.InstructionId);
                var objCheck=_context.Instructions.SingleOrDefault(opt=>opt.Description.ToLower()==model.Description.ToLower() && opt.QuizTopicId==model.QuizTopicId);

                if(objCheck!=null && objCheck.Description.ToLower()!=objInstruction.Description.ToLower())
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "duplicateInstruction"});
                }
                else
                {
                    objInstruction.Description=model.Description;
                    objInstruction.LastUpdatedBy=model.LastUpdatedBy;
                    objInstruction.LastUpdatedDate=DateTime.Now;
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
        ///Create Contact-us
        ///</summary>
        [AllowAnonymous]
        [HttpPost]       
        public ActionResult CreateContacts(Contacts model)
        {
            try
            {                  
                model.DateAdded=DateTime.Now;
                _contactRepo.Insert(model);
                return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullySubmitted" });                    
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

         ///<summary>
        ///Update Faq
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin")]
        [HttpPut]       
        public ActionResult RenewPlan(BillRegister model)
        {
            try
            {
                using var transaction = _context.Database.BeginTransaction();

                model.Price=_context.BillingPlans.FirstOrDefault(q=>q.BillingPlanId==model.BillingPlanId).Price;
                PaymentService objPaymentService=new PaymentService(_context);
                int paymentId=objPaymentService.InsertBillingPayment(model);

                var objUser=_context.Users.FirstOrDefault(q=>q.UserId==model.AddedBy);
                objUser.BillingPlanId=model.BillingPlanId;
                objUser.PaymentMode=model.PaymentMode;
                objUser.TransactionDetail=model.TransactionDetail;
                objUser.PaymentId=paymentId;
                _context.SaveChanges();

                transaction.Commit();  
                return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullyRenewed" });                                         
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Get Contacts
        ///</summary>
        [HttpGet]
        [Authorize(Roles="SuperAdmin")]
        public ActionResult GetContacts()
        {
            try
            {              
                var list=_context.Contacts.OrderByDescending(q=>q.DateAdded);
                return Ok(list);           
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Create Payment
        ///</summary>
        [AllowAnonymous]
        [HttpPost]       
        public ActionResult CreatePayment(BillRegister model)
        {
            try
            {       
                PaymentService objPaymentService=new PaymentService(_context);
                objPaymentService.InsertBillingPayment(model);
                return Ok();                   
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Get Billing List
        ///</summary>
        [HttpGet]
        [Authorize(Roles="Admin,SuperAdmin")]
        public ActionResult GetBillingPaymentList()
        {
            try
            {              
                var list=_context.BillingPayments.OrderByDescending(q=>q.DateAdded);
                return Ok(list);           
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }


        ///<summary>
        ///Get Plan List
        ///</summary>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetPlanList()
        {
            try
            {              
                var list=_context.BillingPlans.OrderBy(q=>q.Price);
                return Ok(list);           
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Delete Plan by Id
        ///</summary>
        [HttpDelete("{id}")]
        [Authorize(Roles="SuperAdmin")]
        public ActionResult DeletePlan(int id)
        {
            try
            {   
                var chkMigration=_context.BillingPlans.FirstOrDefault(q=>q.BillingPlanId==id);
                if(chkMigration.IsMigrationData)
                {
                    return Accepted(new Confirmation { Status = "error", ResponseMsg = "notallowedtodeletethisondemoversion" });
                }
                else if(_context.Users.FirstOrDefault(q=>q.BillingPlanId==id)!=null)
                {
                    return Accepted(new Confirmation { Status = "error", ResponseMsg = "thisplanhasexistingusersNotallowedtodeletethis" });
                }  
                else
                {
                    _planRepo.Delete(id);
                    return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullyDeleted" });
                }                                             
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Create Plan
        ///</summary>
        [Authorize(Roles="SuperAdmin")]
        [HttpPost]       
        public ActionResult CreatePlan(BillingPlan model)
        {
            try
            {                  
                var objCheck=_context.BillingPlans.Where(opt=>opt.Title.ToLower()==model.Title.ToLower()
                || opt.Price==model.Price).FirstOrDefault();
                
                if(objCheck==null)
                {
                    model.DateAdded=DateTime.Now;
                    model.IsActive=true;
                    _planRepo.Insert(model);
                    return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullySaved" });                  
                }
                else if(objCheck.Title.ToLower()==model.Title.ToLower())
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "duplicateTitle" });
                }
                else if(objCheck.Price==model.Price)
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "duplicatePrice" });
                }
                else
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "somethingUnexpected" });
                }                    
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Update Plan
        ///</summary>
        [Authorize(Roles="SuperAdmin")]
        [HttpPut]       
        public ActionResult UpdatePlan(BillingPlan model)
        {
            try
            {
                var objPlan=_context.BillingPlans.SingleOrDefault(opt=>opt.BillingPlanId==model.BillingPlanId);
                var objCheckTitle=_context.BillingPlans.SingleOrDefault(opt=>opt.Title.ToLower()==model.Title.ToLower());
                var objCheckPrice=_context.BillingPlans.SingleOrDefault(opt=>opt.Price==model.Price);

                if(objCheckTitle!=null && objCheckTitle.Title.ToLower()!=objPlan.Title.ToLower())
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "duplicateTitle" });
                }
                else if(objCheckPrice!=null && objCheckPrice.Price!=objPlan.Price)
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "duplicatePrice" });
                }
                else
                {
                    objPlan.Title=model.Title;
                    objPlan.Price=model.Price;
                    objPlan.Interval=model.Interval;
                    objPlan.AssessmentCount=model.AssessmentCount;
                    objPlan.QuestionPerAssessmentCount=model.QuestionPerAssessmentCount;
                    objPlan.ResponsePerAssessmentCount=model.ResponsePerAssessmentCount;
                    objPlan.AdditionalText=model.AdditionalText;
                    objPlan.LastUpdatedDate=DateTime.Now;
                    _context.SaveChanges();
                    return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullyUpdated" });
                }                                             
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }
    }
}