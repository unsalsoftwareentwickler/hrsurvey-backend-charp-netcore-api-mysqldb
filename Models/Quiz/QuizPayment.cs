using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizplusApi.Models.Quiz
{
    public class QuizPayment
    {
        public int QuizPaymentId{get;set;}
        [Required]
        public int QuizTopicId{get;set;}
        [Column(TypeName = "decimal(5, 2)")]
        [DefaultValue(0)]
        public decimal Amount{get;set;}
        [Required]
        [StringLength(100)]
        public string Email {get; set;}
        [Required]
        [StringLength(50)]
        public string Currency{get;set;}
        [Required]
        [StringLength(200)]
        public string SessionId{get;set;}
        [Required]
		public int AddedBy {get; set;}
        [Required]
		public int AdminId {get; set;}
		[Required]
		public DateTime DateAdded { get; set; }
    }
}