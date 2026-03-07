using BuzzUp_API.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.UseCases
{
    public abstract class EfUseCase
    {
        protected readonly BuzzUpContext Context;

        protected EfUseCase(BuzzUpContext context)
        {
            Context = context;
        }
    }
}
