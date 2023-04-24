using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuizplusApi.Models.Others
{
    public class BillingPlan
    {
        [Key]
        public int BillingPlanId{get;set;}
		[Required]
		[StringLength(500)]
        public string Title{get;set;}
        [Required]
        public int Price{get;set;}
        [Required]
		[StringLength(50)]
        public string Interval{get;set;}
        [Required]
        public int AssessmentCount{get;set;}
        [Required]
        public int QuestionPerAssessmentCount{get;set;}
        [Required]
        public int ResponsePerAssessmentCount{get;set;}
        public string AdditionalText{get;set;}
		[Required]
		public bool IsActive { get; set; }
		[DefaultValue(false)]
		public bool IsMigrationData { get; set; }
		[Required]
		public int AddedBy { get; set; }
		[Required]
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