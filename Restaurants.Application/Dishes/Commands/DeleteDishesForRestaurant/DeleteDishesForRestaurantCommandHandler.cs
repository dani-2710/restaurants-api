using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Interfaces;
using Restaurants.Application.Repositories;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Dishes.Commands.DeleteDishesForRestaurant
{
    public class DeleteDishesForRestaurantCommandHandler(ILogger<DeleteDishesForRestaurantCommandHandler> logger, IRestaurantsRepository restaurantsRepo, IDishesRepository dishesRepo, IRestaurantAuthorizationService authorizationService) : IRequestHandler<DeleteDishesForRestaurantCommand>
    {
        public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Deleting dishes for restaurant: {request.RestaurantId}");
            var restaurant = await restaurantsRepo.GetByIdAsync(request.RestaurantId);

            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }

            if (!authorizationService.Authorize(restaurant, Constants.ResourceOperation.Update))
            {
                throw new ForbidException();
            }

            await dishesRepo.DeleteAllAsync(restaurant.Dishes);
        }
    }
}