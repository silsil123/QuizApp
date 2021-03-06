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
    public class IndexModel : PageModel
    {
        private readonly DAL.App.EF.ApplicationDbContext _context;

        public IndexModel(DAL.App.EF.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Game> Game { get; set; } = null!;

        public IList<Quiz> Quizzes { get; set; } = null!;

        public async Task OnGetAsync(string? sort)
        {
            var unsortedList = await _context.Games.ToListAsync();
            Quizzes = await _context.Quizzes
                .Include(x => x.Questions)
                .ToListAsync();
            
            Game = unsortedList;
            
            if (sort != null)
            {
                switch (sort)
                {
                    case "name":
                        Game = unsortedList.OrderBy(x => x.QuizId).ToList();
                        break;
                    case "score":
                        Game = unsortedList.OrderByDescending(x => x.Points).ToList();
                        break;
                    case "player":
                        Game = unsortedList.OrderBy(x => x.PlayerName).ToList();
                        break;
                    case "date":
                        Game = unsortedList.OrderByDescending(x => x.DateCreated).ToList();
                        break;
                }
            }
        }
    }
}
