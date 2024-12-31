using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using laboratory_work_14_MAUIApp.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace laboratory_work_14_MAUIApp.Areas.Equipments.Services
{
    public class EquipmentService : IEquipmentService
    {
        private static HttpClient httpClient = new HttpClient();
# if DEBUG
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5142" : "http://localhost:5142";
#else
        public static string BaseAddress = "https://localhost:7257";
#endif
        private readonly ScientificLaboratoryDBContext _context;
        public EquipmentService(ScientificLaboratoryDBContext context)
        {
            _context = context;
        }
        public async Task<int> AddEquipment(Equipment equipment)
        {
            string URL = $"{BaseAddress}/Equipment";
            JObject jObject = JObject.FromObject(equipment);
            StringContent stringContent = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, URL) { Content = stringContent };
            var httpResponse = await httpClient.SendAsync(httpRequest);
            return httpResponse.IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> DeleteEquipment(Equipment equipment)
        {
            string URL = $"{BaseAddress}/Equipment";
            JObject jObject = JObject.FromObject(equipment);
            StringContent stringContent = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Delete, URL) { Content = stringContent };
            var httpResponse = await httpClient.SendAsync(httpRequest);
            return httpResponse.IsSuccessStatusCode ? 1 : 0;
        }

        public async Task<List<Equipment>> GetAllEquipments()
        {
            string URL = $"{BaseAddress}/Equipment";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, URL);
            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);
            JArray jArray = JArray.Parse(await httpResponse.Content.ReadAsStringAsync());
            return jArray.ToObject<List<Equipment>>();
        }

        public async Task<int> UpdateEquipment(Equipment equipment)
        {
            string URL = $"{BaseAddress}/Equipment";
            JObject jObject = JObject.FromObject(equipment);
            StringContent stringContent = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Put, URL) { Content = stringContent };
            var httpResponse = await httpClient.SendAsync(httpRequest);
            return httpResponse.IsSuccessStatusCode ? 1 : 0;
        }

    }
}
