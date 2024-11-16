using Restaurants.Domain.Entities;

namespace Restaurants.Application.Repositories
{
    public interface IDishesRepository
    {
        Task<Dish> CreateAsync(Dish dish);
        Task DeleteAllAsync(IEnumerable<Dish> dishes);
    }
}