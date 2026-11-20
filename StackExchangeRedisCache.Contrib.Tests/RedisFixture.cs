using DotNet.Testcontainers.Configurations;
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
        // Ryuk bind-mounts the Docker socket; that fails when the daemon is reached over TCP
        // (e.g. tcp://127.0.0.1:23750) or when Git Bash rewrites Unix socket paths on Windows.
        // Fixture DisposeAsync still stops and removes the Redis container.
        TestcontainersSettings.ResourceReaperEnabled = false;

        _redis = new RedisBuilder("redis:7.2-bookworm").Build();
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
