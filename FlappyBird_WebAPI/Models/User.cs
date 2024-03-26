using Microsoft.AspNetCore.Identity;

namespace FlappyBird_WebAPI.Models
{
    public class User : IdentityUser
    {
        public string Pseudo { get; set; }
        List<Score> Scores { get; set; } = null!;

    }
}
