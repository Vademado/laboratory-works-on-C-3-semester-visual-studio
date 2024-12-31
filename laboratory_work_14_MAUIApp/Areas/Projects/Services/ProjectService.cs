using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using laboratory_work_14_MAUIApp.Models;
using Newtonsoft.Json.Linq;
using System.Net;

namespace laboratory_work_14_MAUIApp.Areas.Projects.Services
{
    public class ProjectService : IProjectService
    {
        private static HttpClient httpClient = new HttpClient();
# if DEBUG
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5142" : "http://localhost:5142";
#else
        public static string BaseAddress = "https://localhost:7257";
#endif
        private readonly ScientificLaboratoryDBContext _context;
        public ProjectService(ScientificLaboratoryDBContext context)
        {
            _context = context;
        }
        public async Task<int> AddProject(Project project)
        {
            string URL = $"{BaseAddress}/Project";
            JObject jObject = JObject.FromObject(project);
            StringContent stringContent = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, URL) { Content = stringContent };
            var httpResponse = await httpClient.SendAsync(httpRequest);
            return httpResponse.IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<int> DeleteProject(Project project)
        {
            string URL = $"{BaseAddress}/Project";
            JObject jObject = JObject.FromObject(project);
            StringContent stringContent = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Delete, URL) { Content = stringContent };
            var httpResponse = await httpClient.SendAsync(httpRequest);
            return httpResponse.IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<List<Project>> GetAllProjects()
        {
            string URL = $"{BaseAddress}/Project";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, URL);
            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);
            JArray jArray = JArray.Parse(await httpResponse.Content.ReadAsStringAsync());
            return jArray.ToObject<List<Project>>();
        }

        public async Task<int> UpdateProject(Project project)
        {
            string URL = $"{BaseAddress}/Project";
            JObject jObject = JObject.FromObject(project);
            StringContent stringContent = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Put, URL) { Content = stringContent };
            var httpResponse = await httpClient.SendAsync(httpRequest);
            return httpResponse.IsSuccessStatusCode ? 1 : 0;
        }
    }
}
