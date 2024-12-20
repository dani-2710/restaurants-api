using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Repositories;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepo) : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
    {
        public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all restaurants");
            var restaurants = await restaurantsRepo.GetAllAsync();
            var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            return restaurantsDto;
        }
    }
}