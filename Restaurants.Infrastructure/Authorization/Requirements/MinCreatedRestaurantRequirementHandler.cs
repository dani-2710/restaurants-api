using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Repositories;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class MinCreatedRestaurantRequirementHandler(ILogger<MinCreatedRestaurantRequirementHandler> logger, IUserContext userContext, IRestaurantsRepository restaurantsRepo) : AuthorizationHandler<MinCreatedRestaurantRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MinCreatedRestaurantRequirement requirement)
        {
            var currentUser = userContext.GetCurrentUser();
            logger.LogInformation("Handling min created restaurant");

            if (currentUser is null)
            {
                logger.LogInformation("User is null");
                context.Fail();
            }

            var restaurants = await restaurantsRepo.GetAllAsync();

            var createdRestaurants = restaurants.Count(restaurant => restaurant.OwnerId == currentUser!.Id);

            if (createdRestaurants >= requirement.MinCreatedRestaurant)
            {
                logger.LogInformation("Authorization succeeded");
                context.Succeed(requirement);
            }
            else
            {
                logger.LogInformation("Authorization Failed");
                context.Fail();
            }
        }
    }
}