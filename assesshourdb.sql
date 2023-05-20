/*
 Navicat Premium Data Transfer

 Source Server         : LocalMysql
 Source Server Type    : MySQL
 Source Server Version : 80032 (8.0.32)
 Source Host           : localhost:3306
 Source Schema         : assesshourdb

 Target Server Type    : MySQL
 Target Server Version : 80032 (8.0.32)
 File Encoding         : 65001

 Date: 14/02/2023 13:29:26
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for __efmigrationshistory
-- ----------------------------
DROP TABLE IF EXISTS `__efmigrationshistory`;
CREATE TABLE `__efmigrationshistory`  (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`MigrationId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of __efmigrationshistory
-- ----------------------------
INSERT INTO `__efmigrationshistory` VALUES ('20221228082502_initial', '6.0.0');

-- ----------------------------
-- Table structure for appmenus
-- ----------------------------
DROP TABLE IF EXISTS `appmenus`;
CREATE TABLE `appmenus`  (
  `AppMenuId` int NOT NULL AUTO_INCREMENT,
  `MenuTitle` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Url` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `SortOrder` int NOT NULL,
  `IconClass` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsMigrationData` tinyint(1) NOT NULL,
  `DateAdded` datetime(6) NOT NULL,
  `AddedBy` int NOT NULL,
  `LastUpdatedDate` datetime(6) NULL DEFAULT NULL,
  `LastUpdatedBy` int NULL DEFAULT NULL,
  PRIMARY KEY (`AppMenuId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 17 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of appmenus
-- ----------------------------
INSERT INTO `appmenus` VALUES (1, 'Dashboard', '/dashboard', 1, 'dashboard', 1, 1, '2022-12-28 14:25:02.000000', 1, NULL, NULL);
INSERT INTO `appmenus` VALUES (2, 'Menus', '/menu/menus', 2, 'menu_open', 1, 1, '2022-12-28 14:25:02.000000', 1, NULL, NULL);
INSERT INTO `appmenus` VALUES (3, 'Roles', '/user/roles', 3, 'supervised_user_circle', 1, 1, '2022-12-28 14:25:02.000000', 1, NULL, NULL);
INSERT INTO `appmenus` VALUES (4, 'Users', '/user/users', 4, 'mdi-account-multiple', 1, 1, '2022-12-28 14:25:02.000000', 1, NULL, NULL);
INSERT INTO `appmenus` VALUES (5, 'Category', '/question/category', 5, 'category', 1, 1, '2022-12-28 14:25:02.000000', 1, NULL, NULL);
INSERT INTO `appmenus` VALUES (6, 'Assessments', '/quiz/topics', 6, 'emoji_objects', 1, 1, '2022-12-28 14:25:02.000000', 1, NULL, NULL);
INSERT INTO `appmenus` VALUES (7, 'Questions', '/question/quizes', 7, 'help_center', 1, 1, '2022-12-28 14:25:02.000000', 1, NULL, NULL);
INSERT INTO `appmenus` VALUES (8, 'Reports', '/report/students', 8, 'description', 1, 1, '2022-12-28 14:25:02.000000', 1, NULL, NULL);
INSERT INTO `appmenus` VALUES (9, 'CertificateTemplate', '/report/certificates', 9, 'card_giftcard', 1, 1, '2022-12-28 14:25:02.000000', 1, NULL, NULL);
INSERT INTO `appmenus` VALUES (10, 'AppSettings', '/settings/appSettings', 16, 'settings', 1, 1, '2022-12-28 14:25:02.000000', 1, NULL, NULL);
INSERT INTO `appmenus` VALUES (11, 'ExamineAndReports', '/report/admin', 10, 'description', 1, 1, '2022-12-28 14:25:02.000000', 1, NULL, NULL);
INSERT INTO `appmenus` VALUES (12, 'Analytics', '/report/analysis', 11, 'analytics', 1, 1, '2022-12-28 14:25:02.000000', 1, NULL, NULL);
INSERT INTO `appmenus` VALUES (13, 'FAQ', '/settings/faq', 12, 'help_center', 1, 1, '2022-12-28 14:25:02.000000', 1, NULL, NULL);
INSERT INTO `appmenus` VALUES (14, 'Contacts', '/settings/contacts', 13, 'contact_support', 1, 1, '2022-12-28 14:25:02.000000', 1, NULL, NULL);
INSERT INTO `appmenus` VALUES (15, 'Payments', '/settings/payments', 14, 'payments', 1, 1, '2022-12-28 14:25:02.000000', 1, NULL, NULL);
INSERT INTO `appmenus` VALUES (16, 'Plans', '/settings/plans', 15, 'monetization_on', 1, 1, '2022-12-28 14:25:02.000000', 1, NULL, NULL);

-- ----------------------------
-- Table structure for billingpayments
-- ----------------------------
DROP TABLE IF EXISTS `billingpayments`;
CREATE TABLE `billingpayments`  (
  `BillingPaymentId` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Price` int NOT NULL,
  `Interval` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `AssessmentCount` int NOT NULL,
  `QuestionPerAssessmentCount` int NOT NULL,
  `ResponsePerAssessmentCount` int NOT NULL,
  `StripeSessionId` varchar(800) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `UserEmail` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `TransactionEmail` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `PaymentMode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `TransactionDetail` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `AddedBy` int NULL DEFAULT NULL,
  `StartDate` datetime(6) NOT NULL,
  `EndDate` datetime(6) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `DateAdded` datetime(6) NOT NULL,
  PRIMARY KEY (`BillingPaymentId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of billingpayments
-- ----------------------------

-- ----------------------------
-- Table structure for billingplans
-- ----------------------------
DROP TABLE IF EXISTS `billingplans`;
CREATE TABLE `billingplans`  (
  `BillingPlanId` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Price` int NOT NULL,
  `Interval` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `AssessmentCount` int NOT NULL,
  `QuestionPerAssessmentCount` int NOT NULL,
  `ResponsePerAssessmentCount` int NOT NULL,
  `AdditionalText` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsMigrationData` tinyint(1) NOT NULL,
  `AddedBy` int NOT NULL,
  `DateAdded` datetime(6) NOT NULL,
  `LastUpdatedDate` datetime(6) NULL DEFAULT NULL,
  `LastUpdatedBy` int NULL DEFAULT NULL,
  PRIMARY KEY (`BillingPlanId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of billingplans
-- ----------------------------

-- ----------------------------
-- Table structure for certificatetemplates
-- ----------------------------
DROP TABLE IF EXISTS `certificatetemplates`;
CREATE TABLE `certificatetemplates`  (
  `CertificateTemplateId` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Heading` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `MainText` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `PublishDate` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `TopLeftImagePath` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `TopRightImagePath` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `BottomMiddleImagePath` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `BackgroundImagePath` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `BackgroundColor` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `LeftSignatureText` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `LeftSignatureImagePath` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `RightSignatureText` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `RightSignatureImagePath` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsMigrationData` tinyint(1) NOT NULL,
  `AddedBy` int NOT NULL,
  `DateAdded` datetime(6) NOT NULL,
  `LastUpdatedDate` datetime(6) NULL DEFAULT NULL,
  `LastUpdatedBy` int NULL DEFAULT NULL,
  PRIMARY KEY (`CertificateTemplateId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of certificatetemplates
-- ----------------------------

-- ----------------------------
-- Table structure for contacts
-- ----------------------------
DROP TABLE IF EXISTS `contacts`;
CREATE TABLE `contacts`  (
  `ContactId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Email` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Message` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `DateAdded` datetime(6) NOT NULL,
  PRIMARY KEY (`ContactId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of contacts
-- ----------------------------

-- ----------------------------
-- Table structure for faqs
-- ----------------------------
DROP TABLE IF EXISTS `faqs`;
CREATE TABLE `faqs`  (
  `FaqId` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsMigrationData` tinyint(1) NOT NULL,
  `AddedBy` int NOT NULL,
  `DateAdded` datetime(6) NOT NULL,
  `LastUpdatedDate` datetime(6) NULL DEFAULT NULL,
  `LastUpdatedBy` int NULL DEFAULT NULL,
  PRIMARY KEY (`FaqId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of faqs
-- ----------------------------
INSERT INTO `faqs` VALUES (1, 'What are the purposes of this app?', 'Assess Hour will fulfill your need to take online Assessments,Exams,Quizes as well as surveys.', 1, 1, 1, '2022-12-28 14:25:02.000000', NULL, NULL);
INSERT INTO `faqs` VALUES (2, 'What will be requirements to take a Exam?', 'Nothing at all! You just need an active Email.', 1, 1, 1, '2022-12-28 14:25:02.000000', NULL, NULL);

-- ----------------------------
-- Table structure for instructions
-- ----------------------------
DROP TABLE IF EXISTS `instructions`;
CREATE TABLE `instructions`  (
  `InstructionId` int NOT NULL AUTO_INCREMENT,
  `QuizTopicId` int NOT NULL,
  `Description` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsMigrationData` tinyint(1) NOT NULL,
  `AddedBy` int NOT NULL,
  `DateAdded` datetime(6) NOT NULL,
  `LastUpdatedDate` datetime(6) NULL DEFAULT NULL,
  `LastUpdatedBy` int NULL DEFAULT NULL,
  PRIMARY KEY (`InstructionId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of instructions
-- ----------------------------

-- ----------------------------
-- Table structure for loghistories
-- ----------------------------
DROP TABLE IF EXISTS `loghistories`;
CREATE TABLE `loghistories`  (
  `LogHistoryId` bigint NOT NULL AUTO_INCREMENT,
  `LogCode` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `LogDate` datetime(6) NOT NULL,
  `UserId` int NOT NULL,
  `AdminId` int NULL DEFAULT NULL,
  `LogInTime` datetime(6) NOT NULL,
  `LogOutTime` datetime(6) NULL DEFAULT NULL,
  `Ip` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Browser` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `BrowserVersion` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Platform` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  PRIMARY KEY (`LogHistoryId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of loghistories
-- ----------------------------
INSERT INTO `loghistories` VALUES (1, 'f6b04ded-3e43-40b3-93fe-733065f08cf2', '2022-12-28 14:28:28.022651', 1, 1, '2022-12-28 14:28:28.022706', '2022-12-28 14:29:14.925445', '37.111.246.88', 'chrome', '108.0.0', 'Linux');
INSERT INTO `loghistories` VALUES (2, '150ff86a-0391-4c0b-8557-4e0e5c252d8a', '2022-12-28 14:31:21.777754', 1, 1, '2022-12-28 14:31:21.777759', NULL, '37.111.246.88', 'chrome', '108.0.0', 'Linux');

-- ----------------------------
-- Table structure for menumappings
-- ----------------------------
DROP TABLE IF EXISTS `menumappings`;
CREATE TABLE `menumappings`  (
  `MenuMappingId` int NOT NULL AUTO_INCREMENT,
  `UserRoleId` int NOT NULL,
  `AppMenuId` int NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsMigrationData` tinyint(1) NOT NULL,
  `DateAdded` datetime(6) NOT NULL,
  `AddedBy` int NOT NULL,
  PRIMARY KEY (`MenuMappingId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 26 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of menumappings
-- ----------------------------
INSERT INTO `menumappings` VALUES (1, 3, 1, 1, 1, '2022-12-28 14:25:02.000000', 1);
INSERT INTO `menumappings` VALUES (2, 3, 2, 1, 1, '2022-12-28 14:25:02.000000', 1);
INSERT INTO `menumappings` VALUES (3, 3, 3, 1, 1, '2022-12-28 14:25:02.000000', 1);
INSERT INTO `menumappings` VALUES (4, 3, 4, 1, 1, '2022-12-28 14:25:02.000000', 1);
INSERT INTO `menumappings` VALUES (10, 3, 10, 1, 1, '2022-12-28 14:25:02.000000', 1);
INSERT INTO `menumappings` VALUES (11, 2, 1, 1, 1, '2022-12-28 14:25:02.000000', 1);
INSERT INTO `menumappings` VALUES (12, 2, 8, 1, 1, '2022-12-28 14:25:02.000000', 1);
INSERT INTO `menumappings` VALUES (13, 2, 12, 1, 1, '2022-12-28 14:25:02.000000', 1);
INSERT INTO `menumappings` VALUES (14, 1, 1, 1, 1, '2022-12-28 14:25:02.000000', 1);
INSERT INTO `menumappings` VALUES (15, 1, 4, 1, 1, '2022-12-28 14:25:02.000000', 1);
INSERT INTO `menumappings` VALUES (16, 1, 5, 1, 1, '2022-12-28 14:25:02.000000', 1);
INSERT INTO `menumappings` VALUES (17, 1, 6, 1, 1, '2022-12-28 14:25:02.000000', 1);
INSERT INTO `menumappings` VALUES (18, 1, 7, 1, 1, '2022-12-28 14:25:02.000000', 1);
INSERT INTO `menumappings` VALUES (19, 1, 11, 1, 1, '2022-12-28 14:25:02.000000', 1);
INSERT INTO `menumappings` VALUES (20, 1, 9, 1, 1, '2022-12-28 14:25:02.000000', 1);
INSERT INTO `menumappings` VALUES (21, 3, 13, 1, 1, '2022-12-28 14:25:02.000000', 1);
INSERT INTO `menumappings` VALUES (22, 3, 14, 1, 1, '2022-12-28 14:25:02.000000', 1);
INSERT INTO `menumappings` VALUES (23, 3, 15, 1, 1, '2022-12-28 14:25:02.000000', 1);
INSERT INTO `menumappings` VALUES (24, 3, 16, 1, 1, '2022-12-28 14:25:02.000000', 1);
INSERT INTO `menumappings` VALUES (25, 1, 15, 1, 1, '2022-12-28 14:25:02.000000', 1);

-- ----------------------------
-- Table structure for questioncategories
-- ----------------------------
DROP TABLE IF EXISTS `questioncategories`;
CREATE TABLE `questioncategories`  (
  `QuestionCategoryId` int NOT NULL AUTO_INCREMENT,
  `QuestionCategoryName` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsMigrationData` tinyint(1) NOT NULL,
  `AddedBy` int NOT NULL,
  `DateAdded` datetime(6) NOT NULL,
  `LastUpdatedDate` datetime(6) NULL DEFAULT NULL,
  `LastUpdatedBy` int NULL DEFAULT NULL,
  PRIMARY KEY (`QuestionCategoryId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of questioncategories
-- ----------------------------

-- ----------------------------
-- Table structure for questionlavels
-- ----------------------------
DROP TABLE IF EXISTS `questionlavels`;
CREATE TABLE `questionlavels`  (
  `QuestionLavelId` int NOT NULL AUTO_INCREMENT,
  `QuestionLavelName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`QuestionLavelId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of questionlavels
-- ----------------------------
INSERT INTO `questionlavels` VALUES (1, 'Easy');
INSERT INTO `questionlavels` VALUES (2, 'Medium');
INSERT INTO `questionlavels` VALUES (3, 'Hard');

-- ----------------------------
-- Table structure for questiontypes
-- ----------------------------
DROP TABLE IF EXISTS `questiontypes`;
CREATE TABLE `questiontypes`  (
  `QuestionTypeId` int NOT NULL AUTO_INCREMENT,
  `QuestionTypeName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`QuestionTypeId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of questiontypes
-- ----------------------------
INSERT INTO `questiontypes` VALUES (1, 'MCQ');
INSERT INTO `questiontypes` VALUES (2, 'Descriptive');

-- ----------------------------
-- Table structure for quizmarkoptions
-- ----------------------------
DROP TABLE IF EXISTS `quizmarkoptions`;
CREATE TABLE `quizmarkoptions`  (
  `QuizMarkOptionId` int NOT NULL AUTO_INCREMENT,
  `QuizMarkOptionName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`QuizMarkOptionId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of quizmarkoptions
-- ----------------------------
INSERT INTO `quizmarkoptions` VALUES (1, 'Equal distribution');
INSERT INTO `quizmarkoptions` VALUES (2, 'No marks(Survey)');
INSERT INTO `quizmarkoptions` VALUES (3, 'Question wise set');

-- ----------------------------
-- Table structure for quizparticipantoptions
-- ----------------------------
DROP TABLE IF EXISTS `quizparticipantoptions`;
CREATE TABLE `quizparticipantoptions`  (
  `QuizParticipantOptionId` int NOT NULL AUTO_INCREMENT,
  `QuizParticipantOptionName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`QuizParticipantOptionId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of quizparticipantoptions
-- ----------------------------
INSERT INTO `quizparticipantoptions` VALUES (1, 'All registered candidates');
INSERT INTO `quizparticipantoptions` VALUES (2, 'Custom Input');

-- ----------------------------
-- Table structure for quizparticipants
-- ----------------------------
DROP TABLE IF EXISTS `quizparticipants`;
CREATE TABLE `quizparticipants`  (
  `QuizParticipantId` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `QuizTopicId` int NOT NULL,
  `AddedBy` int NOT NULL,
  PRIMARY KEY (`QuizParticipantId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of quizparticipants
-- ----------------------------
INSERT INTO `quizparticipants` VALUES (1, 'unsal@gmail.com', 1, 1);

-- ----------------------------
-- Table structure for quizpayments
-- ----------------------------
DROP TABLE IF EXISTS `quizpayments`;
CREATE TABLE `quizpayments`  (
  `QuizPaymentId` int NOT NULL AUTO_INCREMENT,
  `QuizTopicId` int NOT NULL,
  `Amount` decimal(5, 2) NOT NULL,
  `Email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Currency` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `SessionId` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `AddedBy` int NOT NULL,
  `AdminId` int NOT NULL,
  `DateAdded` datetime(6) NOT NULL,
  PRIMARY KEY (`QuizPaymentId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of quizpayments
-- ----------------------------

-- ----------------------------
-- Table structure for quizquestions
-- ----------------------------
DROP TABLE IF EXISTS `quizquestions`;
CREATE TABLE `quizquestions`  (
  `QuizQuestionId` int NOT NULL AUTO_INCREMENT,
  `QuizTopicId` int NOT NULL,
  `QuestionDetail` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `SerialNo` int NOT NULL,
  `PerQuestionMark` decimal(5, 2) NOT NULL,
  `QuestionTypeId` int NOT NULL,
  `QuestionLavelId` int NOT NULL,
  `QuestionCategoryId` int NOT NULL,
  `OptionA` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `OptionB` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `OptionC` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `OptionD` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `OptionE` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `CorrectOption` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `AnswerExplanation` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `ImagePath` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `VideoPath` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `IsCodeSnippet` tinyint(1) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsMigrationData` tinyint(1) NOT NULL,
  `AddedBy` int NOT NULL,
  `DateAdded` datetime(6) NOT NULL,
  `LastUpdatedDate` datetime(6) NULL DEFAULT NULL,
  `LastUpdatedBy` int NULL DEFAULT NULL,
  PRIMARY KEY (`QuizQuestionId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of quizquestions
-- ----------------------------

-- ----------------------------
-- Table structure for quizresponsedetails
-- ----------------------------
DROP TABLE IF EXISTS `quizresponsedetails`;
CREATE TABLE `quizresponsedetails`  (
  `QuizResponseDetailId` bigint NOT NULL AUTO_INCREMENT,
  `QuizResponseInitialId` int NOT NULL,
  `QuizQuestionId` int NOT NULL,
  `QuestionDetail` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `UserAnswer` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `IsAnswerSkipped` tinyint(1) NOT NULL,
  `CorrectAnswer` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `AnswerExplanation` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `QuestionMark` decimal(5, 2) NOT NULL,
  `UserObtainedQuestionMark` decimal(5, 2) NOT NULL,
  `ImagePath` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `VideoPath` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `IsExamined` tinyint(1) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsMigrationData` tinyint(1) NOT NULL,
  `AddedBy` int NOT NULL,
  `DateAdded` datetime(6) NOT NULL,
  `LastUpdatedDate` datetime(6) NULL DEFAULT NULL,
  `LastUpdatedBy` int NULL DEFAULT NULL,
  PRIMARY KEY (`QuizResponseDetailId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of quizresponsedetails
-- ----------------------------

-- ----------------------------
-- Table structure for quizresponseinitials
-- ----------------------------
DROP TABLE IF EXISTS `quizresponseinitials`;
CREATE TABLE `quizresponseinitials`  (
  `QuizResponseInitialId` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `Email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `AttemptCount` int NOT NULL,
  `IsExamined` tinyint(1) NOT NULL,
  `QuizTopicId` int NOT NULL,
  `QuizTitle` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `QuizMark` decimal(5, 2) NOT NULL,
  `QuizPassMarks` decimal(5, 2) NOT NULL,
  `UserObtainedQuizMark` decimal(5, 2) NOT NULL,
  `QuizTime` decimal(5, 2) NOT NULL,
  `TimeTaken` decimal(5, 2) NULL DEFAULT NULL,
  `StartTime` datetime(6) NOT NULL,
  `EndTime` datetime(6) NULL DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsMigrationData` tinyint(1) NOT NULL,
  `AddedBy` int NOT NULL,
  `DateAdded` datetime(6) NOT NULL,
  `LastUpdatedDate` datetime(6) NULL DEFAULT NULL,
  `LastUpdatedBy` int NULL DEFAULT NULL,
  PRIMARY KEY (`QuizResponseInitialId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of quizresponseinitials
-- ----------------------------

-- ----------------------------
-- Table structure for quiztopics
-- ----------------------------
DROP TABLE IF EXISTS `quiztopics`;
CREATE TABLE `quiztopics`  (
  `QuizTopicId` int NOT NULL AUTO_INCREMENT,
  `QuizTitle` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `QuizTime` decimal(5, 2) NOT NULL,
  `QuizTotalMarks` decimal(5, 2) NOT NULL,
  `QuizPassMarks` decimal(5, 2) NOT NULL,
  `QuizMarkOptionId` int NOT NULL,
  `QuizParticipantOptionId` int NOT NULL,
  `CertificateTemplateId` int NULL DEFAULT NULL,
  `AllowMultipleInputByUser` tinyint(1) NOT NULL,
  `AllowMultipleAnswer` tinyint(1) NOT NULL,
  `AllowMultipleAttempt` tinyint(1) NOT NULL,
  `AllowCorrectOption` tinyint(1) NOT NULL,
  `AllowQuizStop` tinyint(1) NOT NULL,
  `AllowQuizSkip` tinyint(1) NOT NULL,
  `AllowQuestionSuffle` tinyint(1) NOT NULL,
  `QuizscheduleStartTime` datetime(6) NULL DEFAULT NULL,
  `QuizscheduleEndTime` datetime(6) NULL DEFAULT NULL,
  `QuizPrice` int NOT NULL,
  `IsRunning` tinyint(1) NOT NULL,
  `Categories` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsMigrationData` tinyint(1) NOT NULL,
  `AddedBy` int NOT NULL,
  `DateAdded` datetime(6) NOT NULL,
  `LastUpdatedDate` datetime(6) NULL DEFAULT NULL,
  `LastUpdatedBy` int NULL DEFAULT NULL,
  PRIMARY KEY (`QuizTopicId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of quiztopics
-- ----------------------------

-- ----------------------------
-- Table structure for reporttypes
-- ----------------------------
DROP TABLE IF EXISTS `reporttypes`;
CREATE TABLE `reporttypes`  (
  `ReportTypeId` int NOT NULL AUTO_INCREMENT,
  `ReportTypeName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`ReportTypeId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of reporttypes
-- ----------------------------
INSERT INTO `reporttypes` VALUES (1, 'Pending Examine');
INSERT INTO `reporttypes` VALUES (2, 'Reports');

-- ----------------------------
-- Table structure for sitesettings
-- ----------------------------
DROP TABLE IF EXISTS `sitesettings`;
CREATE TABLE `sitesettings`  (
  `SiteSettingsId` int NOT NULL AUTO_INCREMENT,
  `SiteTitle` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `WelComeMessage` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `CopyRightText` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `LogoPath` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `FaviconPath` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `AppBarColor` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `HeaderColor` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `FooterColor` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `BodyColor` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `AllowWelcomeEmail` tinyint(1) NOT NULL,
  `AllowFaq` tinyint(1) NOT NULL,
  `AllowRightClick` tinyint(1) NOT NULL,
  `EndExam` tinyint(1) NOT NULL,
  `LogoOnExamPage` tinyint(1) NOT NULL,
  `PaidRegistration` tinyint(1) NOT NULL,
  `RegistrationPrice` int NULL DEFAULT NULL,
  `Currency` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `CurrencySymbol` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `StripePubKey` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `StripeSecretKey` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `ClientUrl` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `DefaultEmail` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `DisplayName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Password` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Host` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Port` int NOT NULL,
  `Version` int NOT NULL,
  `HomeHeader1` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `HomeDetail1` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `HomeHeader2` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `HomeDetail2` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `HomeBox1Header` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `HomeBox1Detail` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `HomeBox2Header` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `HomeBox2Detail` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `HomeBox3Header` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `HomeBox3Detail` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `HomeBox4Header` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `HomeBox4Detail` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `Feature1Header` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `Feature1Detail` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `Feature2Header` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `Feature2Detail` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `Feature3Header` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `Feature3Detail` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `Feature4Header` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `Feature4Detail` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `RegistrationText` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `ContactUsText` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `ContactUsTelephone` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `ContactUsEmail` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `FooterText` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `FooterFacebook` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `FooterTwitter` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `FooterLinkedin` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `FooterInstagram` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `ForgetPasswordEmailSubject` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `ForgetPasswordEmailHeader` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `ForgetPasswordEmailBody` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `WelcomeEmailSubject` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `WelcomeEmailHeader` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `WelcomeEmailBody` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `InvitationEmailSubject` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `InvitationEmailHeader` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `InvitationEmailBody` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `ReportEmailSubject` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `ReportEmailHeader` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `ReportEmailBody` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsMigrationData` tinyint(1) NOT NULL,
  `AddedBy` int NOT NULL,
  `DateAdded` datetime(6) NOT NULL,
  `LastUpdatedDate` datetime(6) NULL DEFAULT NULL,
  `LastUpdatedBy` int NULL DEFAULT NULL,
  PRIMARY KEY (`SiteSettingsId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sitesettings
-- ----------------------------
INSERT INTO `sitesettings` VALUES (1, 'Assess Hour', 'Log in to get started.', '© 2022 Assess Hour | All rights reserved', '', '', '#363636', '#F5F5F5', '#FFFFFF', '#F9F9F9', 1, 1, 1, 1, 1, 1, 0, 'usd', '$', '', '', 'http://localhost:8080', '', NULL, '', 'smtp.gmail.com', 587, 1, 'Taking an assessment is always difficult!', 'We believe that scientific assessment activities help create a fair and merit-based society. This is why we develop culturally appropriate assessments and tests for you.Measure your candidates skill on a way that is secure and robust.Then what are you waiting for?', 'Online Assessment Platform for educational institutions, employers, training providers and professional bodies', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 'Educators', 'We help educators to measure & develop employability skills to ensure success in education, careers, and everyday life.', 'Employers', 'Our online testing platform helps organisations determine whether their new and existing staff have the modern skills.', 'Training Providers', 'Our online tests and assessments make it easier for training providers to recognize achievement.', 'Professional Bodies', 'We enable associations to deliver reliable online assessments and successful certification programmes.', 'Experience a new horizon', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 'Flexible question settings', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 'justify your candidates', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 'Analyse your candidates', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Iste explicabo commodi quisquam asperiores dolore ad enim provident veniam perferendis voluptate, perspiciatis. ', '+xx (xx) xxxxx-xxxx', 'email@email.com', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt.', '', '', '', '', 'Forget Password', 'Forget Password Header', 'Forget Password Body', 'Welcome', 'Welcome Header', 'Welcome Body', 'Invitation', 'Invitation Header', 'Invitation Body', 'N/A', 'Report Header', 'N/A', 1, 1, 1, '2022-12-28 14:25:02.000000', NULL, NULL);

-- ----------------------------
-- Table structure for userroles
-- ----------------------------
DROP TABLE IF EXISTS `userroles`;
CREATE TABLE `userroles`  (
  `UserRoleId` int NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `DisplayName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `RoleDesc` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsMigrationData` tinyint(1) NOT NULL,
  `AddedBy` int NOT NULL,
  `DateAdded` datetime(6) NOT NULL,
  `LastUpdatedDate` datetime(6) NULL DEFAULT NULL,
  `LastUpdatedBy` int NULL DEFAULT NULL,
  PRIMARY KEY (`UserRoleId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of userroles
-- ----------------------------
INSERT INTO `userroles` VALUES (1, 'Admin', 'Admin', 'Application Admin', 1, 1, 1, '2022-12-28 14:25:02.000000', NULL, NULL);
INSERT INTO `userroles` VALUES (2, 'Student', 'Candidate', 'All Students', 1, 1, 1, '2022-12-28 14:25:02.000000', NULL, NULL);
INSERT INTO `userroles` VALUES (3, 'SuperAdmin', 'Super Admin', 'Application Super Admin', 1, 1, 1, '2022-12-28 14:25:02.000000', NULL, NULL);

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users`  (
  `UserId` int NOT NULL AUTO_INCREMENT,
  `UserRoleId` int NOT NULL,
  `FullName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Mobile` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Password` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Address` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `DateOfBirth` datetime(6) NULL DEFAULT NULL,
  `ImagePath` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `StripeSessionId` varchar(800) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `BillingPlanId` int NULL DEFAULT NULL,
  `PaymentId` int NULL DEFAULT NULL,
  `PaymentMode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `TransactionDetail` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsMigrationData` tinyint(1) NOT NULL,
  `AddedBy` int NULL DEFAULT NULL,
  `DateAdded` datetime(6) NOT NULL,
  `LastUpdatedDate` datetime(6) NULL DEFAULT NULL,
  `LastUpdatedBy` int NULL DEFAULT NULL,
  `PositionCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `PositionName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `JobCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `JobName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `DeparmentCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `DeparmentName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `RegionCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `RegionName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  PRIMARY KEY (`UserId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of users
-- ----------------------------
INSERT INTO `users` VALUES (1, 3, 'John Doe', NULL, 'superAdmin@assessHour.com', 'abcd1234', NULL, NULL, '', NULL, NULL, NULL, NULL, NULL, 1, 1, 1, '2022-12-28 14:25:02.000000', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `users` VALUES (2, 2, 'string', 'string', 'unsal@gmail.com', 'gfgdfgdf__', 'string', '2023-02-11 14:49:27.200000', 'string', 'string', 0, 0, 'string', 'string', 1, 0, 1, '2023-02-11 17:52:58.128890', '2023-02-11 14:49:27.200000', 0, 'string', 'string', 'string', 'string', 'string', 'string', 'string', 'string');

SET FOREIGN_KEY_CHECKS = 1;
