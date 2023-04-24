using System;
using System.ComponentModel.DataAnnotations;

namespace QuizplusApi.Models.Others
{
    public class Contacts
    {
        [Key]
        public int ContactId{get;set;}
		[Required]
		[StringLength(500)]
        public string Name{get;set;}
        [Required]
		[StringLength(1000)]
        public string Email{get;set;}
		[Required]
        public string Message{get;set;}
		[Required]
		public DateTime DateAdded { get; set; }
    }
}