
using Microsoft.AspNetCore.Mvc;
using FlappyBird_WebAPI.Data;
using FlappyBird_WebAPI.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace FlappyBird_WebAPI.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly ScoreService _scoreService;

        public ScoresController(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }


        // POST: api/Scores
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddScore(Score score)
        {
            if (score == null)
            {
                return BadRequest();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User? user = await _scoreService.FindUserAsync(userId);

            if(user != null)
            {
                await _scoreService.AddScoreAsync(user, score);
                return NoContent();
            }
            else
            {
                return NotFound(new {Message = "Aucun utilisateur connecté(e)"} );
            }

        }

        // GET: api/Scores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Score>>> GetScore()
        {
            if (_scoreService.isContextNull())
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User? user = await _scoreService.FindUserAsync(userId);

            if (user != null)
            {
               return await _scoreService.GetScoreAsync(user);
            }
            else { return NotFound(new  {Message = "Il faut se connecter pour accèder au scores privés." }); }
        }

        // GET: api/Scores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Score>>> GetPublicScore()
        {
            if (_scoreService.isContextNull())
            {
                return NotFound();
            }

            return await _scoreService.GetPublicScoreAsync();
            
        }

        // PUT: api/Scores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutScore(Score score)
        {
            if (score == null)
            {
                return BadRequest();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User? user = await _scoreService.FindUserAsync(userId);

            if (user == null)
            {
                return NotFound(new { Message = "Il faut se connecter pour modifer les scores privés." });
            }

                await _scoreService.PutScoreAsync(score);

            return NoContent();
        }

    }
}
