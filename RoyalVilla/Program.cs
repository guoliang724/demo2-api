using Data;
using Microsoft.EntityFrameworkCore;
using RoyalVilla.Model;
using RoyalVilla.Model.DTO;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
  string? cString = builder.Configuration.GetSection("connectionStrings").Get<DbString>()?.defaults ?? string.Empty;
  options.UseSqlServer(cString);
});
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(a =>
{
  a.CreateMap<Villa, VillaCreateDTO>().ReverseMap();
  a.CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
  a.CreateMap<Villa, VillaDTO>();
});



var app = builder.Build();

// solution1
string? connectionString = app.Configuration.GetSection("connectionStrings")["defaults"];

DbString? c1 = app.Configuration.GetSection("connectionStrings").Get<DbString>();

Console.WriteLine("ConnectionStrings" + c1?.defaults);


if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.MapOpenApi();
  app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();


app.Run();

