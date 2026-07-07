using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Restaurants.Commands.UpdateRestaurant
{
  public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, bool>
  {
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly IMapper _mapper;

    public UpdateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository, IMapper mapper)
    {
      _restaurantsRepository = restaurantsRepository;
      _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
      var restaurant = await _restaurantsRepository.GetByIdAsync(request.Id);
      if (restaurant == null)
      {
        return false;
      }

      _mapper.Map(request, restaurant);

      await _restaurantsRepository.SaveChangesAsync();
      return true;
    }
  }
}