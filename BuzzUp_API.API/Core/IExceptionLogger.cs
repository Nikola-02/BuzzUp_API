using BuzzUp_API.Application;

namespace BuzzUp_API.API.Core
{
    public interface IExceptionLogger
    {
        Guid Log(Exception ex, IApplicationActor actor);
    }
}
