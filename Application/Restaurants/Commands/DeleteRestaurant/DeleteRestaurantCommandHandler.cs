

using Domain.Exceptions;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Restaurants.Commands.DeleteRestaurant
{
  public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand>
  {
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly ILogger<DeleteRestaurantCommandHandler> _logger;

    public DeleteRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository, ILogger<DeleteRestaurantCommandHandler> logger)
    {
      _restaurantsRepository = restaurantsRepository;
      _logger = logger;
    }

    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling delete restaurant command.");
      var restaurant = await _restaurantsRepository.GetByIdAsync(request.Id);
      if (restaurant == null)
      {
        _logger.LogWarning("Restaurant not found.");
        throw new NotFoundException($"Restaurant ID {request.Id} not found.");
      }

      _logger.LogInformation("Restaurant found. Proceeding with deletion.");
      await _restaurantsRepository.DeleteAsync(restaurant);
      await _restaurantsRepository.SaveChangesAsync();

    }
  }
}