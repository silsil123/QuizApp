using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace QuizApp.Pages.AddQuestion
{
    public class Overview : PageModel
    {
        private readonly ILogger<Overview> _logger;
        private readonly ApplicationDbContext _context;
        
        public Overview(ApplicationDbContext context, ILogger<Overview> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Quiz Quiz { get; set; } = null!;
        
        
        public async Task OnGetAsync(int id)
        {
            Quiz = await _context.Quizzes.Where(x => x.QuizId == id)
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .FirstOrDefaultAsync();
            
        }
    }
}