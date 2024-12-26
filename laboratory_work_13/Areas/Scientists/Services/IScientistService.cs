using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using laboratory_work_13.Models;

namespace laboratory_work_13.Areas.Scientists.Services
{
    public interface IScientistService
    {
        Task<List<Scientist>> GetAllScientists();

        Task<int> AddScientist(Scientist scientist);

        Task<int> DeleteScientist(Scientist scientist);

        Task<int> UpdateScientist(Scientist scientist);
    }
}
