namespace QuizplusApi.ViewModels.Email
{
    public class Invitation
    {
        public string Email { get; set; }
        public string LogoPath { get; set; }
        public string SiteUrl { get; set; }
        public string SiteTitle { get; set; }
        public string RegistrationLink { get; set; }
    }
}