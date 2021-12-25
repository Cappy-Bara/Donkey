using Donkey.API.DTOs.Requests;
using FluentValidation;

namespace Donkey.API.Validation
{
    public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
    {
        public CreatePostDtoValidator()
        {
            RuleFor(x => x.PostTitle)
                .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("You have to provide post title")
                    .NotEmpty().WithMessage("Post title cannot be empty!");

            RuleFor(x => x.PostContent)
                .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("You have to provide some post content")
                    .NotEmpty().WithMessage("Post content cannot be empty!");
        }
    }
}
