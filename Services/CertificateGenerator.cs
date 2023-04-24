using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using QuizplusApi.Models;
using System.Linq;
using QuizplusApi.ViewModels.Quiz;
using System.Globalization;

namespace QuizplusApi.Services
{
    public class CertificateGenerator
    {
        private readonly AppDbContext _context;
        public CertificateGenerator(AppDbContext context)
        {
            _context=context;
        }
        public string GetHTMLString(int cid,int uid,int rid)
        {
            string base64Header="data:image/png;base64,";
            string topLeftImg="",topRightImg="",bottomMiddleImg="",signatureLeftImg="",signatureRightImg="";

            var objCertificate=_context.CertificateTemplates.FirstOrDefault(q=>q.CertificateTemplateId==cid);

            if(objCertificate.TopLeftImagePath.Length>10)
            {
                string path=Path.Combine(Directory.GetCurrentDirectory(), @"Resources","CertificateImages", objCertificate.TopLeftImagePath);
                byte[] imageArray = File.ReadAllBytes(path);
                topLeftImg = Convert.ToBase64String(imageArray);
            }
            if(objCertificate.TopRightImagePath.Length>10)
            {
                string path=Path.Combine(Directory.GetCurrentDirectory(), @"Resources","CertificateImages", objCertificate.TopRightImagePath);
                byte[] imageArray = File.ReadAllBytes(path);
                topRightImg = Convert.ToBase64String(imageArray);
            }
            if(objCertificate.BottomMiddleImagePath.Length>10)
            {
                string path=Path.Combine(Directory.GetCurrentDirectory(), @"Resources","CertificateImages", objCertificate.BottomMiddleImagePath);
                byte[] imageArray = File.ReadAllBytes(path);
                bottomMiddleImg = Convert.ToBase64String(imageArray);
            }
            if(objCertificate.LeftSignatureImagePath.Length>10)
            {
                string path=Path.Combine(Directory.GetCurrentDirectory(), @"Resources","CertificateImages", objCertificate.LeftSignatureImagePath);
                byte[] imageArray = File.ReadAllBytes(path);
                signatureLeftImg = Convert.ToBase64String(imageArray);
            }
            if(objCertificate.RightSignatureImagePath.Length>10)
            {
                string path=Path.Combine(Directory.GetCurrentDirectory(), @"Resources","CertificateImages", objCertificate.RightSignatureImagePath);
                byte[] imageArray = File.ReadAllBytes(path);
                signatureRightImg = Convert.ToBase64String(imageArray);
            }


            if(uid>0 && rid>0)
            {
                string FormattedText(string text,CertificateInfo obj)
                {
                    text=text.Replace("[fullName]",obj.FullName);
                    text=text.Replace("[email]",obj.Email);
                    text=text.Replace("[mobile]",obj.Mobile);
                    text=text.Replace("[address]",obj.Address);
                    text=text.Replace("[dateOfBirth]",obj.DateOfBirth!=null?obj.DateOfBirth.Value.ToString("dddd, dd MMMM yyyy"):"");
                    text=text.Replace("[quizTitle]",obj.QuizTitle);
                    text=text.Replace("[quizTime]",obj.QuizTime.ToString());
                    text=text.Replace("[timeTaken]",obj.TimeTaken.ToString());
                    text=text.Replace("[quizMark]",obj.QuizMark.ToString());
                    text=text.Replace("[userObtainedQuizMark]",obj.UserObtainedQuizMark.ToString());
                    text=text.Replace("[attemptCount]",obj.AttemptCount.ToString());
                    return text;
                }
                var objStudentInfo=(from i in _context.QuizResponseInitials join u in _context.Users on i.UserId equals u.UserId join t in _context.QuizTopics on i.QuizTopicId equals t.QuizTopicId
                where i.QuizResponseInitialId.Equals(rid) && i.UserId.Equals(uid) 
                select new{u.UserId,u.FullName,u.Email,u.Mobile,u.Address,
                u.DateOfBirth,i.AttemptCount,i.QuizTopicId,i.QuizResponseInitialId,
                i.QuizTitle,i.QuizMark,i.QuizTime,i.UserObtainedQuizMark,i.TimeTaken,
                i.IsExamined,i.DateAdded,i.QuizPassMarks,t.CertificateTemplateId,t.AllowCorrectOption,
                t.QuizMarkOptionId}).FirstOrDefault();

                CertificateInfo obj=new CertificateInfo(){
                    Address=objStudentInfo.Address,
                    AllowCorrectOption=objStudentInfo.AllowCorrectOption,
                    AttemptCount=objStudentInfo.AttemptCount,                                    
                    DateOfBirth=objStudentInfo.DateOfBirth,
                    Email=objStudentInfo.Email,
                    FullName=objStudentInfo.FullName,
                    IsExamined=objStudentInfo.IsExamined,
                    Mobile=objStudentInfo.Mobile,
                    QuizMark=objStudentInfo.QuizMark,
                    QuizPassMarks=objStudentInfo.QuizPassMarks,
                    QuizTime=objStudentInfo.QuizTime,
                    QuizTitle=objStudentInfo.QuizTitle,
                    TimeTaken=objStudentInfo.TimeTaken,
                    UserObtainedQuizMark=objStudentInfo.UserObtainedQuizMark
                };
                
                objCertificate.Title=FormattedText(objCertificate.Title,obj);
                objCertificate.Heading=FormattedText(objCertificate.Heading,obj);
                objCertificate.MainText=FormattedText(objCertificate.MainText,obj);
            }
            
            var sb = new StringBuilder();
            sb.Append(@"<!DOCTYPE html>
                        <html>
                            <head>
                            <title></title>
                                <style> body { background-color:"+objCertificate.BackgroundColor+";}></style></head><body><div class='parent'>");
                            if(objCertificate.TopLeftImagePath.Length>10 || objCertificate.TopRightImagePath.Length>10)
                                {
                                    sb.Append(@"<div class='topImages'>");
                                    if(objCertificate.TopLeftImagePath.Length>10)
                                    {
                                        sb.Append(@"<img class='imgTopLeft' src="+base64Header+topLeftImg+" alt='Left Logo'>");
                                    }
                                    if(objCertificate.TopRightImagePath.Length>10)
                                    {
                                        sb.Append(@"<img class='imgTopRight' src="+base64Header+topRightImg+" alt='Right Logo'>");
                                    }
                                    sb.Append(@"</div>");
                                }
                            sb.Append(@"<div class='title'>"+objCertificate.Title+"</div><div class='heading'>"+objCertificate.Heading+"</div><div class='mainText'>"+objCertificate.MainText+"</div>");
                            if(objCertificate.LeftSignatureImagePath.Length>10 || objCertificate.RightSignatureImagePath.Length>10)
                                {
                                    sb.Append(@"<div class='signatureImages'>");
                                    if(objCertificate.LeftSignatureImagePath.Length>10)
                                    {
                                        sb.Append(@"<img class='imgTopLeft' src="+base64Header+signatureLeftImg+" alt='Left Signature'>");
                                    }
                                    if(objCertificate.RightSignatureImagePath.Length>10)
                                    {
                                        sb.Append(@"<img class='imgTopRight' src="+base64Header+signatureRightImg+" alt='Right Signature'>");
                                    }
                                    sb.Append(@"</div>");
                                }
                            if(objCertificate.LeftSignatureText.Length>5 || objCertificate.RightSignatureText.Length>5)
                                {
                                    sb.Append(@"<div class='signatureHrTag'>");
                                    if(objCertificate.LeftSignatureText.Length>5)
                                    {
                                        sb.Append(@"<hr class='hrLeft'>");
                                    }
                                    if(objCertificate.RightSignatureText.Length>5)
                                    {
                                        sb.Append(@"<hr class='hrRight'>");
                                    }
                                    sb.Append(@"</div>");
                                }
                            if(objCertificate.LeftSignatureText.Length>5 || objCertificate.RightSignatureText.Length>5)
                                {
                                    sb.Append(@"<div class='signatureText'>");
                                    if(objCertificate.LeftSignatureText.Length>5)
                                    {
                                        sb.Append(@"<span class='textLeft'>"+objCertificate.LeftSignatureText+"</span>");
                                    }
                                    if(objCertificate.RightSignatureText.Length>5)
                                    {
                                        sb.Append(@"<span class='textRight'>"+objCertificate.RightSignatureText+"</span>");
                                    }
                                    sb.Append(@"</div>");
                                }
                            sb.Append(@"<div class='bottomText'>");
                                if(objCertificate.BottomMiddleImagePath.Length>10)
                                {
                                    sb.Append(@"<img class='imgBottomLeft' src="+base64Header+bottomMiddleImg+" alt='Bottom Image'>");
                                }
                                sb.Append(@"<span class='textBottomMiddle'>ASSHR"+(objCertificate.CertificateTemplateId<10?("0"+objCertificate.CertificateTemplateId):objCertificate.CertificateTemplateId)+"</span>");
                                sb.Append(@"<span class='textBottomRight'>Printed on "+DateTime.Now.ToString("yyyy/M/dd",CultureInfo.InvariantCulture)+"</span>");
                            sb.Append(@"</div>");
                               
            sb.Append(@"</div></body>
                        </html>");
            return sb.ToString();
        }
    }
}