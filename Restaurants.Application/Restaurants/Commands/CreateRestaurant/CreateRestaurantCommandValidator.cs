using FluentValidation;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;

namespace Restaurants.Application.Restaurants.Validators
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        private readonly List<string> _validCategories =
        ["Italian", "Mexican", "Japanese", "American", "Indian"];

        public CreateRestaurantCommandValidator()
        {
            RuleFor(dto => dto.Name)
            .Length(3, 100);

            RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("Description is required.");

            RuleFor(dto => dto.Category)
            .Must(_validCategories.Contains)
            .WithMessage("Invalid category.");

            RuleFor(dto => dto.Category)
            .NotEmpty().WithMessage("Category  is required.");

            RuleFor(dto => dto.ContactEmail)
            .EmailAddress().WithMessage("Please provide a valid email address.");

            RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}").WithMessage("Please provide a valid postal code. (XX-XXX)");
        }
    }
}