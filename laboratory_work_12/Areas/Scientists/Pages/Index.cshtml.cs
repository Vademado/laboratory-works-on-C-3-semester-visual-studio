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
    public class IndexModel : PageModel
    {
        private readonly laboratory_work_12.Models.ScientificLaboratoryDBContext _context;

        public IndexModel(laboratory_work_12.Models.ScientificLaboratoryDBContext context)
        {
            _context = context;
        }

        public IList<Scientist> Scientist { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Scientist = await _context.Scientists.ToListAsync();
        }
    }
}
