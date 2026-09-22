using BuzzUp_API.Application.DTO.Posts;
using BuzzUp_API.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.Validators.Post
{
    public class PostInsertValidator : AbstractValidator<PostInsertDTO>
    {
        public PostInsertValidator(BuzzUpContext ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .MaximumLength(30)
                .WithMessage("Maximum length for Title is 30.");

            RuleFor(x => x.Description)
                .MaximumLength(50)
                .WithMessage("Maximum length for Description is 50.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x.Location)
                .MaximumLength(50)
                .WithMessage("Maximum length for Location is 50.")
                .When(x => !string.IsNullOrWhiteSpace(x.Location));

            RuleFor(x => x.VisibilityTypeId)
                .NotEmpty()
                .WithMessage("Visibility is required.")
                .Must(id => ctx.VisibilityTypes.Any(v => v.Id == id && v.IsActive && v.DeletedAt == null))
                .WithMessage("Visibility type does not exist.");

            RuleFor(x => x.FeelingTypeId)
                .Must(id => ctx.FeelingTypes.Any(f => f.Id == id.Value && f.IsActive && f.DeletedAt == null))
                .WithMessage("Feeling type does not exist.")
                .When(x => x.FeelingTypeId.HasValue);

            RuleFor(x => x.Image)
                .Must(name =>
                {
                    var lower = name.ToLowerInvariant();
                    return lower.EndsWith(".jpg") || lower.EndsWith(".jpeg") || lower.EndsWith(".png");
                })
                .WithMessage("Image must be a JPG or PNG file.")
                .Must(_ => ctx.PostMediaTypes.Any(t => t.Name == "Image" && t.IsActive && t.DeletedAt == null))
                .WithMessage("Image media type does not exist.")
                .When(x => !string.IsNullOrWhiteSpace(x.Image));
        }
    }
}
