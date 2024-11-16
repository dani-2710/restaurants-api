using Microsoft.EntityFrameworkCore;
using Restaurants.Application.Repositories;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
    {
        public async Task<Restaurant> CreateAsync(Restaurant restaurant)
        {
            await dbContext.Restaurants.AddAsync(restaurant);
            await dbContext.SaveChangesAsync();
            return restaurant;
        }

        public async Task DeleteAsync(Restaurant restaurant)
        {
            dbContext.Restaurants.Remove(restaurant);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await dbContext.Restaurants.Include(restaurant => restaurant.Dishes).ToListAsync();
        }

        public async Task<Restaurant?> GetByIdAsync(Guid id)
        {
            return await dbContext.Restaurants.Include(restaurant => restaurant.Dishes).FirstOrDefaultAsync(restaurant => restaurant.Id == id);
        }

        public async Task UpdateAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}