using System.Linq;
using System.Reflection;
using FuturiceCalc.Services;
using FuturiceCalc.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace FuturiceCalc.Infrastructure.Extensions
{
    /// <summary>
    /// extension method to register services, so startup is not over populated
    /// </summary>
    public static class ConfigureServicesExtensions
    {
        /// <summary>
        /// registers swagger services
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Futurice Calculus", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(xmlFile);
            });
        }

        /// <summary>
        /// registers custom application services
        /// </summary>
        /// <param name="services"></param>
        public static void AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICalculusService, CalculusService>();
            services.AddScoped<ICalculusServiceV2, CalculusServiceV2>();
            services.AddScoped<IOperatorValidationService, OperatorValidationService>();
        }
    }
}
