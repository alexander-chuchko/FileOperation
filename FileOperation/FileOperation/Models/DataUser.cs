using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FileOperation.Models
{
    public class DataUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public IFormFile Picture { get; set; }
    }
}
