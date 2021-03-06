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
    public class IndexModel : PageModel
    {
        private readonly DAL.App.EF.ApplicationDbContext _context;

        public IndexModel(DAL.App.EF.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Quiz> Quiz { get; set; } = null!;

        public async Task OnGetAsync()
        {
            Quiz = await _context.Quizzes.ToListAsync();
        }
    }
}
