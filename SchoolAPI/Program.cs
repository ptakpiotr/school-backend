using Coravel;
using Newtonsoft.Json.Serialization;
using School.DataAccess;
using School.DataAccess.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration = builder.Configuration;

//setting up Serilog - structured logging framework
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();
builder.Host.UseSerilog();

IServiceCollection services = builder.Services;
// Add services to the container.ss

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddScheduler();
services.AddMvc().AddNewtonsoftJson(opts =>
{
    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});
services.AddDataAccess();

services.AddCors((opts) =>
{
    opts.AddPolicy("AllowAnyonePolicy", (policyOpts) =>
    {
        policyOpts.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});

//configuration for options pattern: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-7.0
services.Configure<ConnectionStringOptions>(Configuration.GetSection("ConnectionStrings"));

var app = builder.Build();
app.Services.UseScheduler((opts) =>
{
    opts.Schedule(() => { Console.WriteLine("HELLO"); }).EveryMinute();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyonePolicy");
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
