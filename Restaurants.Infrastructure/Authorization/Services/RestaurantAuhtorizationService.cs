using Microsoft.Extensions.Logging;
using Restaurants.Application.Constants;
using Restaurants.Application.Interfaces;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Authorization.Services
{
    public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger, IUserContext userContext) : IRestaurantAuthorizationService
    {
        public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
        {
            var currentUser = userContext.GetCurrentUser();
            logger.LogInformation("Authorizing user for restaurant");

            if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
            {
                logger.LogInformation("Create/read operation - successful authorization");
                return true;
            }

            if (resourceOperation == ResourceOperation.Delete && currentUser!.IsInRole(UserRoles.Admin))
            {
                logger.LogInformation("Admin user, delete operation - successful authorization");
                return true;
            }

            if (resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update && currentUser!.Id == restaurant.OwnerId)
            {
                logger.LogInformation("Restaurant owner - successful authorization");
                return true;
            }

            return false;
        }
    }
}