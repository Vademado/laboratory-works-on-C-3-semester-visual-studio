using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace laboratory_work_15
{
    interface ILoggerRepository
    {
        Task LogAsync(string message);
    }

    class TextFileLoggerRepository : ILoggerRepository
    {
        private string filePath;
        public TextFileLoggerRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public async Task LogAsync(string message)
        {
            await File.AppendAllLinesAsync(filePath, new string[] { message });
        }
    }

    public class JsonFileLoggerRepository : ILoggerRepository
    {
        private string filePath;

        public JsonFileLoggerRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public async Task LogAsync(string message)
        {
            JObject jObject = JObject.Parse(await File.ReadAllTextAsync(filePath));
            string[] property = message.Split(':');
            if (property.Length == 1) jObject.Add(new JProperty("message", property[0]));
            else jObject.Add(new JProperty(property[0], property[1]));
            await File.WriteAllTextAsync(filePath, jObject.ToString());
        }
    }

    public class Log
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "text")]
        public string LogMessage { get; set; }
    }

    public class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions<LogDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Log> Logs { get; set; }
    }

    internal class MyLogger
    {
        private ILoggerRepository repository;

        public MyLogger(ILoggerRepository repository)
        {
            this.repository = repository;
        }

        public async Task LogAsync(string message)
        {
            await repository.LogAsync(message);
        }
    }
}