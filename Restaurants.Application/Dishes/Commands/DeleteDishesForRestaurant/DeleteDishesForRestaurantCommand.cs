using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDishesForRestaurant
{
    public class DeleteDishesForRestaurantCommand(Guid restaurantId) : IRequest
    {
        public Guid RestaurantId { get; } = restaurantId;
    }
}