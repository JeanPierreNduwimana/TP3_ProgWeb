using FlappyBird_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlappyBird_WebAPI.Data
{
    public class ScoreService
    {
        protected readonly FlappyBird_WebAPIContext _context;
        public ScoreService(FlappyBird_WebAPIContext context) 
        {
            _context = context;
        }

        public async Task AddScoreAsync(User user, Score score)
        {
            score.User = user;
            score.pseudo = user.UserName;
            score.date = DateTime.Now.ToString();
            string[] splitedtime = score.timeInSeconds.Split('.');
            string formatdecimals = splitedtime[1].Substring(0, 3);
            score.timeInSeconds = splitedtime[0] + "." + formatdecimals;

            await _context.Score.AddAsync(score);
            await _context.SaveChangesAsync();

        }

        public async Task<ActionResult<IEnumerable<Score>>> GetScoreAsync( User user)
        {
            return await _context.Score.Where(x => x.pseudo == user.UserName).OrderByDescending(x => x.scoreValue).Take(10).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Score>>> GetPublicScoreAsync()
        {
            return await _context.Score.Where(x => x.isPublic == true).OrderByDescending(x => x.scoreValue).Take(10).ToListAsync();
        }

        public async Task PutScoreAsync(Score score)
        {
            _context.Entry(score).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreExists(score.id))
                {
                    throw;
                }
                else
                {
                    throw;
                }
            }
        }

        public bool isContextNull()
        {
            return _context == null || _context.Score == null;

            
        }
        private bool ScoreExists(int id)
        {
            return (_context.Score?.Any(e => e.id == id)).GetValueOrDefault();
        }

    }
}
