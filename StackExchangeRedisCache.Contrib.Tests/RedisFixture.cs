using Testcontainers.Redis;
using Xunit;

namespace StackExchangeRedisCache.Contrib.Tests;

/// <summary>Single Redis instance for integration tests (Docker via Testcontainers).</summary>
public sealed class RedisFixture : IAsyncLifetime
{
    private RedisContainer? _redis;

    /// <summary>Connection string accepted by <see cref="StackExchange.Redis.ConnectionMultiplexer.Connect(string)"/>.</summary>
    public string ConnectionString { get; private set; } = "";

    public async Task InitializeAsync()
    {
        _redis = new RedisBuilder().WithImage("redis:7.2-bookworm").Build();
        await _redis.StartAsync().ConfigureAwait(false);
        ConnectionString = _redis.GetConnectionString();
    }

    public async Task DisposeAsync()
    {
        if (_redis is not null)
        {
            await _redis.DisposeAsync().ConfigureAwait(false);
        }
    }
}
