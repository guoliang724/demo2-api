using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Restaurant;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
  public class RestaurantRepository : IRestaurantsRepository
  {
    private readonly ApplicationDbContext _context;

    public RestaurantRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
      var restaurants = await _context.Restaurants.ToListAsync();
      return restaurants;
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
      var restaurant = await _context.Restaurants.Include(r => r.Dishes).FirstOrDefaultAsync(r => r.Id == id);
      return restaurant;
    }
  }
}