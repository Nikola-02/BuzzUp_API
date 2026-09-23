using AutoMapper;
using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO;
using BuzzUp_API.Application.DTO.Users;
using BuzzUp_API.Application.UseCases.Queries.Users;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BuzzUp_API.Implementation.UseCases.Queries.Users
{
    public class EfGetUsersQuery : EfUseCaseMapper, IGetUsersQuery
    {
        private readonly IApplicationActor _actor;

        public EfGetUsersQuery(BuzzUpContext context, IMapper mapper, IApplicationActor actor) : base(context, mapper)
        {
            _actor = actor;
        }

        public int Id => 6;

        public string Name => "Search Users";

        public PagedResponse<UserMiniDTO> Execute(UserSearch search)
        {
            var query = Context.Users
                                    .Where(x => x.IsActive && x.DeletedAt == null)
                                    .AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Username.ToLower().Contains(search.Keyword.ToLower()) ||
                                         x.FirstName.ToLower().Contains(search.Keyword.ToLower()) ||
                                         x.LastName.ToLower().Contains(search.Keyword.ToLower()) ||
                                         x.Email.ToLower().Contains(search.Keyword.ToLower()));
            }

            var result = query.AsPagedReponse<User, UserMiniDTO>(search, Mapper);

            if (!(_actor.Role == "Admin" && search.AdminView))
            {
                foreach (var dto in result.Data)
                {
                    if (ShouldHidePrivateDetails(dto))
                    {
                        UserPrivacy.HidePrivateDetails(dto);
                    }
                }
            }

            return result;
        }

        private bool ShouldHidePrivateDetails(UserMiniDTO dto)
        {
            if (!dto.IsPrivate || _actor.Id == 0 || _actor.Id == dto.Id)
            {
                return false;
            }

            return !Context.Friendships.Any(f =>
                f.IsActive &&
                f.DeletedAt == null &&
                f.FriendRequestStatus.Name == "Accepted" &&
                ((f.SenderUserId == _actor.Id && f.ReceiverUserId == dto.Id) ||
                 (f.ReceiverUserId == _actor.Id && f.SenderUserId == dto.Id)));
        }
    }
}
