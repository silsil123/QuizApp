using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace QuizApp.Pages.Play
{
    public class Index : PageModel
    {
        private readonly ILogger<Index> _logger;
        private readonly DAL.App.EF.ApplicationDbContext _context;
        
        public Index(ILogger<Index> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Quiz Quiz { get; set; } = null!;

        [BindProperty]
        public int QuizId { get; set; }

        [BindProperty]
        public string PlayerName { get; set; } = "";

        public async Task OnGetAsync(int id) //id = Quiz id, q = question number
        {
            Quiz = await _context.Quizzes.Where(x => x.QuizId == id)
                .FirstOrDefaultAsync();
            Quiz.TimesPlayed += 1;
            _context.Update(Quiz);
            await _context.SaveChangesAsync();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var game = new Game()
            {
                QuizId = QuizId,
                PlayerName = PlayerName,
            };

            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Play/GamePlay", new {id = game.GameId, q = 0});
        }
    }
}