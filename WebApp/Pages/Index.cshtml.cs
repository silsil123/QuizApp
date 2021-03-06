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

namespace QuizApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DAL.App.EF.ApplicationDbContext _context;
        
        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<Quiz> Quiz { get; set; } = null!;
        
        [BindProperty]
        public string SortString { get; set; } = "";

        public List<Category> Categories { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(string? sort)
        {
            var unsortedList = await _context.Quizzes
                .Include(x => x.Questions)
                .ToListAsync();
            Categories = await _context.Categories.ToListAsync();
            
            Quiz = unsortedList;

            if (sort != null && Quiz.Count != 0)
            {
                switch (sort)
                {
                    case "name":
                        Quiz = unsortedList.OrderBy(x => x.QuizName).ToList();
                        break;
                    case "creator":
                        Quiz = unsortedList.OrderBy(x => x.CreatorName).ToList();
                        break;
                    case "date":
                        Quiz = unsortedList.OrderBy(x => x.DateCreated).ToList();
                        break;
                    case "category":
                        Quiz = unsortedList.OrderByDescending(x => x.CategoryId).ToList();
                        break;
                    case "popular":
                        Quiz = unsortedList.OrderByDescending(x => x.TimesPlayed).ToList();
                        return RedirectToPage("/Quizzes/Details", new {id = Quiz.FirstOrDefault()!.QuizId});
                    case "longest":
                        var count = 0;
                        var qId = Quiz.FirstOrDefault()!.QuizId;
                        foreach (var quiz in Quiz)
                        {
                            if (quiz.Questions.Count > count)
                            {
                                count = quiz.Questions.Count;
                                qId = quiz.QuizId;
                            }
                        }
                        return RedirectToPage("/Quizzes/Details", new {id = qId});
                    case "recent":
                        Quiz = unsortedList.OrderBy(x => x.DateCreated).ToList();
                        return RedirectToPage("/Quizzes/Details", new {id = Quiz.FirstOrDefault()!.QuizId});
                    default:
                        Quiz = unsortedList.Where(x => x.QuizName.ToLower().Contains(sort.ToLower()) ||
                                                     x.CreatorName.ToLower().Contains(sort.ToLower())).ToList();
                        break;
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return RedirectToPage("Index", new {sort = SortString});
        }
        
    }
}