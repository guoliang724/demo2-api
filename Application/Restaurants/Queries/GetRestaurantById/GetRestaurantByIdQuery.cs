using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using MediatR;

namespace Application.Restaurants.Queries.GetAllRestaurants.GetRestaurantById
{
    public class GetRestaurantByIdQuery:IRequest<RestaurantResponseDTO>
    {
        public int Id { get; set; }
    }
}