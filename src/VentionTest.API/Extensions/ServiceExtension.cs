using VentionTest.DAL.IRepositories;
using VentionTest.DAL.Repositories;
using VentionTest.Service.Interfaces;
using VentionTest.Service.Services;

namespace VentionTest.API.Extensions
{
    public static class ServiceExtension
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<ICapitalService, CapitalService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<ILanguageCountryService, LanguageCountryService>();
        }
    }
}
