using Restaurants.Domain.Entities;

namespace Restaurants.Application.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant?> GetByIdAsync(Guid id);
        Task<Restaurant> CreateAsync(Restaurant restaurant);
        Task DeleteAsync(Restaurant restaurant);
        Task UpdateAsync();
    }
}