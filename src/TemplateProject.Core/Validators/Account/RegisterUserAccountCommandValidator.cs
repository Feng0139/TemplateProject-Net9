using FluentValidation;
using TemplateProject.Core.Middlewares.FluentMessageValidator;
using TemplateProject.Message.Commands.Account;

namespace TemplateProject.Core.Validators.Account;

public class RegisterUserAccountCommandValidator : FluentMessageValidator<RegisterUserAccountCommand>
{
    public RegisterUserAccountCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long")
            .MaximumLength(64).WithMessage("Name cannot be longer than 64 characters")
            .Matches(@"^[a-zA-Z][a-zA-Z0-9]*$").WithMessage("Name must start with a letter and can only contain letters and numbers");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$").WithMessage("Password must contain uppercase letters, lowercase letters, and digits");
    }
}