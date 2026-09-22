using AutoMapper;
using BuzzUp_API.Application.DTO;
using BuzzUp_API.Application.Exceptions;
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
    public abstract class EfUpdateUseCase<TDto, TEntity> : EfUseCase, ICommand<TDto>
        where TEntity : Entity
        where TDto : IUpdateDTO
    {
        private readonly IMapper _mapper;
        private readonly IValidator<TDto> _validator;

        protected EfUpdateUseCase(BuzzUpContext context, IMapper mapper, IValidator<TDto> validator)
            : base(context)
        {
            _mapper = mapper;
            _validator = validator;
        }

        public abstract int Id { get; }
        public abstract string Name { get; }

        public void Execute(TDto request)
        {
            _validator.ValidateAndThrow(request);

            var entity = Context.Set<TEntity>().FirstOrDefault(x => x.Id == request.Id.Value && x.IsActive && x.DeletedAt == null);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity).Name, request.Id.Value);
            }

            EnsureCanUpdate(request, entity);

            _mapper.Map(request, entity);

            AfterUpdate(request, entity);

            Context.SaveChanges();
        }

        protected virtual void EnsureCanUpdate(TDto request, TEntity entity)
        {
        }

        protected virtual void AfterUpdate(TDto request, TEntity entity)
        {
        }
    }
}
