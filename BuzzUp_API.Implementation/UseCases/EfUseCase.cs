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
        private readonly BuzzUpContext _context;


        protected EfUseCase(BuzzUpContext context)
        {
            _context = context;

        }

        protected BuzzUpContext Context => _context;

    }
}
