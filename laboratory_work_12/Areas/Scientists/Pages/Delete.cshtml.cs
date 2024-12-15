using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using laboratory_work_12.Models;

namespace laboratory_work_12.Areas.Scientists.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly laboratory_work_12.Models.ScientificLaboratoryDBContext _context;

        public DeleteModel(laboratory_work_12.Models.ScientificLaboratoryDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Scientist Scientist { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scientist = await _context.Scientists.FirstOrDefaultAsync(m => m.ScientistId == id);

            if (scientist == null)
            {
                return NotFound();
            }
            else
            {
                Scientist = scientist;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scientist = await _context.Scientists.FindAsync(id);
            if (scientist != null)
            {
                Scientist = scientist;
                _context.Scientists.Remove(Scientist);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
