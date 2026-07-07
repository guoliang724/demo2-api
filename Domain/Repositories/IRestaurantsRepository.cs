using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Restaurant;

namespace Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();

        Task<Restaurant?> GetByIdAsync(int id);

        Task<int> CreateAsync(Restaurant restaurant);

        Task DeleteAsync(Restaurant restaurant);

        Task SaveChangesAsync();
    }
}