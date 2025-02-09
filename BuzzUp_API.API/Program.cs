using BuzzUp_API.API;
using BuzzUp_API.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var settings = new AppSettings();

builder.Configuration.Bind(settings); //mapira podatke iz appsettings.json u objekat settings

builder.Services.AddSingleton(settings.Jwt);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//context
builder.Services.AddTransient<BuzzUpContext>(x => new BuzzUpContext(settings.ConnectionString));

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAllOrigins");

app.UseStaticFiles();

app.Run();
