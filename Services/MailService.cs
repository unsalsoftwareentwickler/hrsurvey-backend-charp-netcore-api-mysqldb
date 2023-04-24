using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using QuizplusApi.Models;
using QuizplusApi.ViewModels.Email;

namespace QuizplusApi.Services
{
    public class MailService : IMailService
    {
        private readonly AppDbContext _context;
        public MailService(AppDbContext context)
        {
            _context=context;
        }
        public async Task SendPasswordEmailAsync(ForgetPassword request)
        {
            var emailSettings=_context.SiteSettings.OrderBy(q=>q.SiteSettingsId)
            .Select(p=>new MailSettings{SiteTitle=p.SiteTitle,Mail=p.DefaultEmail,DisplayName=p.DisplayName,Password=p.Password,Host=p.Host,
            Port=p.Port,ForgetPasswordEmailSubject=p.ForgetPasswordEmailSubject,ForgetPasswordEmailHeader=p.ForgetPasswordEmailHeader,
            ForgetPasswordEmailBody=p.ForgetPasswordEmailBody}).FirstOrDefault();

            emailSettings.ForgetPasswordEmailBody=emailSettings.ForgetPasswordEmailBody.Replace("[siteTitle]",request.SiteTitle).Replace("[password]",request.Password);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),"Resources","EmailTemplate","forgetPassword.html");
            StreamReader str = new StreamReader(filePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[subject]",emailSettings.ForgetPasswordEmailSubject).Replace("[logoPath]",request.LogoPath).Replace("[siteUrl]", request.SiteUrl).Replace("[header]", emailSettings.ForgetPasswordEmailHeader).Replace("[body]", emailSettings.ForgetPasswordEmailBody);
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(emailSettings.SiteTitle,emailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject=emailSettings.ForgetPasswordEmailSubject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.LocalDomain="localhost";
            smtp.Connect(emailSettings.Host, emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailSettings.Mail, emailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
        public async Task SendWelcomeEmailAsync(WelcomeRequest request)
        {
            var emailSettings=_context.SiteSettings.OrderBy(q=>q.SiteSettingsId)
            .Select(p=>new MailSettings{SiteTitle=p.SiteTitle,Mail=p.DefaultEmail,DisplayName=p.DisplayName,Password=p.Password,Host=p.Host,
            Port=p.Port,WelcomeEmailSubject=p.WelcomeEmailSubject,WelcomeEmailHeader=p.WelcomeEmailHeader,
            WelcomeEmailBody=p.WelcomeEmailBody}).FirstOrDefault();

            emailSettings.WelcomeEmailBody=emailSettings.WelcomeEmailBody.Replace("[siteTitle]",request.SiteTitle).Replace("[password]",request.Password).Replace("[email]",request.ToEmail);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),"Resources","EmailTemplate","welcome.html");
            StreamReader str = new StreamReader(filePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[subject]",emailSettings.WelcomeEmailSubject).Replace("[logoPath]",request.LogoPath).Replace("[siteUrl]", request.SiteUrl).Replace("[header]", emailSettings.WelcomeEmailHeader).Replace("[body]", emailSettings.WelcomeEmailBody);
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(emailSettings.SiteTitle,emailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = emailSettings.WelcomeEmailSubject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.LocalDomain="localhost";
            smtp.Connect(emailSettings.Host, emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailSettings.Mail, emailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendInvitationEmailAsync(List<Invitation> listOfAddress)
        {
            var emailSettings=_context.SiteSettings.OrderBy(q=>q.SiteSettingsId)
            .Select(p=>new MailSettings{SiteTitle=p.SiteTitle,Mail=p.DefaultEmail,DisplayName=p.DisplayName,Password=p.Password,Host=p.Host,
            Port=p.Port,InvitationEmailSubject=p.InvitationEmailSubject,InvitationEmailHeader=p.InvitationEmailHeader,
            InvitationEmailBody=p.InvitationEmailBody}).FirstOrDefault();

            emailSettings.InvitationEmailBody=emailSettings.InvitationEmailBody.Replace("[siteTitle]",listOfAddress[0].SiteTitle);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),"Resources","EmailTemplate","invitation.html");
            StreamReader str = new StreamReader(filePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[registrationLink]",listOfAddress[0].RegistrationLink).Replace("[subject]",emailSettings.InvitationEmailSubject).Replace("[logoPath]",listOfAddress[0].LogoPath).Replace("[siteUrl]", listOfAddress[0].SiteUrl).Replace("[header]", emailSettings.InvitationEmailHeader).Replace("[body]", emailSettings.InvitationEmailBody);
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(emailSettings.SiteTitle,emailSettings.Mail));
            InternetAddressList list=new InternetAddressList();
            foreach(var item in listOfAddress)
            {
                list.Add(MailboxAddress.Parse(item.Email));
            }
            email.To.AddRange(list);
            email.Subject=emailSettings.InvitationEmailSubject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.LocalDomain="localhost";
            smtp.Connect(emailSettings.Host, emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailSettings.Mail, emailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendReportEmailAsync(ReExamRequest report)
        {
            var emailSettings=_context.SiteSettings.OrderBy(q=>q.SiteSettingsId)
            .Select(p=>new MailSettings{SiteTitle=p.SiteTitle,Mail=p.DefaultEmail,DisplayName=p.DisplayName,Password=p.Password,Host=p.Host,
            Port=p.Port,ReportEmailHeader=p.ReportEmailHeader}).FirstOrDefault();

            string filePath = Path.Combine(Directory.GetCurrentDirectory(),"Resources","EmailTemplate","reportStudents.html");
            StreamReader str = new StreamReader(filePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[logoPath]",report.LogoPath).Replace("[siteUrl]", report.SiteUrl).Replace("[header]", emailSettings.ReportEmailHeader).Replace("[body]", report.Body);
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(emailSettings.SiteTitle,emailSettings.Mail));
            InternetAddressList list=new InternetAddressList();
            foreach(var item in report.emails)
            {
                list.Add(MailboxAddress.Parse(item.Email));
            }
            email.To.AddRange(list);
            email.Subject=report.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.LocalDomain="localhost";
            smtp.Connect(emailSettings.Host, emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailSettings.Mail, emailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}