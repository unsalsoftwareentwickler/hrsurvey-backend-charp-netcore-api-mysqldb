using System;
using Microsoft.EntityFrameworkCore;
using QuizplusApi.Models.Menu;
using QuizplusApi.Models.Others;
using QuizplusApi.Models.Question;
using QuizplusApi.Models.Quiz;
using QuizplusApi.Models.User;

namespace QuizplusApi.ViewModels.Helper
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>(b=>
            {
                b.HasKey(e=>e.UserRoleId);  
                b.Property(b=>b.UserRoleId).HasIdentityOptions(startValue:3);        
                b.HasData(
                    new UserRole
                    {
                        UserRoleId=1,
                        RoleName="Admin",
                        DisplayName="Admin",
                        RoleDesc="Application Admin",
                        IsActive=true,
                        DateAdded=DateTime.Now,
                        AddedBy=1,
                        IsMigrationData=true
                    },
                    new UserRole
                    {
                        UserRoleId=2,
                        RoleName="Student",
                        DisplayName="Candidate",
                        RoleDesc="All Students",
                        IsActive=true,
                        DateAdded=DateTime.Now,
                        AddedBy=1,
                        IsMigrationData=true
                    },
                    new UserRole
                    {
                        UserRoleId=3,
                        RoleName="SuperAdmin",
                        DisplayName="Super Admin",
                        RoleDesc="Application Super Admin",
                        IsActive=true,
                        DateAdded=DateTime.Now,
                        AddedBy=1,
                        IsMigrationData=true
                    });  
            });

            modelBuilder.Entity<Users>(b=>{
                b.HasKey(e=>e.UserId);  
                b.Property(b=>b.UserId).HasIdentityOptions(startValue:2);              
                b.HasData(
                    new Users
                    {
                        UserId=1,
                        UserRoleId=3,
                        FullName="John Doe",
                        Email="superAdmin@assessHour.com",                       
                        Password="abcd1234",
                        IsActive=true,
                        DateAdded=DateTime.Now,
                        ImagePath="",
                        AddedBy=1,                       
                        IsMigrationData=true
                    }
                    );
            });

            modelBuilder.Entity<AppMenu>(b=>{
                b.HasKey(e=>e.AppMenuId);  
                b.Property(b=>b.AppMenuId).HasIdentityOptions(startValue:17);              
                b.HasData(
                    new AppMenu
                    {
                        AppMenuId=1,
                        MenuTitle="Dashboard",
                        Url="/dashboard",
                        SortOrder=1,                       
                        IconClass="dashboard",
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new AppMenu
                    {
                        AppMenuId=2,
                        MenuTitle="Menus",
                        Url="/menu/menus",
                        SortOrder=2,                       
                        IconClass="menu_open",
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new AppMenu
                    {
                        AppMenuId=3,
                        MenuTitle="Roles",
                        Url="/user/roles",
                        SortOrder=3,                       
                        IconClass="supervised_user_circle",
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new AppMenu
                    {
                        AppMenuId=4,
                        MenuTitle="Users",
                        Url="/user/users",
                        SortOrder=4,                       
                        IconClass="mdi-account-multiple",
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new AppMenu
                    {
                        AppMenuId=5,
                        MenuTitle="Category",
                        Url="/question/category",
                        SortOrder=5,                       
                        IconClass="category",
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new AppMenu
                    {
                        AppMenuId=6,
                        MenuTitle="Assessments",
                        Url="/quiz/topics",
                        SortOrder=6,                       
                        IconClass="emoji_objects",
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new AppMenu
                    {
                        AppMenuId=7,
                        MenuTitle="Questions",
                        Url="/question/quizes",
                        SortOrder=7,                       
                        IconClass="help_center",
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new AppMenu
                    {
                        AppMenuId=8,
                        MenuTitle="Reports",
                        Url="/report/students",
                        SortOrder=8,                       
                        IconClass="description",
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new AppMenu
                    {
                        AppMenuId=9,
                        MenuTitle="CertificateTemplate",
                        Url="/report/certificates",
                        SortOrder=9,                       
                        IconClass="card_giftcard",
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new AppMenu
                    {
                        AppMenuId=10,
                        MenuTitle="AppSettings",
                        Url="/settings/appSettings",
                        SortOrder=16,                       
                        IconClass="settings",
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new AppMenu
                    {
                        AppMenuId=11,
                        MenuTitle="ExamineAndReports",
                        Url="/report/admin",
                        SortOrder=10,                       
                        IconClass="description",
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new AppMenu
                    {
                        AppMenuId=12,
                        MenuTitle="Analytics",
                        Url="/report/analysis",
                        SortOrder=11,                       
                        IconClass="analytics",
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new AppMenu
                    {
                        AppMenuId=13,
                        MenuTitle="FAQ",
                        Url="/settings/faq",
                        SortOrder=12,                       
                        IconClass="help_center",
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new AppMenu
                    {
                        AppMenuId=14,
                        MenuTitle="Contacts",
                        Url="/settings/contacts",
                        SortOrder=13,                       
                        IconClass="contact_support",
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new AppMenu
                    {
                        AppMenuId=15,
                        MenuTitle="Payments",
                        Url="/settings/payments",
                        SortOrder=14,                       
                        IconClass="payments",
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new AppMenu
                    {
                        AppMenuId=16,
                        MenuTitle="Plans",
                        Url="/settings/plans",
                        SortOrder=15,                       
                        IconClass="monetization_on",
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    }
                    );
            });

            modelBuilder.Entity<MenuMapping>(b=>{
                b.HasKey(e=>e.MenuMappingId);  
                b.Property(b=>b.MenuMappingId).HasIdentityOptions(startValue:12);              
                b.HasData(
                    new MenuMapping
                    {
                        MenuMappingId=1,
                        UserRoleId=3,
                        AppMenuId=1,
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new MenuMapping
                    {
                        MenuMappingId=2,
                        UserRoleId=3,
                        AppMenuId=2,
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new MenuMapping
                    {
                        MenuMappingId=3,
                        UserRoleId=3,
                        AppMenuId=3,
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new MenuMapping
                    {
                        MenuMappingId=4,
                        UserRoleId=3,
                        AppMenuId=4,
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new MenuMapping
                    {
                        MenuMappingId=10,
                        UserRoleId=3,
                        AppMenuId=10,
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new MenuMapping
                    {
                        MenuMappingId=21,
                        UserRoleId=3,
                        AppMenuId=13,
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new MenuMapping
                    {
                        MenuMappingId=22,
                        UserRoleId=3,
                        AppMenuId=14,
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new MenuMapping
                    {
                        MenuMappingId=23,
                        UserRoleId=3,
                        AppMenuId=15,
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new MenuMapping
                    {
                        MenuMappingId=24,
                        UserRoleId=3,
                        AppMenuId=16,
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },






                    new MenuMapping
                    {
                        MenuMappingId=11,
                        UserRoleId=2,
                        AppMenuId=1,
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new MenuMapping
                    {
                        MenuMappingId=12,
                        UserRoleId=2,
                        AppMenuId=8,
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new MenuMapping
                    {
                        MenuMappingId=13,
                        UserRoleId=2,
                        AppMenuId=12,
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },






                    new MenuMapping
                    {
                        MenuMappingId=14,
                        UserRoleId=1,
                        AppMenuId=1,
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },                   
                    new MenuMapping
                    {
                        MenuMappingId=15,
                        UserRoleId=1,
                        AppMenuId=4,
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },  
                    new MenuMapping
                    {
                        MenuMappingId=16,
                        UserRoleId=1,
                        AppMenuId=5,
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },                 
                    new MenuMapping
                    {
                        MenuMappingId=17,
                        UserRoleId=1,
                        AppMenuId=6,
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new MenuMapping
                    {
                        MenuMappingId=18,
                        UserRoleId=1,
                        AppMenuId=7,
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new MenuMapping
                    {
                        MenuMappingId=19,
                        UserRoleId=1,
                        AppMenuId=11,
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new MenuMapping
                    {
                        MenuMappingId=20,
                        UserRoleId=1,
                        AppMenuId=9,
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new MenuMapping
                    {
                        MenuMappingId=25,
                        UserRoleId=1,
                        AppMenuId=15,
                        IsActive=true,
                        DateAdded=DateTime.Now,                      
                        AddedBy=1,                       
                        IsMigrationData=true
                    }
                    );
            });

            modelBuilder.Entity<QuizMarkOption>(b=>{
                b.HasKey(e=>e.QuizMarkOptionId);  
                b.Property(b=>b.QuizMarkOptionId).HasIdentityOptions(startValue:3);              
                b.HasData(
                    new QuizMarkOption
                    {
                        QuizMarkOptionId=1,
                        QuizMarkOptionName="Equal distribution"
                    },
                    new QuizMarkOption
                    {
                        QuizMarkOptionId=2,
                        QuizMarkOptionName="No marks(Survey)"
                    },
                    new QuizMarkOption
                    {
                        QuizMarkOptionId=3,
                        QuizMarkOptionName="Question wise set"
                    });
            });

            modelBuilder.Entity<QuizParticipantOption>(b=>{
                b.HasKey(e=>e.QuizParticipantOptionId);  
                b.Property(b=>b.QuizParticipantOptionId).HasIdentityOptions(startValue:3);              
                b.HasData(
                    new QuizParticipantOption
                    {
                        QuizParticipantOptionId=1,
                        QuizParticipantOptionName="All registered candidates"
                    },
                    new QuizParticipantOption
                    {
                        QuizParticipantOptionId=2,
                        QuizParticipantOptionName="Custom Input"
                    });
            });

            modelBuilder.Entity<ReportType>(b=>{
                b.HasKey(e=>e.ReportTypeId);  
                b.Property(b=>b.ReportTypeId).HasIdentityOptions(startValue:3);              
                b.HasData(
                    new ReportType
                    {
                        ReportTypeId=1,
                        ReportTypeName="Pending Examine"
                    },
                    new ReportType
                    {
                        ReportTypeId=2,
                        ReportTypeName="Reports"
                    });
            });

            modelBuilder.Entity<QuestionType>(b=>{
                b.HasKey(e=>e.QuestionTypeId);  
                b.Property(b=>b.QuestionTypeId).HasIdentityOptions(startValue:3);              
                b.HasData(
                    new QuestionType
                    {
                        QuestionTypeId=1,
                        QuestionTypeName="MCQ"
                    },
                    new QuestionType
                    {
                        QuestionTypeId=2,
                        QuestionTypeName="Descriptive"
                    });
            });

            modelBuilder.Entity<QuestionLavel>(b=>{
                b.HasKey(e=>e.QuestionLavelId);  
                b.Property(b=>b.QuestionLavelId).HasIdentityOptions(startValue:4);              
                b.HasData(
                    new QuestionLavel
                    {
                        QuestionLavelId=1,
                        QuestionLavelName="Easy"
                    },
                    new QuestionLavel
                    {
                        QuestionLavelId=2,
                        QuestionLavelName="Medium"
                    },
                    new QuestionLavel
                    {
                        QuestionLavelId=3,
                        QuestionLavelName="Hard"
                    });
            });

            modelBuilder.Entity<Faq>(b=>{
                b.HasKey(e=>e.FaqId);  
                b.Property(b=>b.FaqId).HasIdentityOptions(startValue:3);              
                b.HasData(
                    new Faq
                    {
                        FaqId=1,
                        Title="What are the purposes of this app?",
                        Description="Assess Hour will fulfill your need to take online Assessments,Exams,Quizes as well as surveys.",                                             
                        IsActive=true,
                        DateAdded=DateTime.Now,                       
                        AddedBy=1,                       
                        IsMigrationData=true
                    },
                    new Faq
                    {
                        FaqId=2,
                        Title="What will be requirements to take a Exam?",
                        Description="Nothing at all! You just need an active Email.",                                             
                        IsActive=true,
                        DateAdded=DateTime.Now,                       
                        AddedBy=1,                       
                        IsMigrationData=true
                    });
            });

            modelBuilder.Entity<SiteSettings>(b=>{
                b.HasKey(e=>e.SiteSettingsId);  
                b.Property(b=>b.SiteSettingsId).HasIdentityOptions(startValue:2);              
                b.HasData(
                    new SiteSettings
                    {
                        SiteSettingsId=1,
                        SiteTitle="Assess Hour",
                        WelComeMessage="Log in to get started.",                       
                        CopyRightText="© 2022 Assess Hour | All rights reserved",
                        DefaultEmail="",
                        Password="",
                        Host="smtp.gmail.com",
                        Port=587, 
                        Version=1,
                        LogoPath="",
                        FaviconPath="", 
                        AppBarColor="#363636",
                        HeaderColor="#F5F5F5",
                        FooterColor="#FFFFFF", 
                        BodyColor="#F9F9F9",
                        AllowWelcomeEmail=true,
                        AllowFaq=true,
                        AllowRightClick=true,
                        EndExam=true,
                        LogoOnExamPage=true,
                        PaidRegistration=true,
                        RegistrationPrice=0,
                        Currency="usd",
                        CurrencySymbol="$",
                        StripePubKey="",
                        StripeSecretKey="",
                        HomeHeader1="Taking an assessment is always difficult!",
                        HomeDetail1="We believe that scientific assessment activities help create a fair and merit-based society. This is why we develop culturally appropriate assessments and tests for you.Measure your candidates skill on a way that is secure and robust.Then what are you waiting for?",
                        HomeHeader2="Online Assessment Platform for educational institutions, employers, training providers and professional bodies",
                        HomeDetail2="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        HomeBox1Header="Educators",
                        HomeBox1Detail="We help educators to measure & develop employability skills to ensure success in education, careers, and everyday life.",
                        HomeBox2Header="Employers",
                        HomeBox2Detail="Our online testing platform helps organisations determine whether their new and existing staff have the modern skills.",
                        HomeBox3Header="Training Providers",
                        HomeBox3Detail="Our online tests and assessments make it easier for training providers to recognize achievement.",
                        HomeBox4Header="Professional Bodies",
                        HomeBox4Detail="We enable associations to deliver reliable online assessments and successful certification programmes.",
                        Feature1Header="Experience a new horizon",
                        Feature1Detail="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        Feature2Header="Flexible question settings",
                        Feature2Detail="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        Feature3Header="justify your candidates",
                        Feature3Detail="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        Feature4Header="Analyse your candidates",
                        Feature4Detail="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        RegistrationText="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        ContactUsText="Lorem ipsum dolor sit amet consectetur adipisicing elit. Iste explicabo commodi quisquam asperiores dolore ad enim provident veniam perferendis voluptate, perspiciatis. ",
                        ContactUsTelephone="+xx (xx) xxxxx-xxxx",
                        ContactUsEmail="email@email.com",
                        FooterText="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt.",
                        FooterFacebook="",
                        FooterTwitter="",
                        FooterLinkedin="",
                        FooterInstagram="",
                        ForgetPasswordEmailSubject="Forget Password",
                        ForgetPasswordEmailHeader="Forget Password Header",
                        ForgetPasswordEmailBody="Forget Password Body",
                        WelcomeEmailSubject="Welcome",
                        WelcomeEmailHeader="Welcome Header",
                        WelcomeEmailBody="Welcome Body",
                        InvitationEmailSubject="Invitation",
                        InvitationEmailHeader="Invitation Header",
                        InvitationEmailBody="Invitation Body",
                        ReportEmailSubject="N/A",
                        ReportEmailHeader="Report Header",
                        ReportEmailBody="N/A",
                        IsActive=true,
                        DateAdded=DateTime.Now,                       
                        AddedBy=1,                       
                        IsMigrationData=true
                    });
            });
        }
    }
}