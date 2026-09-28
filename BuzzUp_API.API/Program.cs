using BuzzUp_API.API;
using BuzzUp_API.API.Core;
using BuzzUp_API.API.Hubs;
using BuzzUp_API.Application;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Implementation;
using BuzzUp_API.Implementation.Validators.User;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var settings = new AppSettings();

builder.Configuration.Bind(settings); //mapira podatke iz appsettings.json u objekat settings

builder.Services.AddSingleton(settings);
builder.Services.AddSingleton(settings.Jwt);
builder.Services.AddSingleton(settings.Email);
builder.Services.AddSingleton(settings.Frontend);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//context
builder.Services.AddTransient<BuzzUpContext>(x => new BuzzUpContext(settings.ConnectionString));
builder.Services.AddScoped<IDbConnection>(x => new SqlConnection(settings.ConnectionString));
builder.Services.AddTransient<JwtTokenCreator>();

//Validators
builder.Services.AddValidatorsFromAssemblyContaining<UserInsertValidator>();

builder.Services.AddUseCases();
//builder.Services.AddAutoMapperProfiles();
builder.Services.AddAutoMapper(typeof(UseCaseInfo).Assembly);

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IExceptionLogger, DbExceptionLogger>();
builder.Services.AddTransient<ITokenStorage, InMemoryTokenStorage>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        cors =>
        {
            cors.WithOrigins("http://localhost:8080", "http://localhost:8081")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});

builder.Services.AddTransient<IApplicationActorProvider>(x =>
{
    var accessor = x.GetService<IHttpContextAccessor>();

    var request = accessor.HttpContext.Request;

    var authHeader = request.Headers.Authorization.ToString();

    var context = x.GetService<BuzzUpContext>();

    return new JwtApplicationActorProvider(authHeader);
});
builder.Services.AddTransient<IApplicationActor>(x =>
{
    var accessor = x.GetService<IHttpContextAccessor>();
    if (accessor.HttpContext == null)
    {
        return new UnauthorizedActor();
    }

    return x.GetService<IApplicationActorProvider>().GetActor();
});

builder.Services.AddSignalR();
builder.Services.AddSingleton<IUserIdProvider, ChatUserIdProvider>();
builder.Services.AddTransient<IChatRealtimeNotifier, ChatRealtimeNotifier>();
builder.Services.AddTransient<INotificationRealtimeNotifier, NotificationRealtimeNotifier>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = settings.Jwt.Issuer,
        ValidateIssuer = true,
        ValidAudience = "Any",
        ValidateAudience = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Jwt.SecretKey)),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
    cfg.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var requestPath = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && requestPath.StartsWithSegments("/hubs/chat"))
            {
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var storage = context.HttpContext.RequestServices.GetService<ITokenStorage>();
            var tokenIdValue = context.SecurityToken?.Id;

            if (string.IsNullOrEmpty(tokenIdValue))
            {
                tokenIdValue = context.Principal?.FindFirst(JwtRegisteredClaimNames.Jti)?.Value
                    ?? context.Principal?.FindFirst("jti")?.Value;
            }

            if (string.IsNullOrEmpty(tokenIdValue) ||
                !Guid.TryParse(tokenIdValue, out var tokenId) ||
                storage == null ||
                !storage.Exists(tokenId))
            {
                context.Fail("Invalid token");
            }

            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

using (var startupScope = app.Services.CreateScope())
{
    var db = startupScope.ServiceProvider.GetRequiredService<BuzzUpContext>();
    var usersStillMarkedOnline = db.Users.Where(user => user.IsOnline).ToList();
    foreach (var user in usersStillMarkedOnline)
    {
        user.IsOnline = false;
    }
    if (usersStillMarkedOnline.Count > 0)
    {
        db.SaveChanges();
    }
}

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/hubs/chat");

app.UseStaticFiles();

app.Run();
