using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Repositories;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepo, IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, RestaurantDto>
    {
        public async Task<RestaurantDto> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            logger.LogInformation("Creating new restaurant");
            var restaurant = mapper.Map<Restaurant>(request);
            restaurant.OwnerId = currentUser!.Id;
            var createdRestaurant = await restaurantsRepo.CreateAsync(restaurant);
            var restaurantDto = mapper.Map<RestaurantDto>(createdRestaurant);
            return restaurantDto;
        }
    }
}