using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizplusApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AppMenus",
                columns: table => new
                {
                    AppMenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MenuTitle = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IconClass = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsMigrationData = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMenus", x => x.AppMenuId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BillingPayments",
                columns: table => new
                {
                    BillingPaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Interval = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AssessmentCount = table.Column<int>(type: "int", nullable: false),
                    QuestionPerAssessmentCount = table.Column<int>(type: "int", nullable: false),
                    ResponsePerAssessmentCount = table.Column<int>(type: "int", nullable: false),
                    StripeSessionId = table.Column<string>(type: "varchar(800)", maxLength: 800, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserEmail = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TransactionEmail = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentMode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TransactionDetail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddedBy = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingPayments", x => x.BillingPaymentId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BillingPlans",
                columns: table => new
                {
                    BillingPlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Interval = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AssessmentCount = table.Column<int>(type: "int", nullable: false),
                    QuestionPerAssessmentCount = table.Column<int>(type: "int", nullable: false),
                    ResponsePerAssessmentCount = table.Column<int>(type: "int", nullable: false),
                    AdditionalText = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsMigrationData = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingPlans", x => x.BillingPlanId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CertificateTemplates",
                columns: table => new
                {
                    CertificateTemplateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Heading = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MainText = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PublishDate = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TopLeftImagePath = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TopRightImagePath = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BottomMiddleImagePath = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BackgroundImagePath = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BackgroundColor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LeftSignatureText = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LeftSignatureImagePath = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RightSignatureText = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RightSignatureImagePath = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsMigrationData = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificateTemplates", x => x.CertificateTemplateId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Message = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Faqs",
                columns: table => new
                {
                    FaqId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsMigrationData = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faqs", x => x.FaqId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    InstructionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuizTopicId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsMigrationData = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.InstructionId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LogHistories",
                columns: table => new
                {
                    LogHistoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LogCode = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LogDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    LogInTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LogOutTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Ip = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Browser = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BrowserVersion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Platform = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogHistories", x => x.LogHistoryId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MenuMappings",
                columns: table => new
                {
                    MenuMappingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserRoleId = table.Column<int>(type: "int", nullable: false),
                    AppMenuId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsMigrationData = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuMappings", x => x.MenuMappingId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuestionCategories",
                columns: table => new
                {
                    QuestionCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuestionCategoryName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsMigrationData = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionCategories", x => x.QuestionCategoryId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuestionLavels",
                columns: table => new
                {
                    QuestionLavelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuestionLavelName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionLavels", x => x.QuestionLavelId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuestionTypes",
                columns: table => new
                {
                    QuestionTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuestionTypeName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTypes", x => x.QuestionTypeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuizMarkOptions",
                columns: table => new
                {
                    QuizMarkOptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuizMarkOptionName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizMarkOptions", x => x.QuizMarkOptionId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuizParticipantOptions",
                columns: table => new
                {
                    QuizParticipantOptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuizParticipantOptionName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizParticipantOptions", x => x.QuizParticipantOptionId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuizParticipants",
                columns: table => new
                {
                    QuizParticipantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuizTopicId = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizParticipants", x => x.QuizParticipantId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuizPayments",
                columns: table => new
                {
                    QuizPaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuizTopicId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Currency = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SessionId = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddedBy = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizPayments", x => x.QuizPaymentId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuizQuestions",
                columns: table => new
                {
                    QuizQuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuizTopicId = table.Column<int>(type: "int", nullable: false),
                    QuestionDetail = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SerialNo = table.Column<int>(type: "int", nullable: false),
                    PerQuestionMark = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    QuestionTypeId = table.Column<int>(type: "int", nullable: false),
                    QuestionLavelId = table.Column<int>(type: "int", nullable: false),
                    QuestionCategoryId = table.Column<int>(type: "int", nullable: false),
                    OptionA = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OptionB = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OptionC = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OptionD = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OptionE = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CorrectOption = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AnswerExplanation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagePath = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VideoPath = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsCodeSnippet = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsMigrationData = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestions", x => x.QuizQuestionId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuizResponseDetails",
                columns: table => new
                {
                    QuizResponseDetailId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuizResponseInitialId = table.Column<int>(type: "int", nullable: false),
                    QuizQuestionId = table.Column<int>(type: "int", nullable: false),
                    QuestionDetail = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserAnswer = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAnswerSkipped = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CorrectAnswer = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AnswerExplanation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuestionMark = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    UserObtainedQuestionMark = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ImagePath = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VideoPath = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsExamined = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsMigrationData = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizResponseDetails", x => x.QuizResponseDetailId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuizResponseInitials",
                columns: table => new
                {
                    QuizResponseInitialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AttemptCount = table.Column<int>(type: "int", nullable: false),
                    IsExamined = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    QuizTopicId = table.Column<int>(type: "int", nullable: false),
                    QuizTitle = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuizMark = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    QuizPassMarks = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    UserObtainedQuizMark = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    QuizTime = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TimeTaken = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsMigrationData = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizResponseInitials", x => x.QuizResponseInitialId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuizTopics",
                columns: table => new
                {
                    QuizTopicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuizTitle = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuizTime = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    QuizTotalMarks = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    QuizPassMarks = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    QuizMarkOptionId = table.Column<int>(type: "int", nullable: false),
                    QuizParticipantOptionId = table.Column<int>(type: "int", nullable: false),
                    CertificateTemplateId = table.Column<int>(type: "int", nullable: true),
                    AllowMultipleInputByUser = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AllowMultipleAnswer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AllowMultipleAttempt = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AllowCorrectOption = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AllowQuizStop = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AllowQuizSkip = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AllowQuestionSuffle = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    QuizscheduleStartTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    QuizscheduleEndTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    QuizPrice = table.Column<int>(type: "int", nullable: false),
                    IsRunning = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Categories = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsMigrationData = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizTopics", x => x.QuizTopicId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ReportTypes",
                columns: table => new
                {
                    ReportTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReportTypeName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTypes", x => x.ReportTypeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SiteSettings",
                columns: table => new
                {
                    SiteSettingsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SiteTitle = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WelComeMessage = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CopyRightText = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LogoPath = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FaviconPath = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AppBarColor = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HeaderColor = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FooterColor = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BodyColor = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AllowWelcomeEmail = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AllowFaq = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AllowRightClick = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EndExam = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LogoOnExamPage = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PaidRegistration = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RegistrationPrice = table.Column<int>(type: "int", nullable: true),
                    Currency = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurrencySymbol = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StripePubKey = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StripeSecretKey = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClientUrl = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DefaultEmail = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DisplayName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Host = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Port = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    HomeHeader1 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HomeDetail1 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HomeHeader2 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HomeDetail2 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HomeBox1Header = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HomeBox1Detail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HomeBox2Header = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HomeBox2Detail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HomeBox3Header = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HomeBox3Detail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HomeBox4Header = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HomeBox4Detail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Feature1Header = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Feature1Detail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Feature2Header = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Feature2Detail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Feature3Header = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Feature3Detail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Feature4Header = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Feature4Detail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RegistrationText = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactUsText = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactUsTelephone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactUsEmail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FooterText = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FooterFacebook = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FooterTwitter = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FooterLinkedin = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FooterInstagram = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ForgetPasswordEmailSubject = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ForgetPasswordEmailHeader = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ForgetPasswordEmailBody = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WelcomeEmailSubject = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WelcomeEmailHeader = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WelcomeEmailBody = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvitationEmailSubject = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvitationEmailHeader = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvitationEmailBody = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReportEmailSubject = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReportEmailHeader = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReportEmailBody = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsMigrationData = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.SiteSettingsId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DisplayName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleDesc = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsMigrationData = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserRoleId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Mobile = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ImagePath = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StripeSessionId = table.Column<string>(type: "varchar(800)", maxLength: 800, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BillingPlanId = table.Column<int>(type: "int", nullable: true),
                    PaymentId = table.Column<int>(type: "int", nullable: true),
                    PaymentMode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TransactionDetail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsMigrationData = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AppMenus",
                columns: new[] { "AppMenuId", "AddedBy", "DateAdded", "IconClass", "IsActive", "IsMigrationData", "LastUpdatedBy", "LastUpdatedDate", "MenuTitle", "SortOrder", "Url" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9499), "dashboard", true, true, null, null, "Dashboard", 1, "/dashboard" },
                    { 2, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9505), "menu_open", true, true, null, null, "Menus", 2, "/menu/menus" },
                    { 3, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9507), "supervised_user_circle", true, true, null, null, "Roles", 3, "/user/roles" },
                    { 4, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9510), "mdi-account-multiple", true, true, null, null, "Users", 4, "/user/users" },
                    { 5, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9512), "category", true, true, null, null, "Category", 5, "/question/category" },
                    { 6, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9513), "emoji_objects", true, true, null, null, "Assessments", 6, "/quiz/topics" },
                    { 7, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9515), "help_center", true, true, null, null, "Questions", 7, "/question/quizes" },
                    { 8, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9516), "description", true, true, null, null, "Reports", 8, "/report/students" },
                    { 9, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9518), "card_giftcard", true, true, null, null, "CertificateTemplate", 9, "/report/certificates" },
                    { 10, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9519), "settings", true, true, null, null, "AppSettings", 16, "/settings/appSettings" },
                    { 11, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9521), "description", true, true, null, null, "ExamineAndReports", 10, "/report/admin" },
                    { 12, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9522), "analytics", true, true, null, null, "Analytics", 11, "/report/analysis" },
                    { 13, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9524), "help_center", true, true, null, null, "FAQ", 12, "/settings/faq" },
                    { 14, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9525), "contact_support", true, true, null, null, "Contacts", 13, "/settings/contacts" },
                    { 15, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9527), "payments", true, true, null, null, "Payments", 14, "/settings/payments" },
                    { 16, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9528), "monetization_on", true, true, null, null, "Plans", 15, "/settings/plans" }
                });

            migrationBuilder.InsertData(
                table: "Faqs",
                columns: new[] { "FaqId", "AddedBy", "DateAdded", "Description", "IsActive", "IsMigrationData", "LastUpdatedBy", "LastUpdatedDate", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 12, 28, 14, 25, 2, 123, DateTimeKind.Local).AddTicks(2094), "Assess Hour will fulfill your need to take online Assessments,Exams,Quizes as well as surveys.", true, true, null, null, "What are the purposes of this app?" },
                    { 2, 1, new DateTime(2022, 12, 28, 14, 25, 2, 123, DateTimeKind.Local).AddTicks(2100), "Nothing at all! You just need an active Email.", true, true, null, null, "What will be requirements to take a Exam?" }
                });

            migrationBuilder.InsertData(
                table: "MenuMappings",
                columns: new[] { "MenuMappingId", "AddedBy", "AppMenuId", "DateAdded", "IsActive", "IsMigrationData", "UserRoleId" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9949), true, true, 3 },
                    { 2, 1, 2, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9954), true, true, 3 },
                    { 3, 1, 3, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9957), true, true, 3 },
                    { 4, 1, 4, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9959), true, true, 3 },
                    { 10, 1, 10, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9961), true, true, 3 },
                    { 11, 1, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9969), true, true, 2 },
                    { 12, 1, 8, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9971), true, true, 2 },
                    { 13, 1, 12, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9972), true, true, 2 },
                    { 14, 1, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9974), true, true, 1 },
                    { 15, 1, 4, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9976), true, true, 1 },
                    { 16, 1, 5, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9978), true, true, 1 },
                    { 17, 1, 6, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9979), true, true, 1 },
                    { 18, 1, 7, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9981), true, true, 1 },
                    { 19, 1, 11, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9983), true, true, 1 },
                    { 20, 1, 9, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9985), true, true, 1 },
                    { 21, 1, 13, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9963), true, true, 3 },
                    { 22, 1, 14, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9964), true, true, 3 },
                    { 23, 1, 15, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9966), true, true, 3 },
                    { 24, 1, 16, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9968), true, true, 3 },
                    { 25, 1, 15, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(9986), true, true, 1 }
                });

            migrationBuilder.InsertData(
                table: "QuestionLavels",
                columns: new[] { "QuestionLavelId", "QuestionLavelName" },
                values: new object[,]
                {
                    { 1, "Easy" },
                    { 2, "Medium" },
                    { 3, "Hard" }
                });

            migrationBuilder.InsertData(
                table: "QuestionTypes",
                columns: new[] { "QuestionTypeId", "QuestionTypeName" },
                values: new object[,]
                {
                    { 1, "MCQ" },
                    { 2, "Descriptive" }
                });

            migrationBuilder.InsertData(
                table: "QuizMarkOptions",
                columns: new[] { "QuizMarkOptionId", "QuizMarkOptionName" },
                values: new object[,]
                {
                    { 1, "Equal distribution" },
                    { 2, "No marks(Survey)" },
                    { 3, "Question wise set" }
                });

            migrationBuilder.InsertData(
                table: "QuizParticipantOptions",
                columns: new[] { "QuizParticipantOptionId", "QuizParticipantOptionName" },
                values: new object[,]
                {
                    { 1, "All registered candidates" },
                    { 2, "Custom Input" }
                });

            migrationBuilder.InsertData(
                table: "ReportTypes",
                columns: new[] { "ReportTypeId", "ReportTypeName" },
                values: new object[,]
                {
                    { 1, "Pending Examine" },
                    { 2, "Reports" }
                });

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "SiteSettingsId", "AddedBy", "AllowFaq", "AllowRightClick", "AllowWelcomeEmail", "AppBarColor", "BodyColor", "ClientUrl", "ContactUsEmail", "ContactUsTelephone", "ContactUsText", "CopyRightText", "Currency", "CurrencySymbol", "DateAdded", "DefaultEmail", "DisplayName", "EndExam", "FaviconPath", "Feature1Detail", "Feature1Header", "Feature2Detail", "Feature2Header", "Feature3Detail", "Feature3Header", "Feature4Detail", "Feature4Header", "FooterColor", "FooterFacebook", "FooterInstagram", "FooterLinkedin", "FooterText", "FooterTwitter", "ForgetPasswordEmailBody", "ForgetPasswordEmailHeader", "ForgetPasswordEmailSubject", "HeaderColor", "HomeBox1Detail", "HomeBox1Header", "HomeBox2Detail", "HomeBox2Header", "HomeBox3Detail", "HomeBox3Header", "HomeBox4Detail", "HomeBox4Header", "HomeDetail1", "HomeDetail2", "HomeHeader1", "HomeHeader2", "Host", "InvitationEmailBody", "InvitationEmailHeader", "InvitationEmailSubject", "IsActive", "IsMigrationData", "LastUpdatedBy", "LastUpdatedDate", "LogoOnExamPage", "LogoPath", "PaidRegistration", "Password", "Port", "RegistrationPrice", "RegistrationText", "ReportEmailBody", "ReportEmailHeader", "ReportEmailSubject", "SiteTitle", "StripePubKey", "StripeSecretKey", "Version", "WelComeMessage", "WelcomeEmailBody", "WelcomeEmailHeader", "WelcomeEmailSubject" },
                values: new object[] { 1, 1, true, true, true, "#363636", "#F9F9F9", null, "email@email.com", "+xx (xx) xxxxx-xxxx", "Lorem ipsum dolor sit amet consectetur adipisicing elit. Iste explicabo commodi quisquam asperiores dolore ad enim provident veniam perferendis voluptate, perspiciatis. ", "© 2022 Assess Hour | All rights reserved", "usd", "$", new DateTime(2022, 12, 28, 14, 25, 2, 123, DateTimeKind.Local).AddTicks(2477), "", null, true, "", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", "Experience a new horizon", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", "Flexible question settings", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", "justify your candidates", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", "Analyse your candidates", "#FFFFFF", "", "", "", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt.", "", "Forget Password Body", "Forget Password Header", "Forget Password", "#F5F5F5", "We help educators to measure & develop employability skills to ensure success in education, careers, and everyday life.", "Educators", "Our online testing platform helps organisations determine whether their new and existing staff have the modern skills.", "Employers", "Our online tests and assessments make it easier for training providers to recognize achievement.", "Training Providers", "We enable associations to deliver reliable online assessments and successful certification programmes.", "Professional Bodies", "We believe that scientific assessment activities help create a fair and merit-based society. This is why we develop culturally appropriate assessments and tests for you.Measure your candidates skill on a way that is secure and robust.Then what are you waiting for?", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", "Taking an assessment is always difficult!", "Online Assessment Platform for educational institutions, employers, training providers and professional bodies", "smtp.gmail.com", "Invitation Body", "Invitation Header", "Invitation", true, true, null, null, true, "", true, "", 587, 0, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", "N/A", "Report Header", "N/A", "Assess Hour", "", "", 1, "Log in to get started.", "Welcome Body", "Welcome Header", "Welcome" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserRoleId", "AddedBy", "DateAdded", "DisplayName", "IsActive", "IsMigrationData", "LastUpdatedBy", "LastUpdatedDate", "RoleDesc", "RoleName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(8241), "Admin", true, true, null, null, "Application Admin", "Admin" },
                    { 2, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(8273), "Candidate", true, true, null, null, "All Students", "Student" },
                    { 3, 1, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(8278), "Super Admin", true, true, null, null, "Application Super Admin", "SuperAdmin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AddedBy", "Address", "BillingPlanId", "DateAdded", "DateOfBirth", "Email", "FullName", "ImagePath", "IsActive", "IsMigrationData", "LastUpdatedBy", "LastUpdatedDate", "Mobile", "Password", "PaymentId", "PaymentMode", "StripeSessionId", "TransactionDetail", "UserRoleId" },
                values: new object[] { 1, 1, null, null, new DateTime(2022, 12, 28, 14, 25, 2, 122, DateTimeKind.Local).AddTicks(8884), null, "superAdmin@assessHour.com", "John Doe", "", true, true, null, null, null, "abcd1234", null, null, null, null, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppMenus");

            migrationBuilder.DropTable(
                name: "BillingPayments");

            migrationBuilder.DropTable(
                name: "BillingPlans");

            migrationBuilder.DropTable(
                name: "CertificateTemplates");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Faqs");

            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "LogHistories");

            migrationBuilder.DropTable(
                name: "MenuMappings");

            migrationBuilder.DropTable(
                name: "QuestionCategories");

            migrationBuilder.DropTable(
                name: "QuestionLavels");

            migrationBuilder.DropTable(
                name: "QuestionTypes");

            migrationBuilder.DropTable(
                name: "QuizMarkOptions");

            migrationBuilder.DropTable(
                name: "QuizParticipantOptions");

            migrationBuilder.DropTable(
                name: "QuizParticipants");

            migrationBuilder.DropTable(
                name: "QuizPayments");

            migrationBuilder.DropTable(
                name: "QuizQuestions");

            migrationBuilder.DropTable(
                name: "QuizResponseDetails");

            migrationBuilder.DropTable(
                name: "QuizResponseInitials");

            migrationBuilder.DropTable(
                name: "QuizTopics");

            migrationBuilder.DropTable(
                name: "ReportTypes");

            migrationBuilder.DropTable(
                name: "SiteSettings");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
