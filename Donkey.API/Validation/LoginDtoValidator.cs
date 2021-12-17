using Donkey.API.DTOs.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.API.Validation
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("You specified wrong email format.")
                .NotNull().WithMessage("Email field cannot be empty")
                .NotEmpty().WithMessage("Email field cannot be empty");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password field cannot be null")
                .NotNull().WithMessage("Password field cannot be null");
        }
    }
}
