using BeYourRestaurant.Platform.Core.Domain;
using FluentValidation;

namespace BeYourRestaurant.Platform.User.Domain
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
    }

    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username).NotNull().NotEmpty().MinimumLength(5).MaximumLength(50);
        }
    }
}
