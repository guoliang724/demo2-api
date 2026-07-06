using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Restaurant;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Restaurants
{
  public class RestaurantService
  {
    private readonly IRestaurantsRepository _restaurantRepository;
    private readonly ILogger<RestaurantService> _logger;

    public RestaurantService(IRestaurantsRepository restaurantRepository, ILogger<RestaurantService> logger)
    {
      _restaurantRepository = restaurantRepository;
      _logger = logger;
    }

    public async Task<IEnumerable<Restaurant>> GetRestaurants()
    {
      _logger.LogInformation("Fetching all restaurants from the repository.");
      return await _restaurantRepository.GetAllAsync();
    }

    public async Task<Restaurant?> GetRestaurantById(int id)
    {
      _logger.LogInformation($"Fetching restaurant with ID: {id} from the repository.");
       var restaurant = await _restaurantRepository.GetByIdAsync(id);
      return restaurant;
    }
  }
}