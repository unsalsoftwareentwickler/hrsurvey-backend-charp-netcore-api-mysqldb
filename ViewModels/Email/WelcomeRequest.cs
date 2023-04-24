namespace QuizplusApi.ViewModels.Email
{
    public class WelcomeRequest
    {
        public string ToEmail { get; set; }
        public string Name { get; set; }
        public string LogoPath { get; set; }
        public string SiteUrl { get; set; }
        public string SiteTitle { get; set; }
        public string Password { get; set; }
    }
}