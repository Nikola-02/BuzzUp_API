using AutoMapper;
using BuzzUp_API.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.UseCases
{
    public class EfUseCaseMapper : EfUseCase
    {
        private readonly IMapper _mapper;

        public EfUseCaseMapper(BuzzUpContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        protected IMapper Mapper => _mapper;
    }
}
