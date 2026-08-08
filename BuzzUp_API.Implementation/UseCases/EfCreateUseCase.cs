using AutoMapper;
using BuzzUp_API.Application.Exceptions;
using BuzzUp_API.Application.UseCases;
using BuzzUp_API.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.UseCases
{
    public abstract class EfCreateUseCase<TDto, TEntity> : EfUseCase, ICommand<TDto>
        where TEntity : class
        where TDto : class
    {
        private readonly IMapper _mapper;
        private readonly IValidator<TDto> _validator;

        protected EfCreateUseCase(BuzzUpContext context, IMapper mapper, IValidator<TDto> validator) : base(context)
        {
            _mapper = mapper;
            _validator = validator;
        }

        public abstract int Id { get; }
        public abstract string Name { get; }

        public void Execute(TDto request)
        {
            _validator.ValidateAndThrow(request);

            var entity = _mapper.Map<TEntity>(request);

            Context.Set<TEntity>().Add(entity);

            Context.SaveChanges();
        }
    }
}
