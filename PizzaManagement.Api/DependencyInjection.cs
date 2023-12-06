using Microsoft.AspNetCore.Mvc.Infrastructure;

using PizzaManagement.Api.Common;
using PizzaManagement.Api.Common.Errors;

namespace PizzaManagement.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddSingleton<ProblemDetailsFactory, PizzaManagementProblemDetailFactory>();
            services.AddMappings();
            services.AddCors(options => options.AddPolicy(name: "CorsPolicy", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            return services;
        }
    }
}