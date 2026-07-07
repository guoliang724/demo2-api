
using Application.Restaurants.Commands.CreateRestaurant;
using FluentValidation;

namespace Application.Restaurants.Validators
{
  public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
  {
    public CreateRestaurantCommandValidator()
    {
      RuleFor(x => x.Name)
          .NotEmpty().WithMessage("Name is required.")
          .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

      RuleFor(x => x.Description)
          .NotEmpty().WithMessage("Description is required.")
          .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

      RuleFor(x => x.Category)
          .NotEmpty().WithMessage("Category is required.")
          .MaximumLength(50).WithMessage("Category cannot exceed 50 characters.");

      RuleFor(x => x.ContactEmail)
          .EmailAddress().WithMessage("Invalid email address format.")
          .When(x => !string.IsNullOrEmpty(x.ContactEmail));

      RuleFor(x => x.ContactNumber)
          .Matches(@"^\+?\d{10,15}$").WithMessage("Invalid contact number format.")
          .When(x => !string.IsNullOrEmpty(x.ContactNumber));
    }
  }
}