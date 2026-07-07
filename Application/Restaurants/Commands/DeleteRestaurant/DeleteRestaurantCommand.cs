using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommand:IRequest<bool>
    {
        public int Id { get; set; }
    }
}