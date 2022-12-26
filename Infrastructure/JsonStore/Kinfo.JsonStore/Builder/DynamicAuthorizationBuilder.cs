using Microsoft.Extensions.DependencyInjection;

namespace Kinfo.JsonStore.Builder
{
    internal class DynamicAuthorizationBuilder : IDynamicAuthorizationBuilder
    {
        public DynamicAuthorizationBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}