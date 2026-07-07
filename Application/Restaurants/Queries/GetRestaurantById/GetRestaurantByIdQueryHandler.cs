
using Application.DTOs;
using Application.Restaurants.Queries.GetAllRestaurants.GetRestaurantById;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Restaurants.Queries.GetRestaurantById
{
  public class GetRestaurantByIdQueryHandler : IRequestHandler<GetRestaurantByIdQuery, RestaurantResponseDTO?>
  {
    private readonly IRestaurantsRepository _restaurantRepository;
    private readonly IMapper _mapper;


    public GetRestaurantByIdQueryHandler(IRestaurantsRepository restaurantRepository, IMapper mapper)
    {
      _restaurantRepository = restaurantRepository;
      _mapper = mapper;
    }


    public async Task<RestaurantResponseDTO?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
      var restaurant = await _restaurantRepository.GetByIdAsync(request.Id);
      var restaurantDTO = _mapper.Map<RestaurantResponseDTO>(restaurant);

      return restaurantDTO;

    }
  }
}