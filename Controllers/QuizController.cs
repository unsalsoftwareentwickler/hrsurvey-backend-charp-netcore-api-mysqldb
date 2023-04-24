using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QuizplusApi.Models;
using QuizplusApi.Models.Question;
using QuizplusApi.Models.Quiz;
using QuizplusApi.Services;
using QuizplusApi.ViewModels.Helper;
using QuizplusApi.ViewModels.Question;
using QuizplusApi.ViewModels.Quiz;

namespace QuizplusApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class QuizController:ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;
        private IConverter _converter;
        private readonly ISqlRepository<QuizTopic> _quizTopicRepo;
        private readonly ISqlRepository<QuizMarkOption> _quizMarkOptionRepo;
        private readonly ISqlRepository<QuizParticipantOption> _quizParticipantOptionRepo;
        private readonly ISqlRepository<QuizParticipant> _quizParticipantRepo;
        private readonly ISqlRepository<QuizQuestion> _quizQuestionRepo;
        private readonly ISqlRepository<QuizResponseInitial> _quizResponseInitialRepo;
        private readonly ISqlRepository<QuizResponseDetail> _quizResponseDetailRepo;
        private readonly ISqlRepository<CertificateTemplate> _certificateTemplateRepo;
        private readonly string _conString;

        public QuizController(IConfiguration config,
                            AppDbContext context,
                            IConverter converter,
                            ISqlRepository<QuizTopic> quizTopicRepo,
                            ISqlRepository<QuizMarkOption> quizMarkOptionRepo,
                            ISqlRepository<QuizParticipantOption> quizParticipantOptionRepo,
                            ISqlRepository<QuizParticipant> quizParticipantRepo,
                            ISqlRepository<QuizQuestion> quizQuestionRepo,
                            ISqlRepository<QuizResponseInitial> quizResponseInitialRepo,
                            ISqlRepository<QuizResponseDetail> quizResponseDetailRepo,
                            ISqlRepository<CertificateTemplate> certificateTemplateRepo)
        {
            _config=config;
            _context=context;
            _converter = converter;
            _conString=_config.GetConnectionString("ApiConnStringMysql");
            _quizTopicRepo=quizTopicRepo;
            _quizMarkOptionRepo=quizMarkOptionRepo;
            _quizParticipantOptionRepo=quizParticipantOptionRepo;
            _quizParticipantRepo=quizParticipantRepo;
            _quizQuestionRepo=quizQuestionRepo;
            _quizResponseInitialRepo=quizResponseInitialRepo;
            _quizResponseDetailRepo=quizResponseDetailRepo;
            _certificateTemplateRepo=certificateTemplateRepo;
        }

        ///<summary>
        ///Get Quizes with Questions count
        ///</summary>
        [HttpGet("{userId}")]
        [Authorize(Roles="Admin,SuperAdmin")]
        public ActionResult GetQuizList(int userId)
        {
            try
            {              
                var quizList=from q in _context.QuizTopics where q.AddedBy.Equals(userId) orderby q.DateAdded descending
                select new {q.QuizTopicId,q.QuizTitle,q.QuizTime,q.QuizTotalMarks,q.QuizPassMarks,q.QuizMarkOptionId,q.QuizParticipantOptionId,
                q.CertificateTemplateId,q.AllowMultipleInputByUser,q.AllowMultipleAnswer,q.AllowMultipleAttempt,
                q.AllowCorrectOption,q.AllowQuizStop,q.AllowQuizSkip,q.AllowQuestionSuffle,q.QuizscheduleStartTime,q.QuizscheduleEndTime,q.IsRunning,q.QuizPrice,q.IsActive,
                QuestionsCount=_context.QuizQuestions.Where(m=>m.QuizTopicId==q.QuizTopicId).Count()};
                return Ok(quizList);           
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Get Single Quiz
        ///</summary>
        [HttpGet("{quizId}")]
        [Authorize(Roles="Admin,SuperAdmin")]
        public ActionResult GetSingleQuiz(int quizId)
        {
            try
            {              
                var quiz=_context.QuizTopics.Where(q=>q.QuizTopicId==quizId).SingleOrDefault();
                return Ok(quiz);           
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Get Quizes with Questions count
        ///</summary>
        [HttpGet("{userId}")]
        [Authorize(Roles="Admin,SuperAdmin")]
        public ActionResult GetQuizesForReports(int userId)
        {
            try
            {
                var list=_context.QuizTopics.Where(q=>q.AddedBy==userId);
                return Ok(list);
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Delete Quiz with questions by QuizId
        ///</summary>
        [HttpDelete("{id}")]
        [Authorize(Roles="Admin,SuperAdmin")]
        public ActionResult DeleteQuiz(int id)
        {
            try
            {      
                using var transaction = _context.Database.BeginTransaction();

                var quizObj=_context.QuizTopics.Where(q=>q.QuizTopicId==id).FirstOrDefault();
                if(quizObj!=null)
                {
                    _context.QuizTopics.Remove(quizObj);
                    _context.SaveChanges();
                }
                var questionList=_context.QuizQuestions.Where(q=>q.QuizTopicId==id);
                foreach(var item in questionList)
                {
                    _context.QuizQuestions.Remove(item);
                }
                _context.SaveChanges();
                
                transaction.Commit();
                return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullyDeleted" });                                             
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Create Quiz Topic
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin")]
        [HttpPost]       
        public ActionResult CreateQuizTopic(QuizTopic model)
        {
            try
            {                  
                var objCheck=_context.QuizTopics.Where(opt=>opt.QuizTitle.ToLower()==model.QuizTitle.ToLower() && opt.AddedBy==model.AddedBy).FirstOrDefault();
                var objUser=_context.Users.FirstOrDefault(q=>q.UserId==model.AddedBy);
                //int allowedAssessmentCount=_context.BillingPlans.Where(q=>q.BillingPlanId==objUser.BillingPlanId).FirstOrDefault().AssessmentCount;
                //var objPayment=_context.BillingPayments.FirstOrDefault(q=>q.BillingPaymentId==objUser.PaymentId);
              
                if(objCheck==null)
                {      
                    model.Categories="";          
                    model.DateAdded=DateTime.Now;
                    model.IsActive=true;
                    _quizTopicRepo.Insert(model);                  
                    return Ok(model);                 
                }
                else
                {
                    return Accepted(new Confirmation { Status = "error", ResponseMsg = "somethingUnexpected" });
                }                    
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Update Quiz Topic
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin")]
        [HttpPut]       
        public ActionResult UpdateQuizTopic(QuizTopic model)
        {
            try
            {
                var objQuizTopic=_context.QuizTopics.SingleOrDefault(opt=>opt.QuizTopicId==model.QuizTopicId);
                var objCheck=_context.QuizTopics.SingleOrDefault(opt=>opt.QuizTitle.ToLower()==model.QuizTitle.ToLower() && opt.AddedBy==model.AddedBy);

                if(objCheck!=null && objCheck.QuizTitle.ToLower()!=objQuizTopic.QuizTitle.ToLower())
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "duplicateAssessmentTitle" });
                }
                else
                {
                    using var transaction = _context.Database.BeginTransaction();

                    string message="successfullyUpdated";

                    //int previousMarkOptionId=objQuizTopic.QuizMarkOptionId;
                    //decimal previousMarks=objQuizTopic.QuizTotalMarks;

                    objQuizTopic.QuizTitle=model.QuizTitle;
                    objQuizTopic.QuizTime=model.QuizTime;
                    objQuizTopic.QuizTotalMarks=model.QuizTotalMarks;
                    objQuizTopic.QuizPassMarks=model.QuizPassMarks;
                    objQuizTopic.QuizMarkOptionId=model.QuizMarkOptionId;
                    objQuizTopic.QuizParticipantOptionId=model.QuizParticipantOptionId;
                    objQuizTopic.CertificateTemplateId=model.CertificateTemplateId;
                    objQuizTopic.AllowMultipleInputByUser=model.AllowMultipleInputByUser;
                    objQuizTopic.AllowMultipleAnswer=model.AllowMultipleAnswer;
                    objQuizTopic.AllowMultipleAttempt=model.AllowMultipleAttempt;
                    objQuizTopic.AllowQuizStop=model.AllowQuizStop;
                    objQuizTopic.AllowQuizSkip=model.AllowQuizSkip;
                    objQuizTopic.AllowQuestionSuffle=model.AllowQuestionSuffle;
                    objQuizTopic.AllowCorrectOption=model.AllowCorrectOption;
                    objQuizTopic.QuizscheduleStartTime=model.QuizscheduleStartTime;
                    objQuizTopic.QuizscheduleEndTime=model.QuizscheduleEndTime;
                    objQuizTopic.QuizPrice=model.QuizPrice;
                    objQuizTopic.LastUpdatedBy=model.LastUpdatedBy;
                   
                    objQuizTopic.PositionCode = model.PositionCode;
                    objQuizTopic.PositionName = model.PositionName;
                    objQuizTopic.JobCode = model.JobCode;
                    objQuizTopic.JobName = model.JobName;
                    objQuizTopic.DepartmentCode = model.DepartmentCode;
                    objQuizTopic.DepartmentName = model.DepartmentName;
                    objQuizTopic.RegionCode = model.RegionCode;
                    objQuizTopic.RegionName = model.RegionName;

                    objQuizTopic.LastUpdatedDate=DateTime.Now;
                    _context.SaveChanges();

                    transaction.Commit();
                    return Ok(new Confirmation { Status = "success", ResponseMsg =  message});
                }                                             
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Start Quiz
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin")]
        [HttpPut]       
        public ActionResult StartQuiz(QuizTopic model)
        {
            try
            {
                var questionsAll=_context.QuizQuestions.Where(q=>q.QuizTopicId==model.QuizTopicId);
                var questionsMcq=_context.QuizQuestions.Where(q=>q.QuizTopicId==model.QuizTopicId && q.QuestionTypeId==1);
                bool isCorrectOptionEmpty=false;
                foreach(var item in questionsMcq)
                {
                    if(item.CorrectOption=="")
                    {
                        isCorrectOptionEmpty=true;
                    }
                }

                bool isPerQuestionMarkEmpty=false;
                foreach(var item in questionsAll)
                {
                    if(item.PerQuestionMark==0)
                    {
                        isPerQuestionMarkEmpty=true;
                    }
                }

                if(questionsAll.Count()==0)
                {
                    return Accepted(new Confirmation { Status = "error", ResponseMsg = "liveInterruptionForNoQuestion" });
                }
                else if(isCorrectOptionEmpty==true && (model.QuizMarkOptionId==1 || model.QuizMarkOptionId==3))
                {
                    return Accepted(new Confirmation { Status = "error", ResponseMsg = "liveInterruptionForNoCorrectAnswer" });
                }
                else if(isPerQuestionMarkEmpty==true && model.QuizMarkOptionId==3)
                {
                    return Accepted(new Confirmation { Status = "error", ResponseMsg = "liveInterruptionForNoMarksSet" });
                }
                else
                {
                    using var transaction = _context.Database.BeginTransaction();
                    
                    var objQuizTopic=_context.QuizTopics.SingleOrDefault(opt=>opt.QuizTopicId==model.QuizTopicId);
                    objQuizTopic.IsRunning=true;

                    if(model.QuizMarkOptionId==1)
                    {
                        decimal sumOfQuestionMarks=0;
                        decimal perQuestionMark=model.QuizTotalMarks/questionsAll.Count();
                        var questionsToUpdateMarks=_context.QuizQuestions.Where(q=>q.QuizTopicId==model.QuizTopicId).ToList();
                        foreach(var item in questionsToUpdateMarks)
                        {
                            item.PerQuestionMark=perQuestionMark;
                        }
                        _context.SaveChanges();

                        sumOfQuestionMarks=_context.QuizQuestions.Where(q=>q.QuizTopicId==model.QuizTopicId).Sum(a=>a.PerQuestionMark);
                        if(model.QuizTotalMarks!=sumOfQuestionMarks)
                        {
                            questionsToUpdateMarks[0].PerQuestionMark=questionsToUpdateMarks[0].PerQuestionMark+(model.QuizTotalMarks-sumOfQuestionMarks);                   
                            _context.SaveChanges();
                        }
                    }
                    else if(model.QuizMarkOptionId==2)
                    {
                        objQuizTopic.AllowMultipleAnswer=false;
                        objQuizTopic.QuizTotalMarks=0;
                        objQuizTopic.QuizTime=0;
                        _context.SaveChanges();

                        var listOfQuestions=_context.QuizQuestions.Where(q=>q.QuizTopicId==model.QuizTopicId);
                        foreach(var item in listOfQuestions)
                        {
                            item.CorrectOption="";
                            item.PerQuestionMark=0;
                        }
                        _context.SaveChanges();
                    }
                    else if(model.QuizMarkOptionId==3)
                    {
                        decimal totalMarks=_context.QuizQuestions.Where(q=>q.QuizTopicId==model.QuizTopicId).Sum(s=>s.PerQuestionMark);
                        objQuizTopic.QuizTotalMarks=totalMarks;
                        _context.SaveChanges();
                    }
                    transaction.Commit();   
                    return Ok(new Confirmation { Status = "success", ResponseMsg = "thissssessmentislivenow" });               
                }
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Stop Quiz
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin")]
        [HttpPut]       
        public ActionResult StopQuiz(QuizTopic model)
        {
            try
            {
                var objQuizTopic=_context.QuizTopics.SingleOrDefault(opt=>opt.QuizTopicId==model.QuizTopicId);
                objQuizTopic.IsRunning=false;
                _context.SaveChanges();
                return Ok(new Confirmation { Status = "success", ResponseMsg = "thisAssessmentisstoppedfromlive" });
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Get Quiz Participant Option List
        ///</summary>
        [HttpGet]
        [Authorize(Roles="Admin,SuperAdmin")]
        public ActionResult GetQuizParticipantOptionList()
        {
            try
            {              
                var list=_quizParticipantOptionRepo.SelectAll();
                return Ok(list);           
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Get Quiz Marks Option List
        ///</summary>
        [HttpGet]
        [Authorize(Roles="Admin,SuperAdmin")]
        public ActionResult GetQuizMarksOptionList()
        {
            try
            {              
                var list=_quizMarkOptionRepo.SelectAll();
                return Ok(list);           
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Create Quiz Participants
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin")]
        [HttpPost]       
        public ActionResult CreateQuizParticipants(List<QuizParticipant> list)
        {
            try
            {                
                using var transaction = _context.Database.BeginTransaction();
                var emailsToDelete=_context.QuizParticipants.Where(q=>q.QuizTopicId==list[0].QuizTopicId);
                foreach(var item in emailsToDelete)
                {
                    _context.QuizParticipants.Remove(item);                   
                }
                _context.SaveChanges();

                foreach(var item in list)
                {
                    _context.QuizParticipants.Add(item);   
                }
                _context.SaveChanges();

                transaction.Commit();
                return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullySaved" });                  
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Get Quiz Participants Comma seperated emails
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin")]
        [HttpGet("{quizTopicId}")]       
        public ActionResult GetQuizParticipantsEmail(int quizTopicId)
        {
            try
            {  
                var emailList=_context.QuizParticipants.Where(q=>q.QuizTopicId==quizTopicId);
                string emails="";
                int count=0;
                foreach(var item in emailList)
                {
                    emails+=item.Email+",";
                    count=count+1;
                }
                if(count>1)
                {
                    emails=emails.TrimEnd(',');
                }
                return Ok(emails);                  
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Get Quizes with Question Count
        ///</summary>
        [HttpGet("{userId}")]
        [Authorize(Roles="Admin,SuperAdmin")]
        public ActionResult GetQuizWithQuestionCount(int userId)
        {
            try
            {
                var list=from q in _context.QuizTopics where q.AddedBy.Equals(userId) orderby q.DateAdded descending
                select new {q.QuizTopicId,q.QuizTitle,
                q.QuizTime,q.QuizTotalMarks,q.QuizMarkOptionId,q.AllowMultipleAnswer,q.Categories,
                QuestionsCount=_context.QuizQuestions.Where(m=>m.QuizTopicId==q.QuizTopicId).Count()};
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Get Random Questions
        ///</summary>
        [HttpGet("{quizId}/{randomFlag}")]
        [Authorize(Roles="Student")]
        public ActionResult GetRandomQuestions(int quizId,bool randomFlag)
        {
            try
            {
                var questions=_context.QuizQuestions.Where(q=>q.QuizTopicId==quizId);
                List<int> values = new List<int>();
                foreach(var obj in questions)
                {
                    values.Add(obj.SerialNo);
                }
                if(randomFlag)
                {
                    Random rand = new Random();
                    values = values.OrderBy(_ => rand.Next()).ToList();
                }
                return Ok(values);
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Get Quiz Question List
        ///</summary>
        [HttpGet("{id}")]
        [Authorize(Roles="Admin,SuperAdmin")]
        public ActionResult GetQuizQuestionList(int id)
        {
            try
            {                            
                var quizQuestionList=from q in _context.QuizQuestions join c in _context.QuestionCategories
                on q.QuestionCategoryId equals c.QuestionCategoryId where q.QuizTopicId==id orderby q.DateAdded descending
                select new {q.QuizQuestionId,q.QuizTopicId,
                q.QuestionDetail,q.SerialNo,q.PerQuestionMark,q.QuestionTypeId,q.QuestionLavelId,q.QuestionCategoryId,
                q.OptionA,q.OptionB,q.OptionC,q.OptionD,q.OptionE,q.CorrectOption,q.AnswerExplanation,q.ImagePath,q.VideoPath,
                q.IsCodeSnippet,q.IsActive,q.AddedBy,c.QuestionCategoryName};
                return Ok(quizQuestionList);           
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Get single question according to serial no.
        ///</summary>
        [HttpGet("{quizId}/{serial}")]
        [Authorize(Roles="Student")]
        public ActionResult GetSingleQuestion(int quizId,int serial)
        {
            try
            {                            
                var question=from q in _context.QuizQuestions join l in _context.QuestionLavels on q.QuestionLavelId equals
                l.QuestionLavelId join c in _context.QuestionCategories on q.QuestionCategoryId equals c.QuestionCategoryId
                where q.QuizTopicId==quizId && q.SerialNo==serial select new{q.QuizQuestionId,q.QuizTopicId,q.QuestionDetail,
                q.SerialNo,q.PerQuestionMark,q.QuestionTypeId,q.QuestionLavelId,q.QuestionCategoryId,
                q.OptionA,q.OptionB,q.OptionC,q.OptionD,q.OptionE,q.CorrectOption,q.AnswerExplanation,
                q.ImagePath,q.VideoPath,q.IsCodeSnippet,q.IsActive,q.IsMigrationData,q.AddedBy,q.DateAdded,
                l.QuestionLavelName,c.QuestionCategoryName};

                return Ok(question.SingleOrDefault());           
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Delete Quiz Question by id
        ///</summary>
        [HttpDelete("{id}")]
        [Authorize(Roles="Admin,SuperAdmin")]
        public ActionResult DeleteSingleQuizQuestion(int id)
        {
            try
            {
                using var transaction = _context.Database.BeginTransaction();

                var msg="successfullyDeleted";
                var objQuizQuestion=_context.QuizQuestions.SingleOrDefault(q=>q.QuizQuestionId==id);
                _context.QuizQuestions.Remove(objQuizQuestion);
                _context.SaveChanges();

                var allQuestions=_context.QuizQuestions.Where(q=>q.QuizTopicId==objQuizQuestion.QuizTopicId);
                int counter=1;
                foreach(var item in allQuestions)
                {
                    item.SerialNo=counter;
                    counter++;
                }
                _context.SaveChanges();

                var objCheckQuizRunning=_context.QuizTopics.SingleOrDefault(q=>q.QuizTopicId==objQuizQuestion.QuizTopicId);
                if(objCheckQuizRunning.IsRunning==true)
                {
                    objCheckQuizRunning.IsRunning=false;
                    _context.SaveChanges();
                    msg="deleteAssessmentReset";
                }

                transaction.Commit();
                return Ok(new Confirmation { Status = "success", ResponseMsg = msg });          
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Create Quiz Question
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin")]
        [HttpPost]       
        public ActionResult CreateQuizQuestion(QuizQuestion model)
        {
            try
            {                          
                var objCheck=_context.QuizQuestions.FirstOrDefault(opt=>opt.QuizTopicId==model.QuizTopicId&&opt.QuestionDetail.ToLower()==model.QuestionDetail.ToLower());
                int? billingPlanId=_context.Users.FirstOrDefault(q=>q.UserId==model.AddedBy).BillingPlanId;
                int allowedQuestionCount=_context.BillingPlans.FirstOrDefault(q=>q.BillingPlanId==billingPlanId).QuestionPerAssessmentCount;

                if(_context.QuizQuestions.Where(q=>q.QuizTopicId==model.QuizTopicId).Count()==allowedQuestionCount)
                {
                    return Accepted(new Confirmation { Status = "error", ResponseMsg = "maxQuestionQuota" });
                }
                else if(objCheck!=null && objCheck.QuestionDetail.ToLower()==model.QuestionDetail.ToLower())
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "duplicateQuestion" });
                }
                else if(objCheck==null)
                {
                    using var transaction = _context.Database.BeginTransaction();                   

                    model.SerialNo=_context.QuizQuestions.Where(q=>q.QuizTopicId==model.QuizTopicId).Count()+1;
                    model.OptionA=model.OptionA==null?"":model.OptionA.Trim();
                    model.OptionB=model.OptionB==null?"":model.OptionB.Trim();
                    model.OptionC=model.OptionC==null?"":model.OptionC.Trim();
                    model.OptionD=model.OptionD==null?"":model.OptionD.Trim();
                    model.OptionE=model.OptionE==null?"":model.OptionE.Trim();
                    model.CorrectOption=model.CorrectOption==null?"":model.CorrectOption;
                    model.ImagePath=model.ImagePath==null?"":model.ImagePath;
                    model.VideoPath=model.VideoPath==null?"":model.VideoPath;
                    model.DateAdded=DateTime.Now;
                    model.IsActive=true;                  
                    _context.QuizQuestions.Add(model);
                    _context.SaveChanges();

                    var objCheckQuiz=_context.QuizTopics.SingleOrDefault(q=>q.QuizTopicId==model.QuizTopicId);

                    var uniqueCategories=_context.QuizQuestions.Select(s=>new{QuizTopicId=s.QuizTopicId,QuestionCategoryId=s.QuestionCategoryId}).Where(q=>q.QuizTopicId==model.QuizTopicId).Distinct();
                    string categories="";
                    foreach(var item in uniqueCategories)
                    {
                        categories+=item.QuestionCategoryId+",";
                    }
                    objCheckQuiz.Categories=categories.TrimEnd(',');
                    _context.SaveChanges();

                    if(objCheckQuiz.IsRunning==true && (objCheckQuiz.QuizMarkOptionId==1 || objCheckQuiz.QuizMarkOptionId==3))
                    {
                        objCheckQuiz.IsRunning=false;
                        _context.SaveChanges();
                        transaction.Commit();
                        return Ok(new Confirmation { Status = "success", ResponseMsg = "newQuestionAddAssessmentStart" });
                    }
                    else
                    {
                        transaction.Commit();
                        return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullySaved" });
                    }                  
                }
                else
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "somethingUnexpected!" });
                }                    
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Update Quiz Question
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin")]
        [HttpPut]       
        public ActionResult UpdateQuizQuestion(QuizQuestion model)
        {
            try
            {
                var objQuizQuestion=_context.QuizQuestions.SingleOrDefault(opt=>opt.QuizQuestionId==model.QuizQuestionId);
                var objCheck=_context.QuizQuestions.FirstOrDefault(opt=>opt.QuizTopicId==model.QuizTopicId&&opt.QuestionDetail.ToLower()==model.QuestionDetail.ToLower());

                if(objCheck!=null && objCheck.QuizTopicId==objQuizQuestion.QuizTopicId && objCheck.QuestionDetail.ToLower()!=objQuizQuestion.QuestionDetail.ToLower())
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "duplicateQuestion" });
                }
                else
                {
                    string message="successfullyUpdated";
                    using var transaction = _context.Database.BeginTransaction();

                    var objCheckQuiz=_context.QuizTopics.SingleOrDefault(q=>q.QuizTopicId==model.QuizTopicId);

                    if(objCheckQuiz.QuizMarkOptionId==3)
                    {
                        decimal previousMark=objQuizQuestion.PerQuestionMark;
                        objQuizQuestion.PerQuestionMark=model.PerQuestionMark;
                        if(previousMark!=objQuizQuestion.PerQuestionMark && objCheckQuiz.IsRunning==true)
                        {
                            objCheckQuiz.IsRunning=false;
                            _context.SaveChanges();
                            message="questionMarksChangedAssessmentStart";
                        }
                    }
                    objQuizQuestion.QuizTopicId=model.QuizTopicId;
                    objQuizQuestion.QuestionDetail=model.QuestionDetail;
                    objQuizQuestion.QuestionTypeId=model.QuestionTypeId;
                    objQuizQuestion.QuestionLavelId=model.QuestionLavelId;
                    objQuizQuestion.QuestionCategoryId=model.QuestionCategoryId;
                    objQuizQuestion.OptionA=model.OptionA;
                    objQuizQuestion.OptionB=model.OptionB;
                    objQuizQuestion.OptionC=model.OptionC==null?"":model.OptionC.Trim();
                    objQuizQuestion.OptionD=model.OptionD==null?"":model.OptionD.Trim();
                    objQuizQuestion.OptionE=model.OptionE==null?"":model.OptionE.Trim();
                    objQuizQuestion.CorrectOption=model.CorrectOption==null?"":model.CorrectOption;
                    objQuizQuestion.AnswerExplanation=model.AnswerExplanation;
                    objQuizQuestion.ImagePath=model.ImagePath==null?"":model.ImagePath;
                    objQuizQuestion.VideoPath=model.VideoPath==null?"":model.VideoPath;
                    objQuizQuestion.IsCodeSnippet=model.IsCodeSnippet;
                    objQuizQuestion.LastUpdatedBy=model.LastUpdatedBy;
                    objQuizQuestion.LastUpdatedDate=DateTime.Now;
                    _context.SaveChanges();

                    var uniqueCategories=_context.QuizQuestions.Select(s=>new{QuizTopicId=s.QuizTopicId,QuestionCategoryId=s.QuestionCategoryId}).Where(q=>q.QuizTopicId==model.QuizTopicId).Distinct();
                    string categories="";
                    foreach(var item in uniqueCategories)
                    {
                        categories+=item.QuestionCategoryId+",";
                    }
                    objCheckQuiz.Categories=categories.TrimEnd(',');
                    _context.SaveChanges();

                    transaction.Commit();
                    return Ok(new Confirmation { Status = "success", ResponseMsg = message });
                }                                             
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Question Image upload
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin")]   
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadQuestionImage()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "QuestionImages");
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
        ///Create Quiz Response Initial
        ///</summary>
        [Authorize(Roles="Student")]
        [HttpPost]       
        public ActionResult CreateQuizResponseInitial(QuizResponseInitial model)
        {
            try
            {
                var objUser=_context.Users.FirstOrDefault(q=>q.UserId==(_context.Users.FirstOrDefault(a=>a.UserId==model.AddedBy)).AddedBy);
                //var objPayment=_context.BillingPayments.FirstOrDefault(q=>q.BillingPaymentId==objUser.PaymentId);
                //int allowedResponseCount=_context.BillingPlans.Where(q=>q.BillingPlanId==objUser.BillingPlanId).FirstOrDefault().ResponsePerAssessmentCount;
                
                //int attemptCount=_context.QuizResponseInitials.Where(q=>q.UserId==model.UserId && q.QuizTopicId==model.QuizTopicId).Count();

                //model.AttemptCount=attemptCount+1;
                //model.IsExamined=true;
                //model.StartTime=DateTime.Now;
                //model.DateAdded=DateTime.Now;
                //model.IsActive=true;

                _quizResponseInitialRepo.Insert(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Create Quiz Response Detail
        ///</summary>
        [Authorize(Roles="Student")]
        [HttpPost]       
        public ActionResult CreateQuizResponseDetail(QuizResponseDetail model)
        {
            try
            {
                using var transaction = _context.Database.BeginTransaction();

                var questionInfo=_context.QuizQuestions.SingleOrDefault(q=>q.QuizQuestionId==model.QuizQuestionId);
                model.QuestionDetail=questionInfo.QuestionDetail;
                model.CorrectAnswer=questionInfo.CorrectOption;
                model.AnswerExplanation=questionInfo.AnswerExplanation;
                model.QuestionMark=questionInfo.PerQuestionMark;
                model.ImagePath=questionInfo.ImagePath;
                model.VideoPath=questionInfo.VideoPath;
                model.IsActive=true;
                model.DateAdded=DateTime.Now;

                if(model.IsAnswerSkipped==false && questionInfo.QuestionTypeId==1)
                {
                    if(model.CorrectAnswer!="")
                    {
                        if(model.CorrectAnswer.Contains("#####") && model.UserAnswer.Contains("#####"))
                        {
                            string[] correctAnswers=model.CorrectAnswer.Split("#####");
                            string[] answerList=model.UserAnswer.Split("#####");
                            int countAnswer=0;
                            foreach(string answer in answerList)
                            {   
                                if(model.CorrectAnswer.Contains(answer))
                                {
                                    countAnswer++;
                                }
                                else
                                {
                                    countAnswer--;
                                }
                            }
                            if(countAnswer==correctAnswers.Count())
                            {
                                model.UserObtainedQuestionMark=model.QuestionMark;
                            }
                            else
                            {
                                model.UserObtainedQuestionMark=0;
                            }
                        }
                        else if(model.UserAnswer.Contains("#####"))
                        {
                            string[] userAnswerList=model.UserAnswer.Split("#####");
                            int countAnswer=0;
                            foreach(string userAnswer in userAnswerList)
                            {
                                if(model.CorrectAnswer.Contains(userAnswer))
                                {
                                    countAnswer++;
                                }
                            }
                            if(countAnswer>0)
                            {
                                model.UserObtainedQuestionMark=model.QuestionMark;
                            }
                            else
                            {
                                model.UserObtainedQuestionMark=0;
                            }

                        }
                        else if(model.CorrectAnswer.Contains(model.UserAnswer))
                        {
                            model.UserObtainedQuestionMark=model.QuestionMark;
                        }
                        else
                        {
                            model.UserObtainedQuestionMark=0;
                        }
                    }
                    model.IsExamined=true;
                }
                else if(model.IsAnswerSkipped==false && questionInfo.QuestionTypeId==2)
                {
                    model.UserObtainedQuestionMark=0;
                    model.IsExamined=false;
                }              
                else
                {
                    model.UserAnswer="";
                    model.UserObtainedQuestionMark=0;
                    model.IsExamined=true;
                }
                _context.QuizResponseDetails.Add(model);
                _context.SaveChanges();

                decimal sumOfQuizMark=0;
                QuizResponseInitial objInitial=_context.QuizResponseInitials.SingleOrDefault(q=>q.QuizResponseInitialId==model.QuizResponseInitialId);
                TimeSpan timeTakenForQuiz=DateTime.Now-objInitial.StartTime;

                int quizMarkOptionId=_context.QuizTopics.SingleOrDefault(q=>q.QuizTopicId==questionInfo.QuizTopicId).QuizMarkOptionId;
                if(questionInfo.QuestionTypeId==2)
                {
                    if(model.IsAnswerSkipped==false && quizMarkOptionId!=2)
                    {
                        objInitial.IsExamined=false;
                    }                  
                }
                else if(questionInfo.QuestionTypeId==1)
                {                   
                    if(quizMarkOptionId==1 || quizMarkOptionId==3)
                    {
                        sumOfQuizMark=_context.QuizResponseDetails.Where(q=>q.QuizResponseInitialId==model.QuizResponseInitialId).Sum(q=>q.UserObtainedQuestionMark);
                    }                  
                    objInitial.UserObtainedQuizMark=sumOfQuizMark;
                }
                objInitial.EndTime=DateTime.Now;
                objInitial.TimeTaken=timeTakenForQuiz.TotalMinutes;
                _context.SaveChanges();

                transaction.Commit();
                return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullySaved" });
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Update Quiz Taken time
        ///</summary>
        [Authorize(Roles="Student")]
        [HttpPut]       
        public ActionResult UpdateQuizTakenTime(QuizTimeTaken model)
        {
            try
            {
                var objQuizInitial=_context.QuizResponseInitials.SingleOrDefault(q=>q.QuizResponseInitialId==model.QuizResponseInitialId);
                objQuizInitial.TimeTaken=(DateTime.Now-objQuizInitial.StartTime).TotalMinutes>objQuizInitial.QuizTime?objQuizInitial.QuizTime:(DateTime.Now-objQuizInitial.StartTime).TotalMinutes;
                objQuizInitial.EndTime=DateTime.Now;
                _context.SaveChanges();
                return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullyUpdated" });
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Question CSV upload
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin")]   
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadQuestionCsv()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "QuestionCsv");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
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
        ///Read Question Upload CSV
        ///</summary>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ReadQuestionUploadCsv(CsvUploadHelper model)
        {
            try
            {
                var _uploadService=new UploadService();
                var csvPath=Path.Combine(Directory.GetCurrentDirectory(),model.Path);
                var uploadData=_uploadService.ReadCSVFile(csvPath);

                using var transaction = _context.Database.BeginTransaction();
                foreach(var item in uploadData)
                {
                    QuizQuestion obj=new QuizQuestion();
                    obj.QuizTopicId=model.QuizTopicId;
                    obj.QuestionDetail=item.QuestionDetail;
                    obj.PerQuestionMark=item.PerQuestionMark;
                    obj.QuestionTypeId=item.QuestionTypeId;
                    obj.QuestionLavelId=item.QuestionLavelId;
                    obj.QuestionCategoryId=item.QuestionCategoryId;
                    obj.OptionA=item.QuestionTypeId==1?item.OptionA:"";
                    obj.OptionB=item.QuestionTypeId==1?item.OptionB:"";
                    obj.OptionC=item.QuestionTypeId==1?item.OptionC:"";
                    obj.OptionD=item.QuestionTypeId==1?item.OptionD:"";
                    obj.OptionE=item.QuestionTypeId==1?item.OptionE:"";
                    obj.CorrectOption=item.QuestionTypeId==1?item.CorrectOption:"";
                    obj.AnswerExplanation=item.AnswerExplanation;
                    obj.ImagePath="";
                    obj.VideoPath="";
                    obj.DateAdded=DateTime.Now;
                    obj.IsActive=true;
                    obj.AddedBy=model.AddedBy;
                    _context.QuizQuestions.Add(obj);
                }
                _context.SaveChanges();

                var allQuestions=_context.QuizQuestions.Where(q=>q.QuizTopicId==model.QuizTopicId);
                int counter=1;
                foreach(var item in allQuestions)
                {
                    item.SerialNo=counter;
                    counter++;
                }
                _context.SaveChanges();

                var objCheckQuiz=_context.QuizTopics.SingleOrDefault(q=>q.QuizTopicId==model.QuizTopicId);
                var uniqueCategories=_context.QuizQuestions.Select(s=>new{QuizTopicId=s.QuizTopicId,QuestionCategoryId=s.QuestionCategoryId}).Where(q=>q.QuizTopicId==model.QuizTopicId).Distinct();
                string categories="";
                foreach(var item in uniqueCategories)
                {
                    categories+=item.QuestionCategoryId+",";
                }
                objCheckQuiz.Categories=categories.TrimEnd(',');
                objCheckQuiz.IsRunning=false;
                _context.SaveChanges();

                transaction.Commit();
                return Ok(new Confirmation { Status = "success", ResponseMsg = "bulkQuestionAdd" });
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }
        ///<summary>
        ///Get certificate templates.
        ///</summary>
        [HttpGet]
        [Authorize(Roles="Admin,SuperAdmin")]
        public ActionResult GetCertificateTemplates()
        {
            try
            {
                var list=_context.CertificateTemplates.ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Get certificate templates.
        ///</summary>
        [HttpGet("{id}")]
        [Authorize(Roles="Admin,SuperAdmin")]
        public ActionResult GetCertificateInfo(int id)
        {
            try
            {
                var list=_context.CertificateTemplates.Where(q=>q.AddedBy==id).Select(q=>new{q.CertificateTemplateId,q.Title,q.Heading,q.MainText,q.LeftSignatureText,q.RightSignatureText});
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        /* ///<summary>
        ///Certificate Image upload
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin")]   
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadCertificateImage()
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0 && file.ContentType.StartsWith("image/"))
                {
                    string base64String="";
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        base64String = Convert.ToBase64String(fileBytes);
                        base64String="data:image/png;base64,"+base64String;
                    }
                    return Ok(base64String);
                }
                else
                {
                    return Accepted(new Confirmation { Status = "error", ResponseMsg = "Not an image" });
                }
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });               
            }
        } */

        ///<summary>
        ///Get Certificate PDF
        ///</summary>
        [AllowAnonymous]
        [HttpGet("{cid}")]
        public ActionResult CreateCertificate(int cid)
        {
            try
            {
                var objGenerator=new CertificateGenerator(_context);

                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4Small,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "Certificate Issued",
                };
                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent =objGenerator.GetHTMLString(cid,0,0),
                    WebSettings = { DefaultEncoding = "utf-8",LoadImages=true, UserStyleSheet =  Path.Combine(Directory.GetCurrentDirectory(), @"Resources","Assests", "styles.css") },
                    //HeaderSettings = { FontName = "", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                    //FooterSettings = { FontName = "", FontSize = 9, Line = true, Center = "© Assess Hour | All rights reserved" },    
                };
                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };
                var file = _converter.Convert(pdf);
                return File(file, "application/pdf");
            }
            catch (System.Exception)
            {        
                throw;
            }
        }

        ///<summary>
        ///Get Certificate PDF
        ///</summary>
        [AllowAnonymous]
        [HttpGet("{cid}/{uid}/{rid}")]
        public ActionResult ShowCertificate(int cid,int uid,int rid)
        {
            try
            {
                var objGenerator=new CertificateGenerator(_context);

                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4Small,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "Certificate Issued",
                };
                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent =objGenerator.GetHTMLString(cid,uid,rid),
                    WebSettings = { DefaultEncoding = "utf-8",LoadImages=true, UserStyleSheet =  Path.Combine(Directory.GetCurrentDirectory(), @"Resources","Assests", "styles.css") },
                    //HeaderSettings = { FontName = FontName, FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                    //FooterSettings = { FontName = "", FontSize = 9, Line = true, Center = "© Assess Hour | All rights reserved" },    
                };
                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };
                var file = _converter.Convert(pdf);
                return File(file, "application/pdf");
            }
            catch (System.Exception)
            {        
                throw;
            }
        }

        ///<summary>
        ///Upload certificate images
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin")]   
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadCertificateImages()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "CertificateImages");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0 && file.ContentType.StartsWith("image/"))
                {
                    var fileName = Guid.NewGuid().ToString()+"_"+ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    //var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { fileName });
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
        ///Create certificate template
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin")]
        [HttpPost]       
        public ActionResult CreateCertificateTemplate(CertificateTemplate model)
        {
            try
            {
                model.IsActive=true;
                model.DateAdded=DateTime.Now;
                _certificateTemplateRepo.Insert(model);
                return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullySaved" });
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });               
            }
        }

        ///<summary>
        ///Update Quiz Question
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin")]
        [HttpPut]       
        public ActionResult UpdateCertificateTemplate(CertificateTemplate model)
        {
            try
            {
                var objTemplate=_context.CertificateTemplates.SingleOrDefault(opt=>opt.CertificateTemplateId==model.CertificateTemplateId);
                objTemplate.Title=model.Title;
                objTemplate.Heading=model.Heading;
                objTemplate.MainText=model.MainText;
                objTemplate.PublishDate=model.PublishDate;
                objTemplate.TopLeftImagePath=model.TopLeftImagePath;
                objTemplate.TopRightImagePath=model.TopRightImagePath;
                objTemplate.BottomMiddleImagePath=model.BottomMiddleImagePath;
                objTemplate.BackgroundColor=model.BackgroundColor;
                objTemplate.BackgroundImagePath=model.BackgroundImagePath;
                objTemplate.LeftSignatureText=model.LeftSignatureText;
                objTemplate.LeftSignatureImagePath=model.LeftSignatureImagePath;
                objTemplate.RightSignatureText=model.RightSignatureText;
                objTemplate.RightSignatureImagePath=model.RightSignatureImagePath;
                objTemplate.LastUpdatedBy=model.LastUpdatedBy;
                objTemplate.LastUpdatedDate=DateTime.Now;
                _context.SaveChanges();
                return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullyUpdated" });
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });               
            }
        }

        ///<summary>
        ///Delete certificate template
        ///</summary>
        [HttpDelete("{id}")]
        [Authorize(Roles="Admin,SuperAdmin")]
        public ActionResult DeleteSingleTemplate(int id)
        {
            try
            {
                _certificateTemplateRepo.Delete(id);
                return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullyDeleted" });
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Get Single certificate template
        ///</summary>
        [HttpGet("{id}")]
        [Authorize(Roles="Admin,SuperAdmin,Student")]
        public ActionResult GetSingleTemplate(int id)
        {
            try
            {
                var objCertificate=_certificateTemplateRepo.SelectById(id);
                return Ok(objCertificate);
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }

        ///<summary>
        ///Get Question Types
        ///</summary>
        [HttpGet]
        [Authorize(Roles="Admin,SuperAdmin")]
        public ActionResult GetQuestionType()
        {
            try
            {
                var list=_context.QuestionTypes.ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Get Question Lavels
        ///</summary>
        [HttpGet]
        [Authorize(Roles="Admin,SuperAdmin")]
        public ActionResult GetQuestionLavel()
        {
            try
            {
                var list=_context.QuestionLavels.ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Get Question Categories.
        ///</summary>
        [HttpGet("{userId}")]
        [Authorize(Roles="Admin,SuperAdmin")]
        public ActionResult GetQuestionCategory(int userId)
        {
            try
            {
                var list=_context.QuestionCategories.Where(q=>q.AddedBy==userId).OrderByDescending(a=>a.DateAdded);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });             
            }
        }

        ///<summary>
        ///Delete Question Category
        ///</summary>
        [HttpDelete("{id}")]
        [Authorize(Roles="Admin,SuperAdmin")]
        public ActionResult DeleteSingleQuestionCategory(int id)
        {
            try
            {
                if(_context.QuizQuestions.Where(q=>q.QuestionCategoryId==id).Count()==0)
                {
                    var objCategory=_context.QuestionCategories.SingleOrDefault(q=>q.QuestionCategoryId==id);
                    _context.QuestionCategories.Remove(objCategory);
                    _context.SaveChanges();
                    return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullyDeleted" });
                }
                else
                {
                    return Accepted(new Confirmation { Status = "error", ResponseMsg = "categoryUsageOthers" });
                }
                
            }
            catch (Exception ex)
            {              
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });
            }
        }
        ///<summary>
        ///Create Question Category
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin")]
        [HttpPost]       
        public ActionResult CreateQuestionCategory(QuestionCategory model)
        {
            try
            {
                var chkDuplicate=_context.QuestionCategories.FirstOrDefault(p=>p.QuestionCategoryName.ToLower()==model.QuestionCategoryName.ToLower() && p.AddedBy==model.AddedBy);
                if(chkDuplicate==null)
                {
                    model.IsActive=true;
                    model.DateAdded=DateTime.Now;
                    _context.QuestionCategories.Add(model);
                    _context.SaveChanges();
                    return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullySaved" });
                }
                else
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "categoryExists" });
                }
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });               
            }
        }

        ///<summary>
        ///Update Question Category
        ///</summary>
        [Authorize(Roles="Admin,SuperAdmin")]
        [HttpPut]       
        public ActionResult UpdateQuestionCategory(QuestionCategory model)
        {
            try
            {
                var objCategory=_context.QuestionCategories.SingleOrDefault(opt=>opt.QuestionCategoryId==model.QuestionCategoryId);
                var objCheck=_context.QuestionCategories.SingleOrDefault(p=>p.QuestionCategoryName.ToLower()==model.QuestionCategoryName.ToLower() && p.AddedBy==model.AddedBy);

                if(objCheck!=null && objCheck.QuestionCategoryName.ToLower()!=objCategory.QuestionCategoryName.ToLower())
                {
                    return Accepted(new Confirmation { Status = "duplicate", ResponseMsg = "categoryExists" });
                }
                else
                {
                    objCategory.QuestionCategoryName=model.QuestionCategoryName;
                    objCategory.LastUpdatedBy=model.LastUpdatedBy;
                    objCategory.LastUpdatedDate=DateTime.Now;
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
        ///Create Quiz Payment
        ///</summary>
        [Authorize(Roles="Student")]
        [HttpPost]       
        public ActionResult CreateQuizPayment(QuizPayment model)
        {
            try
            {
                model.DateAdded=DateTime.Now;
                _context.QuizPayments.Add(model);
                _context.SaveChanges();
                return Ok(new Confirmation { Status = "success", ResponseMsg = "successfullySaved" });
            }
            catch (Exception ex)
            {
                return Accepted(new Confirmation { Status = "error", ResponseMsg = ex.Message });               
            }
        }     
    }
}