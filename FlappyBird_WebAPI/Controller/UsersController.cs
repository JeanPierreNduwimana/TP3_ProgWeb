﻿using FlappyBird_WebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using FlappyBird_WebAPI.Data;

namespace FlappyBird_WebAPI.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly UserManager<User> userManager;

        public UsersController(UserManager<User> userManager, FlappyBird_WebAPIContext context)
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
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    validTo = token.ValidTo
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
            if(register.Password != register.PasswordConfirm) 
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Mot de passe non-identiques!" });
            }

            User user = new User()
            {
                UserName = register.Username,
                Email = register.Email
            };

            IdentityResult identityResult = await this.userManager.CreateAsync(user, register.Password);

            if (!identityResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = "La création de l'utilisateur a échoué" });
            }

            return Ok( new {Message = "Inscription réussie! 😎"});

        }



    }
}
