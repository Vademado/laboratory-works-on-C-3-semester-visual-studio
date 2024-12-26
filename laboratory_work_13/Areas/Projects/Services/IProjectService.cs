using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using laboratory_work_13.Models;

namespace laboratory_work_13.Areas.Projects.Services
{
    public interface IProjectService
    {
        Task<List<Project>> GetAllProjects();

        Task<int> AddProject(Project project);

        Task<int> DeleteProject(Project project);

        Task<int> UpdateProject(Project project);
    }
}
