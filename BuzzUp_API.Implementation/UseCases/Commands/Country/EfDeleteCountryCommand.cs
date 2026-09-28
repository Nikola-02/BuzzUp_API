using BuzzUp_API.Application.UseCases.Commands.Country;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.UseCases;

namespace BuzzUp_API.Implementation.UseCases.Commands.Country
{
    public class EfDeleteCountryCommand : EfDeleteUseCase<Domain.Country>, IDeleteCountryCommand
    {
        public EfDeleteCountryCommand(BuzzUpContext context) : base(context)
        {
        }

        public override int Id => 37;

        public override string Name => "Delete Country";
    }
}
