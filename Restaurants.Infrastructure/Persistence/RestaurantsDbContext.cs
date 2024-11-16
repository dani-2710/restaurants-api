using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence
{
    internal class RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) : IdentityDbContext<User>(options)
    {
        internal DbSet<Restaurant> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Restaurant>().OwnsOne(restaurant => restaurant.Address);

            modelBuilder.Entity<Restaurant>()
            .HasMany(restaurant => restaurant.Dishes)
            .WithOne()
            .HasForeignKey(dish => dish.RestaurantId);

            modelBuilder.Entity<User>()
            .HasMany(user => user.OwnedRestaurants)
            .WithOne(restaurant => restaurant.Owner)
            .HasForeignKey(restaurant => restaurant.OwnerId);
        }
    }
}