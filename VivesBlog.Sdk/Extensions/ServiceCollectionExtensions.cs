using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VivesBlog.Sdk.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services, string apiUrl)
        {
            services.AddHttpClient("VivesBlogApi", options =>
            {
                options.BaseAddress = new Uri(apiUrl);
            });

            services.AddScoped<ArticleSdk>();
            services.AddScoped<PersonSdk>();

            return services;
        }
    }
}
