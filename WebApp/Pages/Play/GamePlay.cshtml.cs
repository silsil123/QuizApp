using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace QuizApp.Pages.Play
{
    public class GamePlay : PageModel
    {
        private readonly ILogger<GamePlay> _logger;
        private readonly ApplicationDbContext _context;
        
        public GamePlay(ILogger<GamePlay> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Game Game { get; set; } = null!;

        public Quiz Quiz { get; set; } = null!;

        public Question CurrentQuestion { get; set; } = null!;

        public int QuestionNumber { get; set; }

        [BindProperty]
        public int UserChoice { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int id, int q, int? c) //id = game id, q = question nr, c = last chosen answer
        {
            Game = await _context.Games.Where(x => x.GameId == id)
                .Include(x => x.AnswerChoices)
                .FirstOrDefaultAsync();

            Quiz = await _context.Quizzes.Where(x => x.QuizId == Game.QuizId)
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .FirstOrDefaultAsync();

            if (c != null)
            {
                var choice = new AnswerChoice()
                {
                    GameId = Game.GameId,
                    QuestionId = Quiz.Questions.ElementAt(q - 1).QuestionId,
                    AnswerId = Quiz.Questions.ElementAt(q - 1).Answers.ElementAt((int) c).AnswerId
                };

                await _context.AnswerChoices.AddAsync(choice);
                await _context.SaveChangesAsync();

                if ((await _context.Answers.Where(x => x.AnswerId == choice.AnswerId)
                    .FirstOrDefaultAsync()).CorrectAnswer)
                {
                    Game.Points += 1;
                    await _context.SaveChangesAsync();
                }
                
                if (q == Quiz.Questions.Count)
                {
                    return RedirectToPage("/Play/Result", new { id = Game.GameId });
                }
            }
            
            CurrentQuestion = Quiz.Questions.ElementAt(q);
            QuestionNumber = q + 1;

                return Page();
        }
        
        
    }
}