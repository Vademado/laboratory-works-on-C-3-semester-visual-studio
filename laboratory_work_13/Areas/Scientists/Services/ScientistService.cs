using laboratory_work_13.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratory_work_13.Areas.Scientists.Services
{
    public class ScientistService : IScientistService
    {
        private readonly ScientificLaboratoryDBContext _context;
        public ScientistService(ScientificLaboratoryDBContext context)
        {
            _context = context;
        }
        public async Task<int> AddScientist(Scientist scientist)
        {
            await _context.Scientists.AddAsync(scientist);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteScientist(Scientist scientist)
        {
            var scientistDB = await _context.Scientists.FirstOrDefaultAsync(m => m.ScientistId == scientist.ScientistId);
            if (scientistDB == null) return 0;

            var projectsListDB = await _context.Projects.Where(m => m.ScientistId == scientist.ScientistId).ToListAsync();
            if (projectsListDB is not null) 
            {
                foreach (var project in projectsListDB)
                {
                    var equipmentsList = await _context.Equipment.Where(s => s.ProjectId == project.ProjectId).ToListAsync();
                    foreach (var equipment in equipmentsList) _context.Equipment.Remove(equipment);
                    _context.Projects.Remove(project);
                }
            }
            _context.Scientists.Remove(scientistDB);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Scientist>> GetAllScientists()
        {
            var scientistsList = await _context.Scientists.ToListAsync();
            return scientistsList;
        }

        public async Task<int> UpdateScientist(Scientist scientist)
        {
            var scientistDB = await _context.Scientists.FirstOrDefaultAsync(m => m.ScientistId == scientist.ScientistId);
            if (scientistDB == null) return 0;
            scientistDB.Name = scientist.Name;
            scientistDB.Specialty = scientist.Specialty;
            scientistDB.Email = scientist.Email;
            _context.Scientists.Update(scientistDB);
            return await _context.SaveChangesAsync();

        }
    }
}
