using Microsoft.Extensions.DependencyInjection;

namespace Tests.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ReplaceWithSingleton<T>(this IServiceCollection collection, T with)
        where T : class
    {
        var existingRepository = collection
            .FirstOrDefault(descriptor => descriptor.ServiceType == typeof(T));

        if (existingRepository is not null)
        {
            collection.Remove(existingRepository);
        }
                    
        collection.AddSingleton(with);
    }
}