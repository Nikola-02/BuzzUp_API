using BuzzUp_API.Application;
using BuzzUp_API.Implementation.Logging.UseCases;
using BuzzUp_API.Implementation;
using System.IdentityModel.Tokens.Jwt;
using BuzzUp_API.Application.UseCases.Commands.Account;
using BuzzUp_API.Implementation.UseCases.Commands.Account;
using BuzzUp_API.Application.DTO.Users;
using BuzzUp_API.Implementation.Validators.User;
using static BuzzUp_API.Implementation.Validators.User.BaseUserValidator;

namespace BuzzUp_API.API.Core
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            //Use case handler
            services.AddTransient<UseCaseHandler>();

            //Logger
            services.AddTransient<IUseCaseLogger, SPUseCaseLogger>();

            //Queries
            //services.AddTransient<IGetGenresQuery, EfGetGenresQuery>();
            
            //Commands
            //User
            services.AddTransient<IRegisterUserCommand, RegisterUserCommand>();

            //Validators
            services.AddTransient<BaseUserValidator>();
            services.AddTransient<UserInsertValidator>();
            services.AddTransient<UserUpdateValidator>();
        }

        public static void AddAutoMapperProfiles(this IServiceCollection services)
        {
            //Profiles
            //services.AddAutoMapper(typeof(FileTypeProfile));
        }
        public static Guid? GetTokenId(this HttpRequest request)
        {
            if (request == null || !request.Headers.ContainsKey("Authorization"))
            {
                return null;
            }

            string authHeader = request.Headers["Authorization"].ToString();

            if (authHeader.Split("Bearer ").Length != 2)
            {
                return null;
            }

            string token = authHeader.Split("Bearer ")[1];

            var handler = new JwtSecurityTokenHandler();

            var tokenObj = handler.ReadJwtToken(token);

            var claims = tokenObj.Claims;

            var claim = claims.First(x => x.Type == "jti").Value;

            var tokenGuid = Guid.Parse(claim);

            return tokenGuid;
        }
    }
}
