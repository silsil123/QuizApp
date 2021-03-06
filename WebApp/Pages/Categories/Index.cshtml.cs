using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace QuizApp.Pages.Categories
{
    public class Index : PageModel
    {
        private readonly DAL.App.EF.ApplicationDbContext _context;

        public Index(DAL.App.EF.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Category> Categories { get; set; } = null!;

        [BindProperty]
        public string NewCat { get; set; } = null!;
        
        public async Task OnGetAsync()
        {
            Categories = await _context.Categories.ToListAsync();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            var cat = new Category()
            {
                CategoryString = NewCat
            };

            await _context.Categories.AddAsync(cat);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}