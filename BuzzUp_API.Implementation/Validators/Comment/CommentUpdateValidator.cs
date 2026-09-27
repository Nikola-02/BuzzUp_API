using BuzzUp_API.Application.DTO.Comments;
using BuzzUp_API.DataAccess;
using FluentValidation;

namespace BuzzUp_API.Implementation.Validators.Comment
{
    public class CommentUpdateValidator : AbstractValidator<CommentUpdateDTO>
    {
        public CommentUpdateValidator(BuzzUpContext ctx)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Comment is required.")
                .Must(id => ctx.Comments.Any(comment => comment.Id == id && comment.IsActive && comment.DeletedAt == null))
                .WithMessage("Comment does not exist.");

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Comment is required.")
                .MaximumLength(2000)
                .WithMessage("Comment cannot be longer than 2000 characters.");
        }
    }
}
