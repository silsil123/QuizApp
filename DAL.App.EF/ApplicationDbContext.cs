using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Quiz> Quizzes { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<Answer> Answers { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Game> Games { get; set; } = null!;

        public DbSet<AnswerChoice> AnswerChoices { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
    }
}