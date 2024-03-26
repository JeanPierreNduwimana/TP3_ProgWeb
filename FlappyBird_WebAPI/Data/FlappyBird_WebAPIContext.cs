using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlappyBird_WebAPI.Models;

namespace FlappyBird_WebAPI.Data
{
    public class FlappyBird_WebAPIContext : DbContext
    {
        public FlappyBird_WebAPIContext (DbContextOptions<FlappyBird_WebAPIContext> options)
            : base(options)
        {
        }

        public DbSet<FlappyBird_WebAPI.Models.Score> Score { get; set; } = default!;
    }
}
