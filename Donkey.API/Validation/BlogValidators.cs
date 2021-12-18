using Donkey.API.DTOs.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Donkey.API.Validation
{
    public class CreateBlogDtoValidators : AbstractValidator<CreateBlogDto>
    {
        private readonly Regex _rightDomainNameRegex = new("^[A-Za-z0-9-]{1,63}$");
        
        public CreateBlogDtoValidators()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("You have to pass blog name")
                .NotEmpty().WithMessage("Blog name field cannot be empty")
                .Must(x =>_rightDomainNameRegex.IsMatch(x)).WithMessage("Provided blog name contains invalid signs")
                .MinimumLength(3).WithMessage("Blog name cannot be shorter than 3 characters");
        }
    }
}
