using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace QuizApp.Pages_Quizzes
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.App.EF.ApplicationDbContext _context;

        public DetailsModel(DAL.App.EF.ApplicationDbContext context)
        {
            _context = context;
        }

        public Quiz Quiz { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Quiz = await _context.Quizzes.FirstOrDefaultAsync(m => m.QuizId == id);

            if (Quiz == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
