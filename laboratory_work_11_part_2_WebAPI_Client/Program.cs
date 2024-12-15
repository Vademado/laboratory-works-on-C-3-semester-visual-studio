using Newtonsoft.Json.Linq;
using laboratory_work_11_part_2_WebAPI_Client.Models;
using System.Text;

namespace laboratory_work_11_part_2_WebAPI_Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using HttpClient httpClient = new HttpClient();

            List<Supplier> suppliers = await GetSuppliers(httpClient);

            //await PostSupplier(httpClient, new Supplier() { City = "Moscow", CompanyName = "Yandex" });

            //await PutSupplier(httpClient, 30, new Supplier() { CompanyName = "Sber" });

            //Supplier supplier = await GetSupplier(httpClient, 30);

            //await DeleteSupplier(httpClient, 30);
        }

        public static async Task<List<Supplier>> GetSuppliers(HttpClient httpClient)
        {
            string URL = $"http://localhost:5264/api/Suppliers";
            Console.WriteLine($"{URL} (Get Request Message)\n");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, URL);
            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);
            JArray jArray = JArray.Parse(await httpResponse.Content.ReadAsStringAsync());
            Console.WriteLine(jArray);
            return jArray.ToObject<List<Supplier>>();
        }

        public static async Task PostSupplier(HttpClient httpClient, Supplier supplier)
        {
            string URL = $"http://localhost:5264/api/Suppliers";
            JObject jObject = JObject.FromObject(supplier);
            Console.WriteLine($"{URL} (Post Request Message)\n" + jObject);
            StringContent stringContent = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, URL) { Content = stringContent };
            await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, URL) { Content = stringContent });
        }

        public static async Task<Supplier> GetSupplier(HttpClient httpClient, int id)
        {
            string URL = $"http://localhost:5264/api/Suppliers/{id}";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, URL);
            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);
            JObject jObject = JObject.Parse(await httpResponse.Content.ReadAsStringAsync());
            Console.WriteLine($"{URL} (Get Request Message)\n");
            return jObject.ToObject<Supplier>();
        }

        public static async Task PutSupplier(HttpClient httpClient, int id, Supplier supplier)
        {
            string URL = $"http://localhost:5264/api/Suppliers/{id}";
            JObject jObject = JObject.FromObject(supplier);
            Console.WriteLine($"{URL} (Put Request Message)\n" + jObject);
            StringContent stringContent = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Put, URL) { Content = stringContent };
            await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Put, URL) { Content = stringContent });
        }

        public static async Task DeleteSupplier(HttpClient httpClient, int id)
        {
            string URL = $"http://localhost:5264/api/Suppliers/{id}";
            Console.WriteLine($"{URL} (Delete Request Message)\n");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Delete, URL);
            await httpClient.SendAsync(httpRequest);
        }
    }
}