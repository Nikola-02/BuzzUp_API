using AutoMapper;
using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Posts;
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
    public class EfCreatePostCommand : EfUseCaseMapper, ICreatePostCommand
    {
        private readonly IValidator<PostInsertDTO> _validator;
        private readonly IApplicationActor _actor;

        public EfCreatePostCommand(BuzzUpContext context, IMapper mapper, IValidator<PostInsertDTO> validator, IApplicationActor actor)
            : base(context, mapper)
        {
            _validator = validator;
            _actor = actor;
        }

        public int Id => 10;

        public string Name => "Create Post";

        public void Execute(PostInsertDTO data)
        {
            _validator.ValidateAndThrow(data);

            var post = Mapper.Map<Post>(data);
            post.UserId = _actor.Id;

            if (!string.IsNullOrWhiteSpace(data.Image))
            {
                var imageTypeId = Context.PostMediaTypes
                    .Where(x => x.Name == "Image" && x.IsActive && x.DeletedAt == null)
                    .Select(x => x.Id)
                    .First();

                post.PostMedias.Add(new PostMedia
                {
                    Path = data.Image,
                    PostMediaTypeId = imageTypeId
                });
            }

            Context.Posts.Add(post);
            Context.SaveChanges();
        }
    }
}
