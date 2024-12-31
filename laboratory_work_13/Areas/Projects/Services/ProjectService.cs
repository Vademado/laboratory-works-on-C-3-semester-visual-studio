using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using laboratory_work_13.Models;
using System.Diagnostics;

namespace laboratory_work_13.Areas.Projects.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ScientificLaboratoryDBContext _context;
        public ProjectService(ScientificLaboratoryDBContext context)
        {
            _context = context;
        }
        public async Task<int> AddProject(Project project)
        {
            var scientistDB = await _context.Scientists.FirstOrDefaultAsync(s => s.ScientistId == project.ScientistId);
            if (scientistDB is not null)
            {
                await _context.Projects.AddAsync(project);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> DeleteProject(Project project)
        {
            var projectDB = await _context.Projects.FirstOrDefaultAsync(m => m.ProjectId == project.ProjectId);
            if (projectDB == null) return 0;

            var equipmentsList = await _context.Equipment.Where(s => s.ProjectId == project.ProjectId).ToListAsync();
            foreach (var equipment in equipmentsList) _context.Equipment.Remove(equipment);
            _context.Projects.Remove(projectDB);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Project>> GetAllProjects()
        {
            var projectsList = await _context.Projects.ToListAsync();
            return projectsList;
        }

        public async Task<int> UpdateProject(Project project)
        {
            var projectDB = await _context.Projects.FirstOrDefaultAsync(m => m.ProjectId == project.ProjectId);
            projectDB.Title = project.Title;
            projectDB.StartDate = project.StartDate;
            projectDB.EndDate = project.EndDate;
            projectDB.ScientistId = project.ScientistId;
            _context.Projects.Update(projectDB);
            return await _context.SaveChangesAsync();
        }
    }
}
