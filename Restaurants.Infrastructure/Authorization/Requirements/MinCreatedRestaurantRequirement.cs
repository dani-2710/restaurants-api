using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class MinCreatedRestaurantRequirement(int minCreatedRestaurant) : IAuthorizationRequirement
    {
        public int MinCreatedRestaurant { get; } = minCreatedRestaurant;
    }
}