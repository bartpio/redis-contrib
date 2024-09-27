using Microsoft.Extensions.Caching.StackExchangeRedis;
using StackExchange.Redis;
using StackExchangeRedisCache.Contrib;
using StackExchangeRedisCache.Contrib.Internal;

[assembly: System.Runtime.CompilerServices.InternalsVisibleToAttribute("StackExchangeRedisCache.Contrib.Tests")]

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStackExchangeRedisCache(this IServiceCollection services, Action<RedisCacheOptions> setupAction, ICommandFlagsTweaker tweaker)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(setupAction);
        ArgumentNullException.ThrowIfNull(tweaker);

        services.AddStackExchangeRedisCache(setupAction);

        services.PostConfigure<RedisCacheOptions>(rco =>
        {
            // original factory only present if the consuming app specified a custom factory, which is atypical
            var originalFactory = rco.ConnectionMultiplexerFactory;
            rco.ConnectionMultiplexerFactory = WrappedMultiplexerFactory;

            async Task<IConnectionMultiplexer> WrappedMultiplexerFactory()
            {
                var factory = originalFactory ?? DefaultMultiplexerFactory;
                var multiplexer = await factory().ConfigureAwait(false);
                return multiplexer is WrappedConnectionMultiplexer ? multiplexer : new WrappedConnectionMultiplexer(multiplexer, tweaker);
            }

            async Task<IConnectionMultiplexer> DefaultMultiplexerFactory()
            {
                return await ConnectionMultiplexer.ConnectAsync(rco.GetConfiguredOptions()).ConfigureAwait(false);
            }
        });

        return services;
    }
}
