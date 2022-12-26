using Kinfo.JsonStore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Kinfo.JsonStore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IDynamicAuthorizationBuilder AddDynamicAuthorization(this IServiceCollection services,
            Action<DynamicAuthorizationOptions> options)
           
        {
            var dynamicAuthorizationOptions = new DynamicAuthorizationOptions();
            options.Invoke(dynamicAuthorizationOptions);
            services.AddSingleton(dynamicAuthorizationOptions);

           
            services.AddSingleton<IMvcControllerDiscovery, MvcControllerDiscovery>();

            IDynamicAuthorizationBuilder builder = new DynamicAuthorizationBuilder(services);

          //  DynamicAuthorizationOptions.DbContextType = typeof(TDbContext);

            return builder;
        }
    }
}