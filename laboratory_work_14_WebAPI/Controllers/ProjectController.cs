using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using laboratory_work_14_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace laboratory_work_14_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : Controller
    {
        private readonly ScientificLaboratoryDBContext context;

        public ProjectController(ScientificLaboratoryDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<List<Project>> GetProjects()
        {
            return await context.Projects.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var projectDB = await (from s in context.Projects
                                     where s.ProjectId == id
                                     select s).FirstOrDefaultAsync();
            if (projectDB is null) return BadRequest("The ID in the request does not match the project's ID.");
            var projectJson = JsonConvert.SerializeObject(projectDB, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            return Ok(projectJson);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProject(int id, Project project)
        {
            if (id != project.ProjectId) return BadRequest("The ID in the request does not match the project's ID.");
            
            var projectDB = await (from s in context.Projects
                                   where s.ProjectId == id
                                     select s).FirstOrDefaultAsync();
            if (projectDB is null) return NotFound();
            foreach (var field in typeof(Project).GetProperties())
            {
                if (field.Name == "ProjectId") continue;
                field.SetValue(projectDB, field.GetValue(project));
            }
            await context.SaveChangesAsync();
            var projectJson = JsonConvert.SerializeObject(projectDB, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            return Ok(projectJson);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProject(Project project)
        {
            var projectDB = await context.Projects.FirstOrDefaultAsync(s => s.ProjectId == project.ProjectId);
            projectDB.Title = project.Title;
            projectDB.StartDate = project.StartDate;
            projectDB.EndDate = project.EndDate;
            projectDB.ScientistId = project.ScientistId;

            context.Projects.Update(projectDB);
            await context.SaveChangesAsync();
            var projectJson = JsonConvert.SerializeObject(projectDB, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            return Ok(projectJson);
        }

        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(Project project)
        {
            var scientistDB = await context.Scientists.FirstOrDefaultAsync(s => s.ScientistId == project.ScientistId);
            if (scientistDB is not null)
            {
                context.Projects.Add(project);
                await context.SaveChangesAsync();
                var projectJson = JsonConvert.SerializeObject(project, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                var uri = Url.Action("GetProject", new { id = project.ProjectId });
                return Created(uri, projectJson);
            }
            return BadRequest("The scientist with this ID was not found.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(int id)
        {
            var projectDB = await (from s in context.Projects
                                    where s.ProjectId == id
                                    select s).FirstOrDefaultAsync();
            if (projectDB is null) return BadRequest("The ID in the request does not match the project's ID.");

            var equipmentsList = await context.Equipment.Where(s => s.ProjectId == id).ToListAsync();
            foreach (var equipment in equipmentsList) context.Equipment.Remove(equipment);
            context.Projects.Remove(projectDB);
            await context.SaveChangesAsync();
            var projectJson = JsonConvert.SerializeObject(projectDB, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            return Ok(projectJson);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProject(Project project)
        {
            var projectDB = await context.Projects.FirstOrDefaultAsync(s => s.ProjectId == project.ProjectId);
            if (projectDB is null) return NotFound();

            var equipmentsList = await context.Equipment.Where(s => s.ProjectId == project.ProjectId).ToListAsync();
            foreach (var equipment in equipmentsList) context.Equipment.Remove(equipment);
            context.Projects.Remove(projectDB);
            await context.SaveChangesAsync();
            var projectJson = JsonConvert.SerializeObject(projectDB, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            return Ok(projectJson);
        }
    }
}
