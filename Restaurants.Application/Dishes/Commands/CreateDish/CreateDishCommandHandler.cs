using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Interfaces;
using Restaurants.Application.Repositories;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger, IRestaurantsRepository restaurantsRepo, IDishesRepository dishesRepo, IMapper mapper, IRestaurantAuthorizationService authorizationService) : IRequestHandler<CreateDishCommand, DishDto>
    {
        public async Task<DishDto> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating new dish");
            var restaurant = await restaurantsRepo.GetByIdAsync(request.RestaurantId);

            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }

            if (!authorizationService.Authorize(restaurant, Constants.ResourceOperation.Update))
            {
                throw new ForbidException();
            }


            var dish = mapper.Map<Dish>(request);
            var createdDish = await dishesRepo.CreateAsync(dish);
            var dishDto = mapper.Map<DishDto>(createdDish);
            return dishDto;
        }
    }
}