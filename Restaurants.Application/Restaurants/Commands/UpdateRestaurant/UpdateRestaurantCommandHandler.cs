using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Interfaces;
using Restaurants.Application.Repositories;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger, IRestaurantsRepository restaurantsRepo, IMapper mapper, IRestaurantAuthorizationService authorizationService) : IRequestHandler<UpdateRestaurantCommand>
    {
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Updating restaurant");
            var restaurant = await restaurantsRepo.GetByIdAsync(request.Id);

            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            }

            if (!authorizationService.Authorize(restaurant, Constants.ResourceOperation.Update))
            {
                throw new ForbidException();
            }

            mapper.Map(request, restaurant);

            await restaurantsRepo.UpdateAsync();
        }
    }
}