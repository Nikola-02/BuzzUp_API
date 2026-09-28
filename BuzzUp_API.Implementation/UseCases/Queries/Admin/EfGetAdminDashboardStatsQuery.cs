using BuzzUp_API.Application.DTO.Admin;
using BuzzUp_API.Application.UseCases.Queries.Admin;
using BuzzUp_API.DataAccess;

namespace BuzzUp_API.Implementation.UseCases.Queries.Admin
{
    public class EfGetAdminDashboardStatsQuery : EfUseCase, IGetAdminDashboardStatsQuery
    {
        public EfGetAdminDashboardStatsQuery(BuzzUpContext context) : base(context)
        {
        }

        public int Id => 50;

        public string Name => "Get Admin Dashboard Stats";

        public AdminDashboardStatsDTO Execute(AdminDashboardStatsSearch search)
        {
            return new AdminDashboardStatsDTO
            {
                TotalUsers = Context.Users.Count(user => user.IsActive && user.DeletedAt == null),
                TotalPosts = Context.Posts.Count(post => post.IsActive && post.DeletedAt == null)
            };
        }
    }
}
