using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using laboratory_work_13.Models;

namespace laboratory_work_13.Areas.Equipments.Services
{
    public interface IEquipmentService
    {
        Task<List<Equipment>> GetAllEquipments();

        Task<int> AddEquipment(Equipment equipment);

        Task<int> DeleteEquipment(Equipment equipment);

        Task<int> UpdateEquipment(Equipment equipment);
    }
}
