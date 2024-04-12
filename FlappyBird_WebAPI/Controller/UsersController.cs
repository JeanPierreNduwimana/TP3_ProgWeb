using FlappyBird_WebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlappyBird_WebAPI.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly UserManager<User> userManager;

        public UsersController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }


        [HttpPost]
        public async Task<ActionResult> Login(LoginDTO login)
        {
            User user = await userManager.FindByNameAsync(login.Username);
            if(user != null && await userManager.CheckPasswordAsync(user,login.Password))
            {
                IList<string> roles = await userManager.GetRolesAsync(user);
                List<Claim> authClaims = new List<Claim>();
                foreach(string role in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                authClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Looo000ongue Phrase SinoN Ca nem arche passsAAAssssaS !"));
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: "http://localhost:7151",
                    audience:"http://localhost:4200",
                    claims: authClaims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                    );
                return Ok(
                    new 
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        validTo = token.ValidTo,
                        Message = "Connexion reussie! 😎" 
                    });

            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new { Message = "Le nom d'utilisateur ou le mot de passe est invalide." });
            }
           

        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterDTO register)
        {

            User userExist = await userManager.FindByNameAsync(register.Username);

            if(userExist != null)
            {
                return BadRequest(new { Message = "Le nom de l'utilisateur existe déjà" });
            }

            if (register.Password != register.PasswordConfirm) 
            {
                return BadRequest(new { Message = "Mot de passe non-identiques!" });
            }

            User user = new User()
            {
                UserName = register.Username,
                Email = register.Email
            };

            IdentityResult identityResult = await this.userManager.CreateAsync(user, register.Password);

            if (!identityResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Erreur inattendue." });
                
            }

            return Ok( new {Message = "Inscription réussie! 😎"});

        }



    }
}
