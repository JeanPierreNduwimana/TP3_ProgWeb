using Microsoft.AspNetCore.Identity;

namespace FlappyBird_WebAPI.Models
{
    public class User : IdentityUser
    {
        List<Score> Scores { get; set; } = null!;

    }
}
