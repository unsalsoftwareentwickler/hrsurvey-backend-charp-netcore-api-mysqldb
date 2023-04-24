﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuizplusApi.Models.User
{
    public class Users
    {
		[Key]
		public int UserId { get; set; }
		[Required]
		public int UserRoleId { get; set; }
		[Required]
		[StringLength(100)]
		public string FullName { get; set; }
		[StringLength(100)]
		public string Mobile { get; set; }
		[Required]
		[StringLength(100)]
		public string Email { get; set; }
		[Required]
		[StringLength(100)]
		public string Password { get; set; }
		[StringLength(1000)]
		public string Address{get;set;}
		public DateTime? DateOfBirth { get; set; }
		[StringLength(1000)]
		public string ImagePath { get; set; }
		[StringLength(800)]
		public string StripeSessionId { get; set; }
		public int? BillingPlanId { get; set; }
		public int? PaymentId { get; set; }
		[StringLength(50)]
		public string PaymentMode { get; set; }
		public string TransactionDetail { get; set; }
		[Required]
		public bool IsActive { get; set; }	
		[DefaultValue(false)]
		public bool IsMigrationData { get; set; }
		public int? AddedBy { get; set; }		
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
