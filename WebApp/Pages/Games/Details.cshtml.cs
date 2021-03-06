using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace QuizApp.Pages_Games
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.App.EF.ApplicationDbContext _context;

        public DetailsModel(DAL.App.EF.ApplicationDbContext context)
        {
            _context = context;
        }

        public Game Game { get; set; } = null!;

        public Quiz Quiz { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Game = await _context.Games.FirstOrDefaultAsync(m => m.GameId == id);
            Quiz = await _context.Quizzes.Where(x => x.QuizId == Game.QuizId)
                .Include(x => x.Questions)
                .FirstOrDefaultAsync();

            if (Game == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
