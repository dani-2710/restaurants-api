using Microsoft.EntityFrameworkCore;
using Restaurants.Application.Repositories;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class DishRepository(RestaurantsDbContext dbContext) : IDishesRepository
    {
        public async Task<Dish> CreateAsync(Dish dish)
        {
            await dbContext.Dishes.AddAsync(dish);
            await dbContext.SaveChangesAsync();
            return dish;
        }

        public async Task DeleteAllAsync(IEnumerable<Dish> dishes)
        {

            dbContext.Dishes.RemoveRange(dishes);
            await dbContext.SaveChangesAsync();
        }
    }
}