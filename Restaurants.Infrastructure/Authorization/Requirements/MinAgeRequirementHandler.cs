using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    internal class MinAgeRequirementHandler(ILogger<MinAgeRequirementHandler> logger, IUserContext userContext) : AuthorizationHandler<MinAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinAgeRequirement requirement)
        {
            var currentUser = userContext.GetCurrentUser();

            logger.LogInformation("Handling min age requirement");

            if (currentUser is null || currentUser.DateOfBirth is null)
            {
                logger.LogInformation("User date of birth is null");
                context.Fail();
                return Task.CompletedTask;
            }

            if (currentUser.DateOfBirth.Value.AddYears(requirement.MinAge) < DateOnly.FromDateTime(DateTime.Today))
            {
                logger.LogInformation("Authorization succeeded");
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}