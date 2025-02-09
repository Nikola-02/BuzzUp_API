using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;

namespace BuzzUp_API.API.Core
{
    public class DbExceptionLogger
    {
        private readonly BuzzUpContext _buzzUpContext;

        public DbExceptionLogger(BuzzUpContext buzzUpContext)
        {
            _buzzUpContext = buzzUpContext;
        }

        public Guid Log(Exception ex, IApplicationActor actor)
        {
            Guid id = Guid.NewGuid();

            //ID, Message, Time, StrackTrace
            ErrorLog log = new()
            {
                ErrorId = id,
                Message = ex.Message,
                StrackTrace = ex.StackTrace,
                Time = DateTime.UtcNow
            };

            _buzzUpContext.ErrorLogs.Add(log);

            _buzzUpContext.SaveChanges();

            return id;
        }
    }
}
