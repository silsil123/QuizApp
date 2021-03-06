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
    public class DeleteModel : PageModel
    {
        private readonly DAL.App.EF.ApplicationDbContext _context;

        public DeleteModel(DAL.App.EF.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty] 
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Quiz = await _context.Quizzes.FindAsync(id);

            if (Quiz != null)
            {
                _context.Quizzes.Remove(Quiz);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Index");
        }
    }
}
