using BuzzUp_API.Application.DTO.Feelings;
using BuzzUp_API.Application.UseCases;

namespace BuzzUp_API.Application.UseCases.Commands.Feelings
{
    public interface ICreateFeelingTypeCommand : ICommand<FeelingTypeInsertDTO>
    {
    }
}
