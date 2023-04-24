using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuizplusApi.Models.Quiz
{
    public class CertificateTemplate
    {
        public int CertificateTemplateId{get;set;}
        [Required]
        [StringLength(1000)]
        public string Title{get;set;}
        [StringLength(1000)]
        public string Heading{get;set;}
        [Required]
        [StringLength(2000)]
        public string MainText{get;set;}
        [StringLength(100)]
        public string PublishDate{get;set;}
        public string TopLeftImagePath{get;set;}
        public string TopRightImagePath{get;set;}
        public string BottomMiddleImagePath{get;set;}
        public string BackgroundImagePath{get;set;}
        public string BackgroundColor{get;set;}
        [StringLength(1000)]
        public string LeftSignatureText{get;set;}
        public string LeftSignatureImagePath{get;set;}
        [StringLength(1000)]
        public string RightSignatureText{get;set;}
        public string RightSignatureImagePath{get;set;}
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