# StackExchangeRedisCache.Contrib

## Overview

Provides additional functionality pertaining to `Microsoft.Extensions.Caching.StackExchangeRedis` (the Redis-backed implementation of `IDistributedCache` using the `StackExchange.Redis` client).

## Redis Command Flags

### What?

Library users may:
 - Implement `ICommandFlagsTweaker` to modify the Redis `CommandFlags` used with certain Redis commands during cache operations.
 - Take advantage of `WithReplacementReplicaFlags` and `WithoutReplicaFlags` `CommandFlags` extension methods to modify command flags that control replica selection

### Why?

This library facilitates scenarios such as:

 - Using a read replica for read commands (`CommandFlags.PreferReplica`)
 - Using Fire-and-Forget mode for write commands (`CommandFlags.FireAndForget`)

### How?

This library provides an overload of the `AddStackExchangeRedisCache` extension method that accepts an additional argument of type `ICommandFlagsTweaker`. To use it, write an implementation of `ICommandFlagsTweaker`, and supply an instance of your implementation to the provided extension method (instead of calling the base `AddStackExchangeRedisCache` extension method). `NullCommandFlagsTweaker.Instance` may be supplied when no tweaking is desired.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddStackExchangeRedisCache(rco =>
    {
        rco.ConfigurationOptions = new()
        {
            EndPoints = new()
            {
                "some primary endpoint",
                "some reader (replica) endpoint",
            }
        };

        rco.InstanceName = "some instance name";
    }, MyCommandFlagsTweaker.Instance);
}

private sealed class MyCommandFlagsTweaker : ICommandFlagsTweaker
{
    public static MyCommandFlagsTweaker Instance { get; } = new();

    // set flags for Read commands
    public CommandFlags TweakGetType(CommandFlags flags, RedisKey key) => flags.WithReplacementReplicaFlags(CommandFlags.PreferReplica);

    // set flags for Write commands
    public CommandFlags TweakSetType(CommandFlags flags, RedisKey key) => flags | CommandFlags.FireAndForget;
}
```

### Sliding Expiration

When getting a value from the cache for which sliding expiration has been set (`DistributedCacheEntryOptions.SlidingExpiration`), the base library performs two Redis commands. First, the value (and associated entry options) are retrieved. `TweakGetType` is applicable during this retrieval. Then, a second command is issued to update the expiration. No tweaking is applied during the expiration update. A mechanism to tweak the expiration command may be provided in a future version of this library.

Sliding expiration support when the retrieval command is tweaked to use a replica is somewhat experimental.

### Deletion and Refresh

No tweaking is applied during these distributed cache operations. A mechanism to tweak command flags during these operations may be provided in a future version of this library.
