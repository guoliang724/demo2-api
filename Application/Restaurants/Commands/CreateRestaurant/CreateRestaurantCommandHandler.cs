using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.Restaurant;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Restaurants.Commands.CreateRestaurant
{
  public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
  {
    private readonly ILogger<CreateRestaurantCommandHandler> _logger;
    private readonly IMapper _mapper;

    private readonly IRestaurantsRepository _restaurantRepository;

    public CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper, IRestaurantsRepository restaurantRepository)
    {
      _logger = logger;
      _mapper = mapper;
      _restaurantRepository = restaurantRepository;
    }
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Creating a new restaurant with name: {Name}", request.Name);
      Restaurant? restaurant = _mapper.Map<Restaurant>(request);
      // Implementation for saving the restaurant to the database

      int id = await _restaurantRepository.CreateAsync(restaurant);

      return id;
    }
  }
}