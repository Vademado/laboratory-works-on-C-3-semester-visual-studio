using laboratory_work_11_part_2_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(connection));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

# if !DEBUG
app.UseHttpsRedirection();
#endif

app.UseAuthorization();

app.MapControllers();

app.Run();
