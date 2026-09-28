using BuzzUp_API.Application.UseCases.Commands.Feelings;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation.UseCases;

namespace BuzzUp_API.Implementation.UseCases.Commands.Feelings
{
    public class EfDeleteFeelingTypeCommand : EfDeleteUseCase<FeelingType>, IDeleteFeelingTypeCommand
    {
        public EfDeleteFeelingTypeCommand(BuzzUpContext context) : base(context)
        {
        }

        public override int Id => 41;

        public override string Name => "Delete Feeling Type";
    }
}
