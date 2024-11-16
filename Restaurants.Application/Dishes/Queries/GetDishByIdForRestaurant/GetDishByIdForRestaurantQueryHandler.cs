using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Repositories;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant
{
    public class GetDishByIdForRestaurantQueryHandler(ILogger<GetDishByIdForRestaurantQueryHandler> logger, IRestaurantsRepository restaurantsRepo, IMapper mapper) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
    {
        public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Retrieving dish: {request.DishId} for restaurant: {request.RestaurantId}");
            var restaurant = await restaurantsRepo.GetByIdAsync(request.RestaurantId);

            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }

            var dish = restaurant.Dishes.FirstOrDefault(dish => dish.Id == request.DishId);
            if (dish is null)
            {
                throw new NotFoundException(nameof(Dish), request.RestaurantId.ToString());
            }
            var dishDto = mapper.Map<DishDto>(dish);
            return dishDto;
        }
    }
}