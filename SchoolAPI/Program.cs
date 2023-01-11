using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using School.DataAccess;
using School.DataAccess.Models;
using SchoolAPI.Validation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration = builder.Configuration;

//setting up Serilog - structured logging framework
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();
builder.Host.UseSerilog();

IServiceCollection services = builder.Services;
// Add services to the container.ss

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

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
services.Configure<JWTOptions>(Configuration.GetSection("Jwt"));

services.AddFluentValidationAutoValidation(options =>
{
    options.ImplicitlyValidateChildProperties = true;
    options.ImplicitlyValidateRootCollectionElements = true;
}).AddValidatorsFromAssemblyContaining<UserValidator>();

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer((opts) =>
{
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = Configuration.GetSection("Jwt:Issuer").Value,
        ValidAudience = Configuration.GetSection("Jwt:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration.GetSection("Jwt:Key").Value))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyonePolicy");
app.UseHttpsRedirection();
app.UseSerilogRequestLogging();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
