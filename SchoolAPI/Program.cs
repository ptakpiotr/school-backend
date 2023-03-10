using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using SchoolAPI.Filters;
using SchoolAPI.Validation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration = builder.Configuration;

//setting up Serilog - structured logging framework
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();
builder.Host.UseSerilog();

IServiceCollection services = builder.Services;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddScoped<GlobalExceptionFilter>();

// Adding non-default serializer -> needed for PATCH functionality
services.AddMvc(opts =>
{
    opts.Filters.Add<GlobalExceptionFilter>();
}).AddNewtonsoftJson(opts =>
{
    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});

// Registering services and their implementations from DataAccess project
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
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseCors("AllowAnyonePolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
