﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlappyBird_WebAPI.Data;
using FlappyBird_WebAPI.Models;
using System.Security.Claims;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FlappyBird_WebAPI.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly FlappyBird_WebAPIContext _context;

        public ScoresController(FlappyBird_WebAPIContext context)
        {
            _context = context;
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
            User? user = await _context.Users.FindAsync(userId);

            if (user != null) { score.User = user; score.pseudo = user.UserName; }

            score.date = DateTime.Now.ToString();

            string[] splitedtime = score.timeInSeconds.Split('.');

            string formatdecimals = splitedtime[1].Substring(0, 3);

            score.timeInSeconds = splitedtime[0] + "." + formatdecimals;
            

            await _context.Score.AddAsync(score);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        // GET: api/Scores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Score>>> GetScore()
        {
            if (_context.Score == null)
            {
                return NotFound();
            }
            return await _context.Score.ToListAsync();
        }

        // GET: api/Scores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Score>> GetScore(int id)
        {
            if (_context.Score == null)
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

        // PUT: api/Scores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScore(int id, Score score)
        {
            if (id != score.id)
            {
                return BadRequest();
            }

            _context.Entry(score).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Scores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Score>> PostScore(Score score)
        {
            if (_context.Score == null)
            {
                return Problem("Entity set 'FlappyBird_WebAPIContext.Score'  is null.");
            }
            _context.Score.Add(score);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScore", new { id = score.id }, score);
        }

        // DELETE: api/Scores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScore(int id)
        {
            if (_context.Score == null)
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

        private bool ScoreExists(int id)
        {
            return (_context.Score?.Any(e => e.id == id)).GetValueOrDefault();
        }

        
    }
}
