using BuzzUp_API.Application.DTO.Admin;
using BuzzUp_API.Application.UseCases;

namespace BuzzUp_API.Application.UseCases.Queries.Admin
{
    public interface IGetAdminDashboardStatsQuery : IQuery<AdminDashboardStatsDTO, AdminDashboardStatsSearch>
    {
    }
}
