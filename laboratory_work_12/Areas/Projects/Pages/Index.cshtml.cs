using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using laboratory_work_12.Models;

namespace laboratory_work_12.Areas.Projects.Pages
{
    public class IndexModel : PageModel
    {
        private readonly laboratory_work_12.Models.ScientificLaboratoryDBContext _context;

        public IndexModel(laboratory_work_12.Models.ScientificLaboratoryDBContext context)
        {
            _context = context;
        }

        public IList<Project> Project { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Project = await _context.Projects
                .Include(p => p.Scientist).ToListAsync();
        }
    }
}
