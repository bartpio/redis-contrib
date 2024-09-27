using StackExchange.Redis;

namespace StackExchangeRedisCache.Contrib;

/// <summary>
/// Implementations of this interface can be used to modify the Redis <see cref="CommandFlags"/>
/// used during <see cref="Microsoft.Extensions.Caching.StackExchangeRedis.RedisCache"/> operations
/// via <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/>.
/// Note that when getting a value from the cache for which sliding expiration has been set:
///  - <see cref="TweakGetType(CommandFlags, RedisKey)"/> is applicable while actually getting the value
///  - No tweaking is done while resetting the sliding expiration
/// </summary>
public interface ICommandFlagsTweaker
{
    /// <summary>
    /// Tweak <see cref="CommandFlags"/> during read commands.
    /// </summary>
    /// <param name="flags">
    /// Original flags, as presented by <see cref="Microsoft.Extensions.Caching.StackExchangeRedis.RedisCache"/>.
    /// Generally equal to <see cref="CommandFlags.None"/>.
    /// </param>
    /// <param name="key">
    /// Redis cache key for the operation. Note that the InstanceName (if present) is prefixed onto the key.
    /// </param>
    /// <returns><see cref="CommandFlags"/> to use during the command.</returns>
    /// <remarks>
    /// For example, an implementation could return <see cref="CommandFlags.PreferReplica"/> to engage a Redis read replica.
    /// </remarks>
    CommandFlags TweakGetType(CommandFlags flags, RedisKey key);

    /// <summary>
    /// Tweak <see cref="CommandFlags"/> during write commands.
    /// </summary>
    /// <param name="flags">
    /// Original flags, as presented by <see cref="Microsoft.Extensions.Caching.StackExchangeRedis.RedisCache"/>.
    /// Generally equal to <see cref="CommandFlags.None"/>.
    /// </param>
    /// <param name="key">
    /// Redis cache key for the operation. Note that the InstanceName (if present) is prefixed onto the key.
    /// May be default(RedisKey) when the cache key is not available.
    /// </param>
    /// <returns><see cref="CommandFlags"/> to use during the command.</returns>
    /// <remarks>
    /// For example, an implementation could return <see cref="CommandFlags.FireAndForget"/>.
    /// </remarks>
    CommandFlags TweakSetType(CommandFlags flags, RedisKey key);
}

/// <summary>
/// Implementation of <see cref="ICommandFlagsTweaker"/> that does not actually tweak anything.
/// </summary>
public sealed class NullCommandFlagsTweaker : ICommandFlagsTweaker
{
    /// <summary>
    /// Provides convenient access to an instance of <see cref="NullCommandFlagsTweaker"/>.
    /// </summary>
    public static NullCommandFlagsTweaker Instance { get; } = new();

    public CommandFlags TweakGetType(CommandFlags flags, RedisKey key) => flags;

    public CommandFlags TweakSetType(CommandFlags flags, RedisKey key) => flags;
}
