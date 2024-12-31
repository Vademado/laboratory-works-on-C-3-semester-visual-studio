using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using laboratory_work_14_MAUIApp.Models;

namespace laboratory_work_14_MAUIApp.Areas.Scientists.Services
{
    public interface IScientistService
    {
        private static HttpClient httpClient = new HttpClient();
        Task<List<Scientist>> GetAllScientists();

        Task<int> AddScientist(Scientist scientist);

        Task<int> DeleteScientist(Scientist scientist);

        Task<int> UpdateScientist(Scientist scientist);
    }
}
