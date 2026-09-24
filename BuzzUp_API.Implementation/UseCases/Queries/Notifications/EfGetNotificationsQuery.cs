using AutoMapper;
using AutoMapper.QueryableExtensions;
using BuzzUp_API.Application;
using BuzzUp_API.Application.DTO.Notifications;
using BuzzUp_API.Application.UseCases.Queries.Notifications;
using BuzzUp_API.DataAccess;

namespace BuzzUp_API.Implementation.UseCases.Queries.Notifications
{
    public class EfGetNotificationsQuery : EfUseCaseMapper, IGetNotificationsQuery
    {
        private readonly IApplicationActor _actor;

        public EfGetNotificationsQuery(BuzzUpContext context, IMapper mapper, IApplicationActor actor) : base(context, mapper)
        {
            _actor = actor;
        }

        public int Id => 22;

        public string Name => "Get Notifications";

        public List<NotificationDTO> Execute(NotificationSearch search)
        {
            var actorId = _actor.Id;

            return Context.Notifications
                .Where(n => n.IsActive && n.DeletedAt == null)
                .Where(n => n.RecipientUserId == actorId)
                .Where(n => n.Actor.IsActive && n.Actor.DeletedAt == null)
                .OrderByDescending(n => n.CreatedAt)
                .ProjectTo<NotificationDTO>(Mapper.ConfigurationProvider)
                .ToList();
        }
    }
}
