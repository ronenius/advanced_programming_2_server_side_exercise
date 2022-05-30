using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using advanced_programming_2_server_side_exercise.Models;

namespace advanced_programming_2_server_side_exercise.Data
{
    public class advanced_programming_2_server_side_exerciseContext : DbContext
    {
        public advanced_programming_2_server_side_exerciseContext(DbContextOptions<advanced_programming_2_server_side_exerciseContext> options)
            : base(options)
        {
        }

        public DbSet<advanced_programming_2_server_side_exercise.Models.Message> Message { get; set; }

        public DbSet<advanced_programming_2_server_side_exercise.Models.Review> Review { get; set; }

        public DbSet<advanced_programming_2_server_side_exercise.Models.Contact> Contact { get; set; }

        public DbSet<advanced_programming_2_server_side_exercise.Models.User> User { get; set; }
    }
}
