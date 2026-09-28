using BuzzUp_API.Application.DTO;
using BuzzUp_API.Application.DTO.Admin;
using BuzzUp_API.Application.UseCases;

namespace BuzzUp_API.Application.UseCases.Queries.Admin
{
    public interface IGetUseCaseLogsQuery : IQuery<PagedResponse<UseCaseLogDTO>, UseCaseLogSearch>
    {
    }
}
