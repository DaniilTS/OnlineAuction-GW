using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.API.Models.Requests
{
    public class LotCreateRequest
    {
        [Required]
        public Guid LotCategoryId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile[] FormFiles { get; set; }
    }
}
