using AutoMapper;
using BuzzUp_API.Application.DTO;
using BuzzUp_API.Application.Exceptions;
using BuzzUp_API.Application.Repository;
using BuzzUp_API.Application.UseCases;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.UseCases
{
    public abstract class EfUpdateUseCase<TDto, TEntity>
    : EfUseCase<TEntity>, ICommand<TDto>
    where TEntity : class
    where TDto : IUpdateDTO
    {
        private readonly IMapper _mapper;
        private readonly IValidator<TDto> _validator;

        protected EfUpdateUseCase(IRepository<TEntity> repository, IMapper mapper, IValidator<TDto> validator)
            : base(repository)
        {
            _mapper = mapper;
            _validator = validator;
        }

        public abstract int Id { get; }
        public abstract string Name { get; }

        public void Execute(TDto request)
        {
            _validator.ValidateAndThrow(request);

            var entity = Repository.Find(request.Id.Value);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity).Name, request.Id.Value);
            }

            _mapper.Map(request, entity);

            Context.SaveChanges();
        }
    }
}
