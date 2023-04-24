using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuizplusApi.Models.Quiz
{
    public class Instruction
    {
        public int InstructionId{get;set;}
        [Required]
        public int QuizTopicId{get;set;}
        [StringLength(1000)]
        public string Description{ get; set; }
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