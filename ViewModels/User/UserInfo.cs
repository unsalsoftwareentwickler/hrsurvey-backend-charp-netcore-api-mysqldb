using System;

namespace QuizplusApi.ViewModels.User
{
    public class UserInfo
    {
        public int UserId { get; set; }	
        public int UserRoleId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public string DisplayName { get; set; }
		public string FullName { get; set; }
		public string Mobile { get; set; }
        public string Address{get;set;}
        public string ImagePath { get; set; }
        public int? PaymentId { get; set; }
        public string PlanName { get; set; }
        public string PlanExpiryDate { get; set; }
        public string IsPlanExpired { get; set; }
        public int? AddedBy { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? LastUpdatedBy { get; set; }

        public string PositionCode { get; set; }
        public string PositionName { get; set; }
        public string JobCode { get; set; }
        public string JobName { get; set; }
        public string DepartmentCode { get; set; }
        public string DeparmentName { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
    }
}