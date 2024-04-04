
using Microsoft.AspNetCore.Mvc;
using FlappyBird_WebAPI.Data;
using FlappyBird_WebAPI.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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
                return NotFound();
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
            else { return NotFound(); }
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

            await _scoreService.PutScoreAsync(score);

            return NoContent();
        }

        

        // GET: api/Scores/5

        /*
            [HttpGet("{id}")]
            public async Task<ActionResult<Score>> GetScore(int id)
            {
                if (_scoreService.isContextNull())
                {
                    return NotFound();
                }
                var score = await _context.Score.FindAsync(id);

                if (score == null)
                {
                    return NotFound();
                }

                return score;
            }


           


            // DELETE: api/Scores/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteScore(int id)
            {
                if (_scoreService.isContextNull())
                {
                    return NotFound();
                }
                var score = await _context.Score.FindAsync(id);
                if (score == null)
                {
                    return NotFound();
                }

                _context.Score.Remove(score);
                await _context.SaveChangesAsync();

                return NoContent();
            }

           
        */

    }
}
