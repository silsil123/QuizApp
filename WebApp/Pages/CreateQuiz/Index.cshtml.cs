using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace QuizApp.Pages.CreateQuiz
{
    public class Index : PageModel
    {
        private readonly ILogger<Index> _logger;
        private readonly ApplicationDbContext _context;
        
        public Index(ApplicationDbContext context, ILogger<Index> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public string QuizName { get; set; } = null!;
        
        [BindProperty]
        public string CreatorName { get; set; } = null!;
        
        [BindProperty]
        public string Description { get; set; } = null!;
        
        [BindProperty]
        public int QuestionAmount { get; set; }
        
        
        [BindProperty]
        public int CategoryId { get; set; }

        public List<Category> Categories { get; set; } = null!;

        public async Task OnGetAsync(int c, int id, string? dir, int p) //c = ship count, p = player, g = placement
        {
            Categories = await _context.Categories.ToListAsync();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var quiz = new Quiz()
            {
                QuizName = QuizName,
                CreatorName = CreatorName,
                CategoryId = CategoryId,
                Description = Description
            };

            await _context.Quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();

            return RedirectToPage("/AddQuestion/Index", new { id = quiz.QuizId, q = QuestionAmount });
        }
    }
}