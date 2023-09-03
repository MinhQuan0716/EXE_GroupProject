using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure
{
    public class AppDbContext:DbContext
    {
       
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }
        public DbSet<User>Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CareerQuiz> CareerQuizzes { get; set; }
        public DbSet<QuizType> QuizTypes { get; set; }
        public DbSet<QuizOption> QuizOptions { get; set; }
        public DbSet<UserResponse> UserResponses { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
