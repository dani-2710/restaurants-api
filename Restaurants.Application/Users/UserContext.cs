using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Restaurants.Application.Users
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }

    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public CurrentUser? GetCurrentUser()
        {
            var user = httpContextAccessor.HttpContext?.User;

            if (user is null) throw new InvalidOperationException("User context is not present");

            if (user.Identity is null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var userId = user.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(claim => claim.Type == ClaimTypes.Email)!.Value;
            var nationality = user.FindFirst(claim => claim.Type == "Nationality")?.Value;
            var dateOfBirthString = user.FindFirst(claim => claim.Type == "DateOfBirth")?.Value;
            var roles = user.FindAll(claim => claim.Type == ClaimTypes.Role)!.Select(role => role.Value);

            var dateOfBirth = dateOfBirthString != null ? DateOnly.ParseExact(dateOfBirthString, "yyyy-MM-dd") : (DateOnly?)null;
            return new CurrentUser(userId, email, roles, nationality, dateOfBirth);
        }
    }
}