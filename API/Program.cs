using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using API.Model;
using API.Model.DTO;
using API.Services;
using Scalar.AspNetCore;
using Infrastructure.Persistence;
using Domain.Entities.Villa;
using Domain.Entities.User;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
  string? cString = builder.Configuration.GetSection("connectionStrings").Get<DbString>()?.defaults ?? string.Empty;
  options.UseSqlServer(cString);
});

builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
  options.RequireHttpsMetadata = false;
  options.SaveToken = true;
  options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
  {
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty)),
    ClockSkew = TimeSpan.Zero
  };
});

builder.Services.AddControllers();
builder.Services.AddOpenApi(options =>
{
  options.AddDocumentTransformer((document, request, cancellationToken) =>
  {
    document.Components ??= new();
    document.Components.SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>
    {
      ["Bearer"] = new OpenApiSecurityScheme
      {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Bearer token"
      }
    };

    return Task.FromResult(document);
  });
}
);


builder.Services.AddAutoMapper(a =>
{
  a.CreateMap<Villa, VillaCreateDTO>().ReverseMap();
  a.CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
  a.CreateMap<Villa, VillaDTO>();
  a.CreateMap<User, UserDTO>().ReverseMap();
});

builder.Services.AddScoped<IAuthService, AuthService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();

