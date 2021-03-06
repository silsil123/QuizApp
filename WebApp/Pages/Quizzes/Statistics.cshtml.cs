using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizApp.Pages.AddQuestion;

namespace QuizApp.Pages.Quizzes
{
    public class Statistics : PageModel
    {
        private readonly ILogger<Statistics> _logger;
        private readonly ApplicationDbContext _context;
        
        public Statistics(ApplicationDbContext context, ILogger<Statistics> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Quiz Quiz { get; set; } = null!;

        public IList<AnswerChoice> Choices { get; set; } = null!;
        public IList<Game> Games { get; set; } = null!;
        
        public async Task OnGetAsync(int id)
        {
            Quiz = await _context.Quizzes.Where(x => x.QuizId == id)
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .FirstOrDefaultAsync();

            Choices = await _context.AnswerChoices.ToListAsync();

            Games = await _context.Games.Where(x => x.QuizId == Quiz.QuizId)
                .ToListAsync();

        }
    }
}