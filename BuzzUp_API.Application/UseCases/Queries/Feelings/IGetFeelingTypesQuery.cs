using BuzzUp_API.Application.DTO.Feelings;
using BuzzUp_API.Application.UseCases;

namespace BuzzUp_API.Application.UseCases.Queries.Feelings
{
    public interface IGetFeelingTypesQuery : IQuery<List<FeelingTypeDTO>, FeelingTypeSearch>
    {
    }
}
