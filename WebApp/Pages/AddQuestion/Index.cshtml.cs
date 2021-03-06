using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace QuizApp.Pages.AddQuestion
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
        public int QuizId { get; set; }

        [BindProperty]
        public int QuestionAmount { get; set; }

        [BindProperty]
        [Required]
        public string Question { get; set; } = null!;

        [Required]
        [BindProperty] 
        public string Answer1 { get; set; } = null!;
        
        [Required]
        [BindProperty] 
        public string Answer2 { get; set; } = null!;

        [BindProperty] 
        public string Answer3 { get; set; } = "";

        [BindProperty] 
        public string Answer4 { get; set; } = "";
        
        [BindProperty] 
        public string Answer5 { get; set; } = "";
        
        [BindProperty] 
        public int CorrectAnswer { get; set; }
        
        
        
        public async Task OnGetAsync(int id, int q) //Quiz id and question amount.
        {
            QuizId = id;
            QuestionAmount = q;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            var question = new Question()
            {
                QuizId = QuizId,
                QuestionString = Question
            };

            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
            
            for (int x = 0; x < 5; x++)
            {
                string? answerS = null;
                var answerC = false;
                
                switch (x)
                {
                    case 0:
                        answerS = Answer1;
                        answerC = CorrectAnswer == 1;
                        break;
                    case 1:
                        answerS = Answer2;
                        answerC = CorrectAnswer == 2;
                        break;
                    case 2:
                        answerS = Answer3;
                        answerC = CorrectAnswer == 3;
                        break;
                    case 3:
                        answerS = Answer4;
                        answerC = CorrectAnswer == 4;
                        break;
                    case 4:
                        answerS = Answer5;
                        answerC = CorrectAnswer == 5;
                        break;
                }

                if (answerS != null)
                {
                    var answer = new Answer()
                    {
                        QuestionId = question.QuestionId,
                        AnswerString = answerS,
                        CorrectAnswer = answerC
                    };

                    await _context.Answers.AddAsync(answer);
                }
            }

            QuestionAmount--;

            await _context.SaveChangesAsync();

            return QuestionAmount == 0 ? RedirectToPage("/AddQuestion/Overview", new { id = QuizId }) :
                RedirectToPage("/AddQuestion/Index", new { id = QuizId, q = QuestionAmount });
        }
    }
}