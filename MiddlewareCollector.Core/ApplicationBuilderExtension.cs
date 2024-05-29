using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using MiddlewareCollector.Abstract;

namespace MiddlewareCollector.Core
{
    public static class ApplicationBuilderExtension
    {
        public static void MiddlewareCollector(this IApplicationBuilder applicationBuilder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
            var indicator = typeof(IMiddlewareCollector);
        
            var types = assemblies.SelectMany(x => x.DefinedTypes);
            var serviceDiscoveries = types.Where(typeInfo => typeInfo.ImplementedInterfaces.Contains(indicator));

            foreach (var typeInfo in serviceDiscoveries)
            {
                applicationBuilder.UseMiddleware(typeInfo);
            }
        }
    }
}