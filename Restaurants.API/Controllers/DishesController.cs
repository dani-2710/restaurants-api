using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDishesForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurant/{restaurantId:guid}/dishes")]
    [Authorize]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDish(Guid restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;
            var dish = await mediator.Send(command);
            return CreatedAtAction(nameof(GetDishByIdForRestaurant), new { restaurantId, dishId = dish.Id }, null);
        }

        [HttpGet]
        [Authorize(Policy = PolicyNames.AtLeast20)]
        public async Task<IActionResult> GetDishesForRestaurant(Guid restaurantId)
        {
            var query = new GetDishesForRestaurantQuery(restaurantId);
            var dishes = await mediator.Send(query);

            return Ok(dishes);
        }

        [HttpGet("{dishId:guid}")]
        public async Task<IActionResult> GetDishByIdForRestaurant(Guid restaurantId, Guid dishId)
        {
            var query = new GetDishByIdForRestaurantQuery(restaurantId, dishId);
            var dish = await mediator.Send(query);

            return Ok(dish);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDishesForRestaurant(Guid restaurantId)
        {
            var command = new DeleteDishesForRestaurantCommand(restaurantId);

            await mediator.Send(command);

            return NoContent();
        }
    }
}