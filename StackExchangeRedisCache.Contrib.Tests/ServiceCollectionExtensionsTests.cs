using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NSubstitute;
using StackExchange.Redis;
using StackExchangeRedisCache.Contrib.Internal;

namespace StackExchangeRedisCache.Contrib.Tests;

/// <summary>
/// Tests for <see cref="ServiceCollectionExtensions.AddStackExchangeRedisCache"/>.
/// <para>
/// These tests do not require a running Redis server; they verify the DI registration logic
/// using substituted <see cref="IConnectionMultiplexer"/> instances.
/// </para>
/// </summary>
public class ServiceCollectionExtensionsTests
{
    /// <summary>
    /// Guards against double-wrapping: if the caller's <c>ConnectionMultiplexerFactory</c> already
    /// returns a <see cref="WrappedConnectionMultiplexer"/> (e.g. the service is registered twice, or
    /// the factory is intentionally pre-wrapped), the PostConfigure hook must detect this and return
    /// the existing wrapper unchanged rather than nesting a second one.
    /// <para>
    /// Double-wrapping would cause every <see cref="ICommandFlagsTweaker"/> method to be called twice
    /// per operation — once by each layer — silently producing incorrect flag values for non-idempotent
    /// tweaker implementations.
    /// </para>
    /// </summary>
    [Fact]
    public async Task AddStackExchangeRedisCache_DoesNotDoubleWrap_WhenFactoryAlreadyReturnsWrappedMultiplexer()
    {
        // Arrange: simulate a caller that has already wrapped the multiplexer before registration.
        var services = new ServiceCollection();
        var innerMux = Substitute.For<IConnectionMultiplexer>();
        var tweaker = NullCommandFlagsTweaker.Instance;
        var preWrapped = new WrappedConnectionMultiplexer(innerMux, tweaker);

        services.AddStackExchangeRedisCache(rco =>
        {
            rco.ConnectionMultiplexerFactory = () => Task.FromResult<IConnectionMultiplexer>(preWrapped);
            rco.InstanceName = "Test";
        }, tweaker);

        await using var sp = services.BuildServiceProvider();

        // Act: resolve the post-configured options and invoke the factory.
        // PostConfigure has already run at this point, so the factory is the wrapped version.
        var options = sp.GetRequiredService<IOptions<RedisCacheOptions>>().Value;
        var mux = await options.ConnectionMultiplexerFactory!();

        // Assert: the factory must hand back the original pre-wrapped instance, not a new outer wrapper.
        Assert.Same(preWrapped, mux);
    }
}
