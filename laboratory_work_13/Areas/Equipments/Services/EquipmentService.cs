using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using laboratory_work_13.Models;
using Microsoft.EntityFrameworkCore;

namespace laboratory_work_13.Areas.Equipments.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly ScientificLaboratoryDBContext _context;
        public EquipmentService(ScientificLaboratoryDBContext context)
        {
            _context = context;
        }
        public async Task<int> AddEquipment(Equipment equipment)
        {
            var projectDB = await _context.Projects.FirstOrDefaultAsync(s => s.ProjectId == equipment.ProjectId);
            if (projectDB is not null)
            {
                await _context.Equipment.AddAsync(equipment);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
        public async Task<int> DeleteEquipment(Equipment equipment)
        {
            var equipmentDB = await _context.Equipment.FirstOrDefaultAsync(m => m.EquipmentId == equipment.EquipmentId);
            if (equipmentDB == null) return 0;
            _context.Equipment.Remove(equipment);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Equipment>> GetAllEquipments()
        {
            var equipmentsList = await _context.Equipment.ToListAsync();
            return equipmentsList;
        }

        public async Task<int> UpdateEquipment(Equipment equipment)
        {
            var equipmentDB = await _context.Equipment.FirstOrDefaultAsync(m => m.EquipmentId == equipment.EquipmentId);
            if (equipmentDB == null) return 0;
            equipmentDB.Name = equipment.Name;
            equipmentDB.Status = equipment.Status;
            equipmentDB.ProjectId = equipment.ProjectId;
            return await _context.SaveChangesAsync();
        }

    }
}
