using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using laboratory_work_14_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using NuGet.Packaging.Signing;
using System.Net;
using Newtonsoft.Json;

namespace laboratory_work_14_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EquipmentController : Controller
    {
        private readonly ScientificLaboratoryDBContext context;

        public EquipmentController(ScientificLaboratoryDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<List<Equipment>> GetEquipments()
        {
            return await context.Equipment.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Equipment>> GetEquipment(int id)
        {
            var equipmentDB = await (from s in context.Equipment
                                   where s.EquipmentId == id
                                  select s).FirstOrDefaultAsync();
            if (equipmentDB is null) return BadRequest("The ID in the request does not match the equipment's ID.");
            var equipmentJson = JsonConvert.SerializeObject(equipmentDB, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            return Ok(equipmentJson);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEquipment(int id, Equipment equipment)
        {
            if (id != equipment.EquipmentId) return BadRequest("The ID in the request does not match the equipment's ID.");

            var equipmentDB = await (from s in context.Equipment
                                    where s.EquipmentId == id
                                    select s).FirstOrDefaultAsync();
            if (equipmentDB is null) return NotFound();
            foreach (var field in typeof(Equipment).GetProperties())
            {
                if (field.Name == "EquipmentId") continue;
                field.SetValue(equipmentDB, field.GetValue(equipment));
            }
            await context.SaveChangesAsync();
            var equipmentJson = JsonConvert.SerializeObject(equipmentDB, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            return Ok(equipmentJson);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEquipment(Equipment equipment)
        {
            var equipmentDB = await context.Equipment.FirstOrDefaultAsync(s => s.EquipmentId == equipment.EquipmentId);
            if (equipmentDB is null) return NotFound();
            equipmentDB.Name = equipment.Name;
            equipmentDB.Status = equipment.Status;
            equipmentDB.ProjectId = equipment.ProjectId;
            await context.SaveChangesAsync();
            var equipmentJson = JsonConvert.SerializeObject(equipmentDB, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            return Ok(equipmentJson);
        }

        [HttpPost]
        public async Task<ActionResult<Equipment>> CreateEquipment(Equipment equipment)
        {
            var projectDB = await context.Projects.FirstOrDefaultAsync(s => s.ProjectId == equipment.ProjectId);
            if (projectDB is not null)
            {
                context.Equipment.Add(equipment);
                await context.SaveChangesAsync();

                var equipmentJson = JsonConvert.SerializeObject(equipment, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                var uri = Url.Action("GetEquipment", new { id = equipment.EquipmentId });
                return Created(uri, equipmentJson);
            }
            return BadRequest("The project with this ID was not found.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEquipment(int id)
        {
            var equipmentDB = await (from s in context.Equipment
                                     where s.EquipmentId == id
                                     select s).FirstOrDefaultAsync();
            if (equipmentDB is null) return BadRequest("The ID in the request does not match the equipment's ID.");
            context.Equipment.Remove(equipmentDB);
            await context.SaveChangesAsync();
            var equipmentJson = JsonConvert.SerializeObject(equipmentDB, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            return Ok(equipmentJson);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEquipment(Equipment equipment)
        {
            var equipmentDB = await context.Equipment.FirstOrDefaultAsync(s => s.EquipmentId == equipment.EquipmentId);
            if (equipmentDB is null) return NotFound();
            context.Equipment.Remove(equipmentDB);
            await context.SaveChangesAsync();
            var equipmentJson = JsonConvert.SerializeObject(equipmentDB, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            return Ok(equipmentJson);
        }
    }
}
