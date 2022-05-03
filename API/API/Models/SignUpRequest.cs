using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.API.Models
{
    public class SignUpRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        [Required]
        public Guid GenderId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
