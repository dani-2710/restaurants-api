using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Constants;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurant")]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = PolicyNames.CreatedAtLeast2Restaurants)]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var query = new GetAllRestaurantsQuery();
            IEnumerable<RestaurantDto> restaurants = await mediator.Send(query);
            return Ok(restaurants);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Policy = PolicyNames.HasNationality)]
        public async Task<IActionResult> GetRestaurantById(Guid id)
        {
            var query = new GetRestaurantByIdQuery(id);
            var restaurant = await mediator.Send(query);

            return Ok(restaurant);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Owner)]
        public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand command)
        {
            var restaurant = await mediator.Send(command);

            return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurant.Id }, null);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRestaurant(Guid id)
        {
            var command = new DeleteRestaurantCommand(id);
            await mediator.Send(command);

            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> UpdateRestaurant(Guid id, UpdateRestaurantCommand command)
        {
            command.Id = id;

            await mediator.Send(command);

            return NoContent();
        }
    }
}