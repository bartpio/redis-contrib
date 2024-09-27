// Portions used under MIT license from the .NET Foundation

using Microsoft.Extensions.Caching.StackExchangeRedis;
using StackExchange.Redis;

namespace StackExchangeRedisCache.Contrib.Internal;

internal static class ConfigurationOptionsExtensions
{
    internal static ConfigurationOptions GetConfiguredOptions(this RedisCacheOptions rco)
    {
        var options = rco.ConfigurationOptions ?? ConfigurationOptions.Parse(rco.Configuration!);

        // we don't want an initially unavailable server to prevent DI creating the service itself
        options.AbortOnConnectFail = false;

        return options;
    }
}
