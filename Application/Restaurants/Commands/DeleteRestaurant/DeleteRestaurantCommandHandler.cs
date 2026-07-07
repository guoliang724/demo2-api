

using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Restaurants.Commands.DeleteRestaurant
{
  public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand, bool>
  {
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly ILogger<DeleteRestaurantCommandHandler> _logger;
  
    public DeleteRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository, ILogger<DeleteRestaurantCommandHandler> logger)
    {
      _restaurantsRepository = restaurantsRepository;
      _logger = logger;
    }

    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling delete restaurant command.");
      var restaurant = await  _restaurantsRepository.GetByIdAsync(request.Id);
      if (restaurant == null)
      {
        _logger.LogWarning("Restaurant not found.");
        return false;
      }
      
      _logger.LogInformation("Restaurant found. Proceeding with deletion.");
      await _restaurantsRepository.DeleteAsync(restaurant);
      await _restaurantsRepository.SaveChangesAsync();
      return true;

    }
  }
}