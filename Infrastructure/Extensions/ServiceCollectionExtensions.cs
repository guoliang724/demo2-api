
using Domain.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Repository;
using Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<ApplicationDbContext>(options =>
      {

        string? cString = configuration.GetSection("connectionStrings")["defaults"] ?? string.Empty;
        options.UseSqlServer(cString);
      });


      services.AddScoped<IDataSeed, DataSeed>();
      services.AddScoped<IRestaurantsRepository, RestaurantRepository>();
    }
  }
}