
using Application.DTOs;
using MediatR;

namespace Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQuery:IRequest<IEnumerable<RestaurantResponseDTO>>
    {
        
    }
}