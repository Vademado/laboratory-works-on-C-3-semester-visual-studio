using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using laboratory_work_14_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace laboratory_work_14_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScientistController : Controller
    {
        private readonly ScientificLaboratoryDBContext context;

        public ScientistController(ScientificLaboratoryDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<List<Scientist>> GetScientists()
        {
            return await context.Scientists.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Scientist>> GetScientist(int id)
        {
            var scientistDB = await (from s in context.Scientists
                                     where s.ScientistId == id
                                     select s).FirstOrDefaultAsync();
            if (scientistDB is null) return BadRequest("The ID in the request does not match the scientist's ID."); ;
            var scientistJson = JsonConvert.SerializeObject(scientistDB, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            return Ok(scientistJson);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateScientist(int id, Scientist scientist)
        {
            if (id != scientist.ScientistId) return BadRequest("The ID in the request does not match the scientist's ID.");

            var scientistDB = await (from s in context.Scientists
                                     where s.ScientistId == id
                                     select s).FirstOrDefaultAsync();
            if (scientistDB is null) return NotFound();
            foreach (var field in typeof(Scientist).GetProperties())
            {
                if (field.Name == "ScientistId") continue;
                field.SetValue(scientistDB, field.GetValue(scientist));
            }
            await context.SaveChangesAsync();
            var scientistJson = JsonConvert.SerializeObject(scientistDB, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            return Ok(scientistJson);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateScientist(Scientist scientist)
        {
            var scientistDB = await context.Scientists.FirstOrDefaultAsync(s => s.ScientistId == scientist.ScientistId);
            if (scientistDB is null) return NotFound();
            scientistDB.Name = scientist.Name;
            scientistDB.Specialty = scientist.Specialty;
            scientistDB.Email = scientist.Email;
            await context.SaveChangesAsync();
            var scientistJson = JsonConvert.SerializeObject(scientistDB, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            return Ok(scientistJson);
        }

        [HttpPost]
        public async Task<ActionResult<Scientist>> CreateScientist(Scientist scientist)
        {
            context.Scientists.Add(scientist);
            await context.SaveChangesAsync();
            var projectJson = JsonConvert.SerializeObject(scientist, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            var uri = Url.Action("GetScientist", new { id = scientist.ScientistId });
            return Created(uri, projectJson);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteScientist(int id)
        {
            var scientistDB = await (from s in context.Scientists
                                     where s.ScientistId == id
                                     select s).FirstOrDefaultAsync();
            if (scientistDB is null) return BadRequest("The ID in the request does not match the scientist's ID.");
            context.Scientists.Remove(scientistDB);
            await context.SaveChangesAsync();
            var scientistJson = JsonConvert.SerializeObject(scientistDB, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            return Ok(scientistJson);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteScientist(Scientist scientist)
        {
            var scientistDB = await context.Scientists.FirstOrDefaultAsync(s => s.ScientistId == scientist.ScientistId);
            if (scientistDB is null) return BadRequest("The ID in the request does not match the scientist's ID.");

            var projectsListDB = await context.Projects.Where(s => s.ScientistId == scientist.ScientistId).ToListAsync();
            if (projectsListDB is not null)
            {
                foreach (var project in projectsListDB)
                {
                    var equipmentsList = await context.Equipment.Where(s => s.ProjectId == project.ProjectId).ToListAsync();
                    foreach (var equipment in equipmentsList) context.Equipment.Remove(equipment);
                    context.Projects.Remove(project);
                }
            }
            context.Scientists.Remove(scientistDB);
            await context.SaveChangesAsync();
            var scientistJson = JsonConvert.SerializeObject(scientistDB, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            return Ok(scientistJson);
        }
    }
}
