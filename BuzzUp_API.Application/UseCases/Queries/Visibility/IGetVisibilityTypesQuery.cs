using BuzzUp_API.Application.DTO;
using BuzzUp_API.Application.UseCases;

namespace BuzzUp_API.Application.UseCases.Queries.Visibility
{
    public interface IGetVisibilityTypesQuery : IQuery<List<LookupMiniDTO>, TablesSearch>
    {
    }
}
