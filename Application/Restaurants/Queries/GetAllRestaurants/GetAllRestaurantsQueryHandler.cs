
using Application.DTOs;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Restaurants.Queries.GetAllRestaurants
{
  public class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantResponseDTO>>
  {
    private readonly IRestaurantsRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public GetAllRestaurantsQueryHandler(IRestaurantsRepository restaurantRepository, IMapper mapper)
    {
      _restaurantRepository = restaurantRepository;
      _mapper = mapper;
    }

    public async Task<IEnumerable<RestaurantResponseDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
      var restaurants = await _restaurantRepository.GetAllAsync();
      var restaurantDTOs = _mapper.Map<IEnumerable<RestaurantResponseDTO>>(restaurants);
      return restaurantDTOs;
    }
  }
}