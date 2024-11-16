using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class MinAgeRequirement(int minAge) : IAuthorizationRequirement
    {
        public int MinAge { get; set; } = minAge;
    }
}