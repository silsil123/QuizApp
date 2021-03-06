using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizApp.Pages.AddQuestion;

namespace QuizApp.Pages.Play
{
    public class Result : PageModel
    {
        private readonly ILogger<Result> _logger;
        private readonly ApplicationDbContext _context;
        
        public Result(ApplicationDbContext context, ILogger<Result> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Game Game { get; set; } = null!;

        public Quiz Quiz { get; set; } = null!;
        
        public async Task OnGetAsync(int id)
        {
            Game = await _context.Games.Where(x => x.GameId == id)
                .Include(x => x.AnswerChoices)
                .FirstOrDefaultAsync();

            Quiz = await _context.Quizzes.Where(x => x.QuizId == Game.QuizId)
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .FirstOrDefaultAsync();
        }
    }
}