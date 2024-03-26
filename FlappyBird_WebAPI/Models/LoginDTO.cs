using System.ComponentModel.DataAnnotations;

namespace FlappyBird_WebAPI.Models
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
