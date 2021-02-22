using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Abstractions;
using Service;
using Service.Abstractions;
using Service.Helpers;
using Service.Helpers.Interfaces;

namespace Infrastructure.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterCustomServices(this IServiceCollection services, string path)
        {
            services.AddScoped<IShowRepository, ShowRepository>();
            services.AddScoped<IShowRateRepository, ShowRateRepository>();
            services.AddScoped<IShowTypeRepository, ShowTypeRepository>();

            services.AddScoped<IShowService, ShowService>();
            services.AddScoped<IShowRateService, ShowRateService>();
            services.AddScoped<IShowTypeService, ShowTypeService>();

            services.AddSingleton<IImageHelper>(serviceProvider => new ImageHelper(path));
            services.AddSingleton<ISearchEngineHelper, SearchEngineHelper>();

            return services;
        }
    }
}