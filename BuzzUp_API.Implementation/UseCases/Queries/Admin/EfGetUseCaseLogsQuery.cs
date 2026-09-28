using AutoMapper;
using BuzzUp_API.Application.DTO;
using BuzzUp_API.Application.DTO.Admin;
using BuzzUp_API.Application.UseCases.Queries.Admin;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using BuzzUp_API.Implementation;

namespace BuzzUp_API.Implementation.UseCases.Queries.Admin
{
    public class EfGetUseCaseLogsQuery : EfUseCaseMapper, IGetUseCaseLogsQuery
    {
        public EfGetUseCaseLogsQuery(BuzzUpContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public int Id => 51;

        public string Name => "Search Use Case Logs";

        public PagedResponse<UseCaseLogDTO> Execute(UseCaseLogSearch search)
        {
            var query = Context.UseCaseLogs.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                var keyword = search.Keyword;
                query = query.Where(useCaseLog =>
                    useCaseLog.Username.Contains(keyword) ||
                    useCaseLog.UseCaseName.Contains(keyword) ||
                    (useCaseLog.UseCaseData != null && useCaseLog.UseCaseData.Contains(keyword)));
            }

            return query
                .OrderByDescending(useCaseLog => useCaseLog.ExecutedAt)
                .AsPagedReponse<UseCaseLog, UseCaseLogDTO>(search, Mapper);
        }
    }
}
