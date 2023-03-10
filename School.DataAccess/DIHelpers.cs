using Dapper;
using Microsoft.Extensions.DependencyInjection;
using School.DataAccess.Services;
using System.Reflection;

namespace School.DataAccess;

/// <summary>
/// 
/// </summary>
public static class DIHelpers
{
    public static void AddDataAccess(this IServiceCollection services)
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        string? servicesNamespace = typeof(MarkerService).Namespace;

        IEnumerable<Type> servicesTypes = from t in Assembly.GetAssembly(typeof(MarkerService))?.GetTypes()
                                          where t.IsClass && t.Namespace == servicesNamespace
                                          select t;

        foreach (Type serviceType in servicesTypes)
        {
            //assumption --> only one interface, then registration possible
            Type[] contractTypes = serviceType.GetInterfaces();
            if (contractTypes.Length == 1)
            {
                services.AddScoped(contractTypes[0], serviceType);
            }

        }

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    }
}
