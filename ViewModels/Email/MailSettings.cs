namespace QuizplusApi.ViewModels.Email
{
    public class MailSettings
    {
        public string SiteTitle { get; set; }
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string ForgetPasswordEmailSubject{get;set;}
        public string ForgetPasswordEmailHeader{get;set;}
        public string ForgetPasswordEmailBody{get;set;}
        public string WelcomeEmailSubject{get;set;}
        public string WelcomeEmailHeader{get;set;}
        public string WelcomeEmailBody{get;set;}
        public string InvitationEmailSubject{get;set;}
        public string InvitationEmailHeader{get;set;}
        public string InvitationEmailBody{get;set;}
        public string ReportEmailSubject{get;set;}
        public string ReportEmailHeader{get;set;}
        public string ReportEmailBody{get;set;}
    }
}