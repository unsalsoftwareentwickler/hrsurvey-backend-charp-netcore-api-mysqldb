using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuizplusApi.Models.Others
{
    public class SiteSettings
    {
        public int SiteSettingsId{get;set;}
        [StringLength(200)]
        public string SiteTitle{get;set;}
        [StringLength(1000)]
        public string WelComeMessage{get;set;}
        [StringLength(500)]
        public string CopyRightText{get;set;}
        [StringLength(2000)]
        public string LogoPath{get;set;}
        [StringLength(2000)]
        public string FaviconPath{get;set;}
        [StringLength(50)]
        public string AppBarColor{get;set;}
        [StringLength(50)]
        public string HeaderColor{get;set;}
        [StringLength(50)]
        public string FooterColor{get;set;}
        [StringLength(50)]
        public string BodyColor{get;set;}
        [DefaultValue(true)]
		public bool AllowWelcomeEmail { get; set; }
        [DefaultValue(true)]
		public bool AllowFaq { get; set; }
        [DefaultValue(true)]
		public bool AllowRightClick { get; set; }
        [DefaultValue(false)]
		public bool EndExam { get; set; }
        [DefaultValue(true)]
		public bool LogoOnExamPage { get; set; }
        [DefaultValue(true)]
		public bool PaidRegistration { get; set; }
        public int? RegistrationPrice{get;set;}
        [StringLength(50)]
        public string Currency{get;set;}
        [StringLength(50)]
        public string CurrencySymbol{get;set;}
        [StringLength(500)]
        public string StripePubKey{get;set;}
        [StringLength(500)]
        public string StripeSecretKey{get;set;}
        [StringLength(200)]
        public string ClientUrl{get;set;}

        [StringLength(200)]
        public string DefaultEmail{get;set;}
        [StringLength(200)]
        public string DisplayName{get;set;}
        [StringLength(200)]
        public string Password{get;set;}
        [StringLength(100)]
        public string Host{get;set;}
        [DefaultValue(0)]
        public int Port{get;set;}
        [DefaultValue(1)]
        public int Version{get;set;}
        public string HomeHeader1{get;set;}
        public string HomeDetail1{get;set;}
        public string HomeHeader2{get;set;}
        public string HomeDetail2{get;set;}
        public string HomeBox1Header{get;set;}
        public string HomeBox1Detail{get;set;}
        public string HomeBox2Header{get;set;}
        public string HomeBox2Detail{get;set;}
        public string HomeBox3Header{get;set;}
        public string HomeBox3Detail{get;set;}
        public string HomeBox4Header{get;set;}
        public string HomeBox4Detail{get;set;}
        public string Feature1Header{get;set;}
        public string Feature1Detail{get;set;}
        public string Feature2Header{get;set;}
        public string Feature2Detail{get;set;}
        public string Feature3Header{get;set;}
        public string Feature3Detail{get;set;}
        public string Feature4Header{get;set;}
        public string Feature4Detail{get;set;}
        public string RegistrationText{get;set;}
        public string ContactUsText{get;set;}
        public string ContactUsTelephone{get;set;}
        public string ContactUsEmail{get;set;}
        public string FooterText{get;set;}
        public string FooterFacebook{get;set;}
        public string FooterTwitter{get;set;}
        public string FooterLinkedin{get;set;}
        public string FooterInstagram{get;set;}
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
        [Required]
        [DefaultValue(true)]
		public bool IsActive { get; set; }
		[DefaultValue(false)]
		public bool IsMigrationData { get; set; }
		public int AddedBy { get; set; }
		public DateTime DateAdded { get; set; }
		public DateTime? LastUpdatedDate { get; set; }
		public int? LastUpdatedBy { get; set; }

        public string PositionCode { get; set; }
        public string PositionName { get; set; }
        public string JobCode { get; set; }
        public string JobName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }

    }
}