using NSubstitute;
using StackExchange.Redis;
using StackExchangeRedisCache.Contrib.Internal;

namespace StackExchangeRedisCache.Contrib.Tests;

/// <summary>
/// Unit tests for command-flag tweaking without a live Redis server.
/// </summary>
public sealed class WrappedDatabaseAndBatchFlagsTests
{
    [Fact]
    public void HashGetLease_passes_tweaked_flags_to_inner_database()
    {
        var inner = Substitute.For<IDatabase>();
        var tweaker = Substitute.For<ICommandFlagsTweaker>();
        var key = new RedisKey("cache-key");
        var field = new RedisValue("data");

        tweaker.TweakGetType(CommandFlags.PreferMaster, key).Returns(CommandFlags.DemandReplica);
        inner.HashGetLease(key, field, CommandFlags.DemandReplica).Returns((Lease<byte>?)null);

        var sut = new WrappedDatabase(inner, tweaker);
        sut.HashGetLease(key, field, CommandFlags.PreferMaster);

        tweaker.Received(1).TweakGetType(CommandFlags.PreferMaster, key);
        inner.Received(1).HashGetLease(key, field, CommandFlags.DemandReplica);
    }

    [Fact]
    public async Task HashGetLeaseAsync_passes_tweaked_flags_to_inner_database()
    {
        var inner = Substitute.For<IDatabase>();
        var tweaker = Substitute.For<ICommandFlagsTweaker>();
        var key = new RedisKey("cache-key");
        var field = new RedisValue("data");

        tweaker.TweakGetType(CommandFlags.PreferMaster, key).Returns(CommandFlags.DemandReplica);
        inner.HashGetLeaseAsync(key, field, CommandFlags.DemandReplica).Returns((Lease<byte>?)null);

        var sut = new WrappedDatabase(inner, tweaker);
        await sut.HashGetLeaseAsync(key, field, CommandFlags.PreferMaster);

        tweaker.Received(1).TweakGetType(CommandFlags.PreferMaster, key);
        await inner.Received(1).HashGetLeaseAsync(key, field, CommandFlags.DemandReplica);
    }

    [Fact]
    public void KeyDelete_passes_tweaked_flags_to_inner_database()
    {
        var inner = Substitute.For<IDatabase>();
        var tweaker = Substitute.For<ICommandFlagsTweaker>();
        var key = new RedisKey("cache-key");

        tweaker.TweakSetType(CommandFlags.None, key).Returns(CommandFlags.FireAndForget);
        inner.KeyDelete(key, CommandFlags.FireAndForget).Returns(true);

        var sut = new WrappedDatabase(inner, tweaker);
        sut.KeyDelete(key, CommandFlags.None);

        tweaker.Received(1).TweakSetType(CommandFlags.None, key);
        inner.Received(1).KeyDelete(key, CommandFlags.FireAndForget);
    }

    [Fact]
    public void KeyExpire_TimeSpan_passes_tweaked_flags_to_inner_database()
    {
        var inner = Substitute.For<IDatabase>();
        var tweaker = Substitute.For<ICommandFlagsTweaker>();
        var key = new RedisKey("cache-key");
        var expiry = TimeSpan.FromMinutes(5);

        tweaker.TweakSetType(CommandFlags.None, key).Returns(CommandFlags.FireAndForget);
        inner.KeyExpire(key, expiry, CommandFlags.FireAndForget).Returns(true);

        var sut = new WrappedDatabase(inner, tweaker);
        sut.KeyExpire(key, expiry, CommandFlags.None);

        tweaker.Received(1).TweakSetType(CommandFlags.None, key);
        inner.Received(1).KeyExpire(key, expiry, CommandFlags.FireAndForget);
    }

    [Fact]
    public async Task Batch_HashSetAsync_and_KeyExpireAsync_pass_tweaked_flags_like_RedisCache_TTL_set_path()
    {
        var inner = Substitute.For<IDatabase>();
        var innerBatch = Substitute.For<IBatch>();
        inner.CreateBatch(Arg.Any<object?>()).Returns(innerBatch);

        var tweaker = Substitute.For<ICommandFlagsTweaker>();
        var key = new RedisKey("cache-key");
        var fields = new[] { new HashEntry("h", "v") };

        tweaker.TweakSetType(CommandFlags.None, key).Returns(CommandFlags.FireAndForget);
        innerBatch.HashSetAsync(key, fields, CommandFlags.FireAndForget).Returns(Task.CompletedTask);
        innerBatch.KeyExpireAsync(key, TimeSpan.FromSeconds(30), ExpireWhen.Always, CommandFlags.FireAndForget)
            .Returns(Task.FromResult(true));

        var sut = new WrappedDatabase(inner, tweaker);
        var batch = sut.CreateBatch();

        await batch.HashSetAsync(key, fields);
        await batch.KeyExpireAsync(key, TimeSpan.FromSeconds(30));

        tweaker.Received(2).TweakSetType(CommandFlags.None, key);
        await innerBatch.Received(1).HashSetAsync(key, fields, CommandFlags.FireAndForget);
        await innerBatch.Received(1).KeyExpireAsync(key, TimeSpan.FromSeconds(30), ExpireWhen.Always, CommandFlags.FireAndForget);
    }
}
