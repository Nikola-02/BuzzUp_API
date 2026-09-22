using AutoMapper;
using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Posts;
using BuzzUp_API.Application.Exceptions;
using BuzzUp_API.Application.UseCases.Commands.Posts;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.UseCases.Commands.Posts
{
    public class EfUpdatePostCommand : EfUpdateUseCase<PostUpdateDTO, Post>, IUpdatePostCommand
    {
        private readonly IApplicationActor _actor;

        public EfUpdatePostCommand(BuzzUpContext context, IMapper mapper, IValidator<PostUpdateDTO> validator, IApplicationActor actor)
            : base(context, mapper, validator)
        {
            _actor = actor;
        }

        public override int Id => 13;

        public override string Name => "Update Post";

        protected override void EnsureCanUpdate(PostUpdateDTO request, Post entity)
        {
            if (entity.UserId != _actor.Id)
            {
                throw new ForbiddenException("You can only update your own post.");
            }
        }

        protected override void AfterUpdate(PostUpdateDTO request, Post entity)
        {
            if (request.Image == "")
            {
                SoftDeleteActiveMedia(entity);
                return;
            }

            if (string.IsNullOrWhiteSpace(request.Image))
            {
                return;
            }

            var current = entity.PostMedias.FirstOrDefault(m => m.IsActive && m.DeletedAt == null);
            if (current != null && current.Path == request.Image)
            {
                return;
            }

            SoftDeleteActiveMedia(entity);

            var imageTypeId = Context.PostMediaTypes
                .Where(x => x.Name == "Image" && x.IsActive && x.DeletedAt == null)
                .Select(x => x.Id)
                .First();

            entity.PostMedias.Add(new PostMedia
            {
                Path = request.Image,
                PostMediaTypeId = imageTypeId
            });
        }

        private static void SoftDeleteActiveMedia(Post post)
        {
            foreach (var media in post.PostMedias.Where(m => m.IsActive && m.DeletedAt == null))
            {
                media.IsActive = false;
                media.DeletedAt = DateTime.UtcNow;
            }
        }
    }
}
