using System;
using System.ComponentModel.DataAnnotations;

namespace QuizplusApi.ViewModels.Quiz
{
    public class CertificateInfo
    {
        public string Address{get;set;}
        public bool AllowCorrectOption{get;set;}
        public int AttemptCount{get;set;}
        public int CertificateTemplateId{get;set;}
        public string DateAdded{get;set;}
        public DateTime? DateOfBirth{get;set;}
        public string Email{get;set;}
        public string FullName{get;set;}
        public bool IsExamined{get;set;}
        public string Mobile{get;set;}
        public decimal QuizMark{get;set;}
        public int QuizMarkOptionId{get;set;}
        public decimal QuizPassMarks{get;set;}
        public int QuizResponseInitialId{get;set;}
        public double QuizTime{get;set;}
        public string QuizTitle{get;set;}
        public int QuizTopicId{get;set;}
        public double? TimeTaken{get;set;}
        public decimal UserObtainedQuizMark{get;set;}
    }
}