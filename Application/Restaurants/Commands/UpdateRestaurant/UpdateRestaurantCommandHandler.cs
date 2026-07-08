using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Application.Restaurants.Commands.UpdateRestaurant
{
  public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand>
  {
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly IMapper _mapper;

    public UpdateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository, IMapper mapper)
    {
      _restaurantsRepository = restaurantsRepository;
      _mapper = mapper;
    }

    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
      var restaurant = await _restaurantsRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException($"Not Found ${request.Id}");

      _mapper.Map(request, restaurant);

      await _restaurantsRepository.SaveChangesAsync();

    }
  }
}