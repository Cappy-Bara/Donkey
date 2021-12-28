using Donkey.API.DTOs.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donkey.API.Validation
{
    public class PaginationValidators : AbstractValidator<PaginationDto>
    {
        public PaginationValidators()
        {
            RuleFor(x => x.Page).GreaterThan(0).WithMessage("Page must be higher than 0");
            RuleFor(x => x.Limit).GreaterThan(0).WithMessage("Limit must be higher than 0");
        }
        
    }
}
