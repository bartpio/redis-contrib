#pragma warning disable SER002, SER003, SER006 // StackExchange.Redis experimental APIs surface through IBatch
#nullable disable
using StackExchange.Redis;

namespace StackExchangeRedisCache.Contrib.Internal;

/// <summary>Decorates an <see cref="IBatch"/> so <see cref="HashSetAsync"/> and <see cref="KeyExpireAsync"/> honor <see cref="ICommandFlagsTweaker"/>. </summary>
internal sealed class WrappedBatch(IBatch batch, ICommandFlagsTweaker tweaker) : IBatch
{
    private readonly IBatch _batch = batch;

    private CommandFlags TweakSetType(CommandFlags flags, RedisKey key) => tweaker.TweakSetType(flags, key);

    public void Execute() => _batch.Execute();

    public bool IsConnected(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.IsConnected(key, flags);

    public System.Threading.Tasks.Task KeyMigrateAsync(RedisKey key, System.Net.EndPoint toServer, int toDatabase = 0, int timeoutMilliseconds = 0, MigrateOptions migrateOptions = MigrateOptions.None, CommandFlags flags = CommandFlags.None) =>
        _batch.KeyMigrateAsync(key, toServer, toDatabase, timeoutMilliseconds, migrateOptions, flags);

    public Task<RedisValue> DebugObjectAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.DebugObjectAsync(key, flags);

    public Task<bool> GeoAddAsync(RedisKey key, double longitude, double latitude, RedisValue member, CommandFlags flags = CommandFlags.None) => _batch.GeoAddAsync(key, longitude, latitude, member, flags);

    public Task<bool> GeoAddAsync(RedisKey key, GeoEntry value, CommandFlags flags = CommandFlags.None) => _batch.GeoAddAsync(key, value, flags);

    public Task<long> GeoAddAsync(RedisKey key, GeoEntry[] values, CommandFlags flags = CommandFlags.None) => _batch.GeoAddAsync(key, values, flags);

    public Task<bool> GeoRemoveAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => _batch.GeoRemoveAsync(key, member, flags);

    public Task<double?> GeoDistanceAsync(RedisKey key, RedisValue member1, RedisValue member2, GeoUnit unit = GeoUnit.Meters, CommandFlags flags = CommandFlags.None) => _batch.GeoDistanceAsync(key, member1, member2, unit, flags);

    public Task<string[]> GeoHashAsync(RedisKey key, RedisValue[] members, CommandFlags flags = CommandFlags.None) => _batch.GeoHashAsync(key, members, flags);

    public Task<string> GeoHashAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => _batch.GeoHashAsync(key, member, flags);

    public Task<GeoPosition?[]> GeoPositionAsync(RedisKey key, RedisValue[] members, CommandFlags flags = CommandFlags.None) => _batch.GeoPositionAsync(key, members, flags);

    public Task<GeoPosition?> GeoPositionAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => _batch.GeoPositionAsync(key, member, flags);

    public Task<GeoRadiusResult[]> GeoRadiusAsync(
        RedisKey key,
        RedisValue member,
        double radius,
        GeoUnit unit = GeoUnit.Meters,
        int count = -1,
        Order? order = default,
        GeoRadiusOptions options = GeoRadiusOptions.Default,
        CommandFlags flags = CommandFlags.None
    ) => _batch.GeoRadiusAsync(key, member, radius, unit, count, order, options, flags);

    public Task<GeoRadiusResult[]> GeoRadiusAsync(
        RedisKey key,
        double longitude,
        double latitude,
        double radius,
        GeoUnit unit = GeoUnit.Meters,
        int count = -1,
        Order? order = default,
        GeoRadiusOptions options = GeoRadiusOptions.Default,
        CommandFlags flags = CommandFlags.None
    ) => _batch.GeoRadiusAsync(key, longitude, latitude, radius, unit, count, order, options, flags);

    public Task<GeoRadiusResult[]> GeoSearchAsync(
        RedisKey key,
        RedisValue member,
        GeoSearchShape shape,
        int count = -1,
        bool demandClosest = true,
        Order? order = default,
        GeoRadiusOptions options = GeoRadiusOptions.Default,
        CommandFlags flags = CommandFlags.None
    ) => _batch.GeoSearchAsync(key, member, shape, count, demandClosest, order, options, flags);

    public Task<GeoRadiusResult[]> GeoSearchAsync(
        RedisKey key,
        double longitude,
        double latitude,
        GeoSearchShape shape,
        int count = -1,
        bool demandClosest = true,
        Order? order = default,
        GeoRadiusOptions options = GeoRadiusOptions.Default,
        CommandFlags flags = CommandFlags.None
    ) => _batch.GeoSearchAsync(key, longitude, latitude, shape, count, demandClosest, order, options, flags);

    public Task<long> GeoSearchAndStoreAsync(
        RedisKey sourceKey,
        RedisKey destinationKey,
        RedisValue member,
        GeoSearchShape shape,
        int count = -1,
        bool demandClosest = true,
        Order? order = default,
        bool storeDistances = false,
        CommandFlags flags = CommandFlags.None
    ) => _batch.GeoSearchAndStoreAsync(sourceKey, destinationKey, member, shape, count, demandClosest, order, storeDistances, flags);

    public Task<long> GeoSearchAndStoreAsync(
        RedisKey sourceKey,
        RedisKey destinationKey,
        double longitude,
        double latitude,
        GeoSearchShape shape,
        int count = -1,
        bool demandClosest = true,
        Order? order = default,
        bool storeDistances = false,
        CommandFlags flags = CommandFlags.None
    ) => _batch.GeoSearchAndStoreAsync(sourceKey, destinationKey, longitude, latitude, shape, count, demandClosest, order, storeDistances, flags);

    public Task<long> HashDecrementAsync(RedisKey key, RedisValue hashField, long value = 1, CommandFlags flags = CommandFlags.None) => _batch.HashDecrementAsync(key, hashField, value, flags);

    public Task<double> HashDecrementAsync(RedisKey key, RedisValue hashField, double value, CommandFlags flags = CommandFlags.None) => _batch.HashDecrementAsync(key, hashField, value, flags);

    public Task<bool> HashDeleteAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None) => _batch.HashDeleteAsync(key, hashField, flags);

    public Task<long> HashDeleteAsync(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None) => _batch.HashDeleteAsync(key, hashFields, flags);

    public Task<bool> HashExistsAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None) => _batch.HashExistsAsync(key, hashField, flags);

    public Task<RedisValue> HashFieldGetAndDeleteAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None) => _batch.HashFieldGetAndDeleteAsync(key, hashField, flags);

    public Task<Lease<byte>> HashFieldGetLeaseAndDeleteAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None) => _batch.HashFieldGetLeaseAndDeleteAsync(key, hashField, flags);

    public Task<RedisValue[]> HashFieldGetAndDeleteAsync(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None) => _batch.HashFieldGetAndDeleteAsync(key, hashFields, flags);

    public Task<RedisValue> HashFieldGetAndSetExpiryAsync(RedisKey key, RedisValue hashField, System.TimeSpan? expiry = default, bool persist = false, CommandFlags flags = CommandFlags.None) =>
        _batch.HashFieldGetAndSetExpiryAsync(key, hashField, expiry, persist, flags);

    public Task<RedisValue> HashFieldGetAndSetExpiryAsync(RedisKey key, RedisValue hashField, System.DateTime expiry, CommandFlags flags = CommandFlags.None) => _batch.HashFieldGetAndSetExpiryAsync(key, hashField, expiry, flags);

    public Task<Lease<byte>> HashFieldGetLeaseAndSetExpiryAsync(RedisKey key, RedisValue hashField, System.TimeSpan? expiry = default, bool persist = false, CommandFlags flags = CommandFlags.None) =>
        _batch.HashFieldGetLeaseAndSetExpiryAsync(key, hashField, expiry, persist, flags);

    public Task<Lease<byte>> HashFieldGetLeaseAndSetExpiryAsync(RedisKey key, RedisValue hashField, System.DateTime expiry, CommandFlags flags = CommandFlags.None) => _batch.HashFieldGetLeaseAndSetExpiryAsync(key, hashField, expiry, flags);

    public Task<RedisValue[]> HashFieldGetAndSetExpiryAsync(RedisKey key, RedisValue[] hashFields, System.TimeSpan? expiry = default, bool persist = false, CommandFlags flags = CommandFlags.None) =>
        _batch.HashFieldGetAndSetExpiryAsync(key, hashFields, expiry, persist, flags);

    public Task<RedisValue[]> HashFieldGetAndSetExpiryAsync(RedisKey key, RedisValue[] hashFields, System.DateTime expiry, CommandFlags flags = CommandFlags.None) => _batch.HashFieldGetAndSetExpiryAsync(key, hashFields, expiry, flags);

    public Task<RedisValue> HashFieldSetAndSetExpiryAsync(RedisKey key, RedisValue field, RedisValue value, System.TimeSpan? expiry = default, bool keepTtl = false, When when = When.Always, CommandFlags flags = CommandFlags.None) =>
        _batch.HashFieldSetAndSetExpiryAsync(key, field, value, expiry, keepTtl, when, flags);

    public Task<RedisValue> HashFieldSetAndSetExpiryAsync(RedisKey key, RedisValue field, RedisValue value, System.DateTime expiry, When when = When.Always, CommandFlags flags = CommandFlags.None) =>
        _batch.HashFieldSetAndSetExpiryAsync(key, field, value, expiry, when, flags);

    public Task<RedisValue> HashFieldSetAndSetExpiryAsync(RedisKey key, HashEntry[] hashFields, System.TimeSpan? expiry = default, bool keepTtl = false, When when = When.Always, CommandFlags flags = CommandFlags.None) =>
        _batch.HashFieldSetAndSetExpiryAsync(key, hashFields, expiry, keepTtl, when, flags);

    public Task<RedisValue> HashFieldSetAndSetExpiryAsync(RedisKey key, HashEntry[] hashFields, System.DateTime expiry, When when = When.Always, CommandFlags flags = CommandFlags.None) =>
        _batch.HashFieldSetAndSetExpiryAsync(key, hashFields, expiry, when, flags);

    public Task<ExpireResult[]> HashFieldExpireAsync(RedisKey key, RedisValue[] hashFields, System.TimeSpan expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None) =>
        _batch.HashFieldExpireAsync(key, hashFields, expiry, when, flags);

    public Task<ExpireResult[]> HashFieldExpireAsync(RedisKey key, RedisValue[] hashFields, System.DateTime expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None) =>
        _batch.HashFieldExpireAsync(key, hashFields, expiry, when, flags);

    public Task<long[]> HashFieldGetExpireDateTimeAsync(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None) => _batch.HashFieldGetExpireDateTimeAsync(key, hashFields, flags);

    public Task<PersistResult[]> HashFieldPersistAsync(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None) => _batch.HashFieldPersistAsync(key, hashFields, flags);

    public Task<long[]> HashFieldGetTimeToLiveAsync(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None) => _batch.HashFieldGetTimeToLiveAsync(key, hashFields, flags);

    public Task<RedisValue> HashGetAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None) => _batch.HashGetAsync(key, hashField, flags);

    public Task<Lease<byte>> HashGetLeaseAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None) => _batch.HashGetLeaseAsync(key, hashField, flags);

    public Task<RedisValue[]> HashGetAsync(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None) => _batch.HashGetAsync(key, hashFields, flags);

    public Task<HashEntry[]> HashGetAllAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.HashGetAllAsync(key, flags);

    public Task<long> HashIncrementAsync(RedisKey key, RedisValue hashField, long value = 1, CommandFlags flags = CommandFlags.None) => _batch.HashIncrementAsync(key, hashField, value, flags);

    public Task<double> HashIncrementAsync(RedisKey key, RedisValue hashField, double value, CommandFlags flags = CommandFlags.None) => _batch.HashIncrementAsync(key, hashField, value, flags);

    public Task<RedisValue[]> HashKeysAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.HashKeysAsync(key, flags);

    public Task<long> HashLengthAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.HashLengthAsync(key, flags);

    public Task<RedisValue> HashRandomFieldAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.HashRandomFieldAsync(key, flags);

    public Task<RedisValue[]> HashRandomFieldsAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => _batch.HashRandomFieldsAsync(key, count, flags);

    public Task<HashEntry[]> HashRandomFieldsWithValuesAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => _batch.HashRandomFieldsWithValuesAsync(key, count, flags);

    public IAsyncEnumerable<HashEntry> HashScanAsync(RedisKey key, RedisValue pattern = default, int pageSize = 250, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None) =>
        _batch.HashScanAsync(key, pattern, pageSize, cursor, pageOffset, flags);

    public IAsyncEnumerable<RedisValue> HashScanNoValuesAsync(RedisKey key, RedisValue pattern = default, int pageSize = 250, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None) =>
        _batch.HashScanNoValuesAsync(key, pattern, pageSize, cursor, pageOffset, flags);

    public System.Threading.Tasks.Task HashSetAsync(RedisKey key, HashEntry[] hashFields, CommandFlags flags = CommandFlags.None) => _batch.HashSetAsync(key, hashFields, TweakSetType(flags, key));

    public Task<bool> HashSetAsync(RedisKey key, RedisValue hashField, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None) => _batch.HashSetAsync(key, hashField, value, when, TweakSetType(flags, key));

    public Task<long> HashStringLengthAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None) => _batch.HashStringLengthAsync(key, hashField, flags);

    public Task<RedisValue[]> HashValuesAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.HashValuesAsync(key, flags);

    public Task<bool> HyperLogLogAddAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => _batch.HyperLogLogAddAsync(key, value, flags);

    public Task<bool> HyperLogLogAddAsync(RedisKey key, RedisValue[] values, CommandFlags flags = CommandFlags.None) => _batch.HyperLogLogAddAsync(key, values, flags);

    public Task<long> HyperLogLogLengthAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.HyperLogLogLengthAsync(key, flags);

    public Task<long> HyperLogLogLengthAsync(RedisKey[] keys, CommandFlags flags = CommandFlags.None) => _batch.HyperLogLogLengthAsync(keys, flags);

    public System.Threading.Tasks.Task HyperLogLogMergeAsync(RedisKey destination, RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None) => _batch.HyperLogLogMergeAsync(destination, first, second, flags);

    public System.Threading.Tasks.Task HyperLogLogMergeAsync(RedisKey destination, RedisKey[] sourceKeys, CommandFlags flags = CommandFlags.None) => _batch.HyperLogLogMergeAsync(destination, sourceKeys, flags);

    public Task<System.Net.EndPoint> IdentifyEndpointAsync(RedisKey key = default, CommandFlags flags = CommandFlags.None) => _batch.IdentifyEndpointAsync(key, flags);

    public Task<bool> KeyCopyAsync(RedisKey sourceKey, RedisKey destinationKey, int destinationDatabase = -1, bool replace = false, CommandFlags flags = CommandFlags.None) =>
        _batch.KeyCopyAsync(sourceKey, destinationKey, destinationDatabase, replace, flags);

    public Task<bool> KeyDeleteAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.KeyDeleteAsync(key, flags);

    public Task<long> KeyDeleteAsync(RedisKey[] keys, CommandFlags flags = CommandFlags.None) => _batch.KeyDeleteAsync(keys, flags);

    public Task<byte[]> KeyDumpAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.KeyDumpAsync(key, flags);

    public Task<string> KeyEncodingAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.KeyEncodingAsync(key, flags);

    public Task<bool> KeyExistsAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.KeyExistsAsync(key, flags);

    public Task<long> KeyExistsAsync(RedisKey[] keys, CommandFlags flags = CommandFlags.None) => _batch.KeyExistsAsync(keys, flags);

    public Task<bool> KeyExpireAsync(RedisKey key, System.TimeSpan? expiry, CommandFlags flags) => _batch.KeyExpireAsync(key, expiry, TweakSetType(flags, key));

    public Task<bool> KeyExpireAsync(RedisKey key, System.TimeSpan? expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None) => _batch.KeyExpireAsync(key, expiry, when, TweakSetType(flags, key));

    public Task<bool> KeyExpireAsync(RedisKey key, System.DateTime? expiry, CommandFlags flags) => _batch.KeyExpireAsync(key, expiry, TweakSetType(flags, key));

    public Task<bool> KeyExpireAsync(RedisKey key, System.DateTime? expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None) => _batch.KeyExpireAsync(key, expiry, when, TweakSetType(flags, key));

    public Task<System.DateTime?> KeyExpireTimeAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.KeyExpireTimeAsync(key, flags);

    public Task<long?> KeyFrequencyAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.KeyFrequencyAsync(key, flags);

    public Task<System.TimeSpan?> KeyIdleTimeAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.KeyIdleTimeAsync(key, flags);

    public Task<bool> KeyMoveAsync(RedisKey key, int database, CommandFlags flags = CommandFlags.None) => _batch.KeyMoveAsync(key, database, flags);

    public Task<bool> KeyPersistAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.KeyPersistAsync(key, flags);

    public Task<RedisKey> KeyRandomAsync(CommandFlags flags = CommandFlags.None) => _batch.KeyRandomAsync(flags);

    public Task<long?> KeyRefCountAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.KeyRefCountAsync(key, flags);

    public Task<bool> KeyRenameAsync(RedisKey key, RedisKey newKey, When when = When.Always, CommandFlags flags = CommandFlags.None) => _batch.KeyRenameAsync(key, newKey, when, flags);

    public System.Threading.Tasks.Task KeyRestoreAsync(RedisKey key, byte[] value, System.TimeSpan? expiry = default, CommandFlags flags = CommandFlags.None) => _batch.KeyRestoreAsync(key, value, expiry, flags);

    public Task<System.TimeSpan?> KeyTimeToLiveAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.KeyTimeToLiveAsync(key, flags);

    public Task<bool> KeyTouchAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.KeyTouchAsync(key, flags);

    public Task<long> KeyTouchAsync(RedisKey[] keys, CommandFlags flags = CommandFlags.None) => _batch.KeyTouchAsync(keys, flags);

    public Task<RedisType> KeyTypeAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.KeyTypeAsync(key, flags);

    public Task<RedisValue> ListGetByIndexAsync(RedisKey key, long index, CommandFlags flags = CommandFlags.None) => _batch.ListGetByIndexAsync(key, index, flags);

    public Task<long> ListInsertAfterAsync(RedisKey key, RedisValue pivot, RedisValue value, CommandFlags flags = CommandFlags.None) => _batch.ListInsertAfterAsync(key, pivot, value, flags);

    public Task<long> ListInsertBeforeAsync(RedisKey key, RedisValue pivot, RedisValue value, CommandFlags flags = CommandFlags.None) => _batch.ListInsertBeforeAsync(key, pivot, value, flags);

    public Task<RedisValue> ListLeftPopAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.ListLeftPopAsync(key, flags);

    public Task<RedisValue[]> ListLeftPopAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => _batch.ListLeftPopAsync(key, count, flags);

    public Task<ListPopResult> ListLeftPopAsync(RedisKey[] keys, long count, CommandFlags flags = CommandFlags.None) => _batch.ListLeftPopAsync(keys, count, flags);

    public Task<long> ListPositionAsync(RedisKey key, RedisValue element, long rank = 1, long maxLength = 0, CommandFlags flags = CommandFlags.None) => _batch.ListPositionAsync(key, element, rank, maxLength, flags);

    public Task<long[]> ListPositionsAsync(RedisKey key, RedisValue element, long count, long rank = 1, long maxLength = 0, CommandFlags flags = CommandFlags.None) => _batch.ListPositionsAsync(key, element, count, rank, maxLength, flags);

    public Task<long> ListLeftPushAsync(RedisKey key, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None) => _batch.ListLeftPushAsync(key, value, when, flags);

    public Task<long> ListLeftPushAsync(RedisKey key, RedisValue[] values, When when = When.Always, CommandFlags flags = CommandFlags.None) => _batch.ListLeftPushAsync(key, values, when, flags);

    public Task<long> ListLeftPushAsync(RedisKey key, RedisValue[] values, CommandFlags flags) => _batch.ListLeftPushAsync(key, values, flags);

    public Task<long> ListLengthAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.ListLengthAsync(key, flags);

    public Task<RedisValue> ListMoveAsync(RedisKey sourceKey, RedisKey destinationKey, ListSide sourceSide, ListSide destinationSide, CommandFlags flags = CommandFlags.None) =>
        _batch.ListMoveAsync(sourceKey, destinationKey, sourceSide, destinationSide, flags);

    public Task<RedisValue[]> ListRangeAsync(RedisKey key, long start = 0, long stop = -1, CommandFlags flags = CommandFlags.None) => _batch.ListRangeAsync(key, start, stop, flags);

    public Task<long> ListRemoveAsync(RedisKey key, RedisValue value, long count = 0, CommandFlags flags = CommandFlags.None) => _batch.ListRemoveAsync(key, value, count, flags);

    public Task<RedisValue> ListRightPopAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.ListRightPopAsync(key, flags);

    public Task<RedisValue[]> ListRightPopAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => _batch.ListRightPopAsync(key, count, flags);

    public Task<ListPopResult> ListRightPopAsync(RedisKey[] keys, long count, CommandFlags flags = CommandFlags.None) => _batch.ListRightPopAsync(keys, count, flags);

    public Task<RedisValue> ListRightPopLeftPushAsync(RedisKey source, RedisKey destination, CommandFlags flags = CommandFlags.None) => _batch.ListRightPopLeftPushAsync(source, destination, flags);

    public Task<long> ListRightPushAsync(RedisKey key, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None) => _batch.ListRightPushAsync(key, value, when, flags);

    public Task<long> ListRightPushAsync(RedisKey key, RedisValue[] values, When when = When.Always, CommandFlags flags = CommandFlags.None) => _batch.ListRightPushAsync(key, values, when, flags);

    public Task<long> ListRightPushAsync(RedisKey key, RedisValue[] values, CommandFlags flags) => _batch.ListRightPushAsync(key, values, flags);

    public System.Threading.Tasks.Task ListSetByIndexAsync(RedisKey key, long index, RedisValue value, CommandFlags flags = CommandFlags.None) => _batch.ListSetByIndexAsync(key, index, value, flags);

    public System.Threading.Tasks.Task ListTrimAsync(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None) => _batch.ListTrimAsync(key, start, stop, flags);

    public Task<bool> LockExtendAsync(RedisKey key, RedisValue value, System.TimeSpan expiry, CommandFlags flags = CommandFlags.None) => _batch.LockExtendAsync(key, value, expiry, flags);

    public Task<RedisValue> LockQueryAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.LockQueryAsync(key, flags);

    public Task<bool> LockReleaseAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => _batch.LockReleaseAsync(key, value, flags);

    public Task<bool> LockTakeAsync(RedisKey key, RedisValue value, System.TimeSpan expiry, CommandFlags flags = CommandFlags.None) => _batch.LockTakeAsync(key, value, expiry, flags);

    public Task<long> PublishAsync(RedisChannel channel, RedisValue message, CommandFlags flags = CommandFlags.None) => _batch.PublishAsync(channel, message, flags);

    public Task<RedisResult> ExecuteAsync(string command, System.Object[] args) => _batch.ExecuteAsync(command, args);

    public Task<RedisResult> ExecuteAsync(string command, ICollection<System.Object> args, CommandFlags flags = CommandFlags.None) => _batch.ExecuteAsync(command, args, flags);

    public Task<RedisResult> ScriptEvaluateAsync(string script, RedisKey[] keys = default, RedisValue[] values = default, CommandFlags flags = CommandFlags.None) => _batch.ScriptEvaluateAsync(script, keys, values, flags);

    public Task<RedisResult> ScriptEvaluateAsync(byte[] hash, RedisKey[] keys = default, RedisValue[] values = default, CommandFlags flags = CommandFlags.None) => _batch.ScriptEvaluateAsync(hash, keys, values, flags);

    public Task<RedisResult> ScriptEvaluateAsync(LuaScript script, System.Object parameters = default, CommandFlags flags = CommandFlags.None) => _batch.ScriptEvaluateAsync(script, parameters, flags);

    public Task<RedisResult> ScriptEvaluateAsync(LoadedLuaScript script, System.Object parameters = default, CommandFlags flags = CommandFlags.None) => _batch.ScriptEvaluateAsync(script, parameters, flags);

    public Task<RedisResult> ScriptEvaluateReadOnlyAsync(string script, RedisKey[] keys = default, RedisValue[] values = default, CommandFlags flags = CommandFlags.None) => _batch.ScriptEvaluateReadOnlyAsync(script, keys, values, flags);

    public Task<RedisResult> ScriptEvaluateReadOnlyAsync(byte[] hash, RedisKey[] keys = default, RedisValue[] values = default, CommandFlags flags = CommandFlags.None) => _batch.ScriptEvaluateReadOnlyAsync(hash, keys, values, flags);

    public Task<bool> SetAddAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => _batch.SetAddAsync(key, value, flags);

    public Task<long> SetAddAsync(RedisKey key, RedisValue[] values, CommandFlags flags = CommandFlags.None) => _batch.SetAddAsync(key, values, flags);

    public Task<RedisValue[]> SetCombineAsync(SetOperation operation, RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None) => _batch.SetCombineAsync(operation, first, second, flags);

    public Task<RedisValue[]> SetCombineAsync(SetOperation operation, RedisKey[] keys, CommandFlags flags = CommandFlags.None) => _batch.SetCombineAsync(operation, keys, flags);

    public Task<long> SetCombineAndStoreAsync(SetOperation operation, RedisKey destination, RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None) => _batch.SetCombineAndStoreAsync(operation, destination, first, second, flags);

    public Task<long> SetCombineAndStoreAsync(SetOperation operation, RedisKey destination, RedisKey[] keys, CommandFlags flags = CommandFlags.None) => _batch.SetCombineAndStoreAsync(operation, destination, keys, flags);

    public Task<bool> SetContainsAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => _batch.SetContainsAsync(key, value, flags);

    public Task<bool[]> SetContainsAsync(RedisKey key, RedisValue[] values, CommandFlags flags = CommandFlags.None) => _batch.SetContainsAsync(key, values, flags);

    public Task<long> SetIntersectionLengthAsync(RedisKey[] keys, long limit = 0, CommandFlags flags = CommandFlags.None) => _batch.SetIntersectionLengthAsync(keys, limit, flags);

    public Task<long> SetLengthAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.SetLengthAsync(key, flags);

    public Task<RedisValue[]> SetMembersAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.SetMembersAsync(key, flags);

    public Task<bool> SetMoveAsync(RedisKey source, RedisKey destination, RedisValue value, CommandFlags flags = CommandFlags.None) => _batch.SetMoveAsync(source, destination, value, flags);

    public Task<RedisValue> SetPopAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.SetPopAsync(key, flags);

    public Task<RedisValue[]> SetPopAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => _batch.SetPopAsync(key, count, flags);

    public Task<RedisValue> SetRandomMemberAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.SetRandomMemberAsync(key, flags);

    public Task<RedisValue[]> SetRandomMembersAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => _batch.SetRandomMembersAsync(key, count, flags);

    public Task<bool> SetRemoveAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => _batch.SetRemoveAsync(key, value, flags);

    public Task<long> SetRemoveAsync(RedisKey key, RedisValue[] values, CommandFlags flags = CommandFlags.None) => _batch.SetRemoveAsync(key, values, flags);

    public IAsyncEnumerable<RedisValue> SetScanAsync(RedisKey key, RedisValue pattern = default, int pageSize = 250, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None) =>
        _batch.SetScanAsync(key, pattern, pageSize, cursor, pageOffset, flags);

    public Task<RedisValue[]> SortAsync(RedisKey key, long skip = 0, long take = -1, Order order = Order.Ascending, SortType sortType = SortType.Numeric, RedisValue by = default, RedisValue[] get = default, CommandFlags flags = CommandFlags.None) =>
        _batch.SortAsync(key, skip, take, order, sortType, by, get, flags);

    public Task<long> SortAndStoreAsync(
        RedisKey destination,
        RedisKey key,
        long skip = 0,
        long take = -1,
        Order order = Order.Ascending,
        SortType sortType = SortType.Numeric,
        RedisValue by = default,
        RedisValue[] get = default,
        CommandFlags flags = CommandFlags.None
    ) => _batch.SortAndStoreAsync(destination, key, skip, take, order, sortType, by, get, flags);

    public Task<bool> SortedSetAddAsync(RedisKey key, RedisValue member, double score, CommandFlags flags) => _batch.SortedSetAddAsync(key, member, score, flags);

    public Task<bool> SortedSetAddAsync(RedisKey key, RedisValue member, double score, When when, CommandFlags flags = CommandFlags.None) => _batch.SortedSetAddAsync(key, member, score, when, flags);

    public Task<bool> SortedSetAddAsync(RedisKey key, RedisValue member, double score, SortedSetWhen when = SortedSetWhen.Always, CommandFlags flags = CommandFlags.None) => _batch.SortedSetAddAsync(key, member, score, when, flags);

    public Task<long> SortedSetAddAsync(RedisKey key, SortedSetEntry[] values, CommandFlags flags) => _batch.SortedSetAddAsync(key, values, flags);

    public Task<long> SortedSetAddAsync(RedisKey key, SortedSetEntry[] values, When when, CommandFlags flags = CommandFlags.None) => _batch.SortedSetAddAsync(key, values, when, flags);

    public Task<long> SortedSetAddAsync(RedisKey key, SortedSetEntry[] values, SortedSetWhen when = SortedSetWhen.Always, CommandFlags flags = CommandFlags.None) => _batch.SortedSetAddAsync(key, values, when, flags);

    public Task<RedisValue[]> SortedSetCombineAsync(SetOperation operation, RedisKey[] keys, double[] weights = default, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None) =>
        _batch.SortedSetCombineAsync(operation, keys, weights, aggregate, flags);

    public Task<SortedSetEntry[]> SortedSetCombineWithScoresAsync(SetOperation operation, RedisKey[] keys, double[] weights = default, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None) =>
        _batch.SortedSetCombineWithScoresAsync(operation, keys, weights, aggregate, flags);

    public Task<long> SortedSetCombineAndStoreAsync(SetOperation operation, RedisKey destination, RedisKey first, RedisKey second, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None) =>
        _batch.SortedSetCombineAndStoreAsync(operation, destination, first, second, aggregate, flags);

    public Task<long> SortedSetCombineAndStoreAsync(SetOperation operation, RedisKey destination, RedisKey[] keys, double[] weights = default, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None) =>
        _batch.SortedSetCombineAndStoreAsync(operation, destination, keys, weights, aggregate, flags);

    public Task<double> SortedSetDecrementAsync(RedisKey key, RedisValue member, double value, CommandFlags flags = CommandFlags.None) => _batch.SortedSetDecrementAsync(key, member, value, flags);

    public Task<double> SortedSetIncrementAsync(RedisKey key, RedisValue member, double value, CommandFlags flags = CommandFlags.None) => _batch.SortedSetIncrementAsync(key, member, value, flags);

    public Task<long> SortedSetIntersectionLengthAsync(RedisKey[] keys, long limit = 0, CommandFlags flags = CommandFlags.None) => _batch.SortedSetIntersectionLengthAsync(keys, limit, flags);

    public Task<long> SortedSetLengthAsync(RedisKey key, double min = double.NegativeInfinity, double max = double.PositiveInfinity, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None) =>
        _batch.SortedSetLengthAsync(key, min, max, exclude, flags);

    public Task<long> SortedSetLengthByValueAsync(RedisKey key, RedisValue min, RedisValue max, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None) => _batch.SortedSetLengthByValueAsync(key, min, max, exclude, flags);

    public Task<RedisValue> SortedSetRandomMemberAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.SortedSetRandomMemberAsync(key, flags);

    public Task<RedisValue[]> SortedSetRandomMembersAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => _batch.SortedSetRandomMembersAsync(key, count, flags);

    public Task<SortedSetEntry[]> SortedSetRandomMembersWithScoresAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => _batch.SortedSetRandomMembersWithScoresAsync(key, count, flags);

    public Task<RedisValue[]> SortedSetRangeByRankAsync(RedisKey key, long start = 0, long stop = -1, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) => _batch.SortedSetRangeByRankAsync(key, start, stop, order, flags);

    public Task<long> SortedSetRangeAndStoreAsync(
        RedisKey sourceKey,
        RedisKey destinationKey,
        RedisValue start,
        RedisValue stop,
        SortedSetOrder sortedSetOrder = SortedSetOrder.ByRank,
        Exclude exclude = Exclude.None,
        Order order = Order.Ascending,
        long skip = 0,
        long? take = default,
        CommandFlags flags = CommandFlags.None
    ) => _batch.SortedSetRangeAndStoreAsync(sourceKey, destinationKey, start, stop, sortedSetOrder, exclude, order, skip, take, flags);

    public Task<SortedSetEntry[]> SortedSetRangeByRankWithScoresAsync(RedisKey key, long start = 0, long stop = -1, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) =>
        _batch.SortedSetRangeByRankWithScoresAsync(key, start, stop, order, flags);

    public Task<RedisValue[]> SortedSetRangeByScoreAsync(
        RedisKey key,
        double start = double.NegativeInfinity,
        double stop = double.PositiveInfinity,
        Exclude exclude = Exclude.None,
        Order order = Order.Ascending,
        long skip = 0,
        long take = -1,
        CommandFlags flags = CommandFlags.None
    ) => _batch.SortedSetRangeByScoreAsync(key, start, stop, exclude, order, skip, take, flags);

    public Task<SortedSetEntry[]> SortedSetRangeByScoreWithScoresAsync(
        RedisKey key,
        double start = double.NegativeInfinity,
        double stop = double.PositiveInfinity,
        Exclude exclude = Exclude.None,
        Order order = Order.Ascending,
        long skip = 0,
        long take = -1,
        CommandFlags flags = CommandFlags.None
    ) => _batch.SortedSetRangeByScoreWithScoresAsync(key, start, stop, exclude, order, skip, take, flags);

    public Task<RedisValue[]> SortedSetRangeByValueAsync(RedisKey key, RedisValue min, RedisValue max, Exclude exclude, long skip, long take = -1, CommandFlags flags = CommandFlags.None) =>
        _batch.SortedSetRangeByValueAsync(key, min, max, exclude, skip, take, flags);

    public Task<RedisValue[]> SortedSetRangeByValueAsync(
        RedisKey key,
        RedisValue min = default,
        RedisValue max = default,
        Exclude exclude = Exclude.None,
        Order order = Order.Ascending,
        long skip = 0,
        long take = -1,
        CommandFlags flags = CommandFlags.None
    ) => _batch.SortedSetRangeByValueAsync(key, min, max, exclude, order, skip, take, flags);

    public Task<long?> SortedSetRankAsync(RedisKey key, RedisValue member, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) => _batch.SortedSetRankAsync(key, member, order, flags);

    public Task<bool> SortedSetRemoveAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => _batch.SortedSetRemoveAsync(key, member, flags);

    public Task<long> SortedSetRemoveAsync(RedisKey key, RedisValue[] members, CommandFlags flags = CommandFlags.None) => _batch.SortedSetRemoveAsync(key, members, flags);

    public Task<long> SortedSetRemoveRangeByRankAsync(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None) => _batch.SortedSetRemoveRangeByRankAsync(key, start, stop, flags);

    public Task<long> SortedSetRemoveRangeByScoreAsync(RedisKey key, double start, double stop, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None) => _batch.SortedSetRemoveRangeByScoreAsync(key, start, stop, exclude, flags);

    public Task<long> SortedSetRemoveRangeByValueAsync(RedisKey key, RedisValue min, RedisValue max, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None) => _batch.SortedSetRemoveRangeByValueAsync(key, min, max, exclude, flags);

    public IAsyncEnumerable<SortedSetEntry> SortedSetScanAsync(RedisKey key, RedisValue pattern = default, int pageSize = 250, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None) =>
        _batch.SortedSetScanAsync(key, pattern, pageSize, cursor, pageOffset, flags);

    public Task<double?> SortedSetScoreAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => _batch.SortedSetScoreAsync(key, member, flags);

    public Task<double?[]> SortedSetScoresAsync(RedisKey key, RedisValue[] members, CommandFlags flags = CommandFlags.None) => _batch.SortedSetScoresAsync(key, members, flags);

    public Task<bool> SortedSetUpdateAsync(RedisKey key, RedisValue member, double score, SortedSetWhen when = SortedSetWhen.Always, CommandFlags flags = CommandFlags.None) => _batch.SortedSetUpdateAsync(key, member, score, when, flags);

    public Task<long> SortedSetUpdateAsync(RedisKey key, SortedSetEntry[] values, SortedSetWhen when = SortedSetWhen.Always, CommandFlags flags = CommandFlags.None) => _batch.SortedSetUpdateAsync(key, values, when, flags);

    public Task<SortedSetEntry?> SortedSetPopAsync(RedisKey key, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) => _batch.SortedSetPopAsync(key, order, flags);

    public Task<SortedSetEntry[]> SortedSetPopAsync(RedisKey key, long count, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) => _batch.SortedSetPopAsync(key, count, order, flags);

    public Task<SortedSetPopResult> SortedSetPopAsync(RedisKey[] keys, long count, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) => _batch.SortedSetPopAsync(keys, count, order, flags);

    public Task<long> StreamAcknowledgeAsync(RedisKey key, RedisValue groupName, RedisValue messageId, CommandFlags flags = CommandFlags.None) => _batch.StreamAcknowledgeAsync(key, groupName, messageId, flags);

    public Task<long> StreamAcknowledgeAsync(RedisKey key, RedisValue groupName, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None) => _batch.StreamAcknowledgeAsync(key, groupName, messageIds, flags);

    public Task<StreamTrimResult> StreamAcknowledgeAndDeleteAsync(RedisKey key, RedisValue groupName, StreamTrimMode mode, RedisValue messageId, CommandFlags flags = CommandFlags.None) =>
        _batch.StreamAcknowledgeAndDeleteAsync(key, groupName, mode, messageId, flags);

    public Task<StreamTrimResult[]> StreamAcknowledgeAndDeleteAsync(RedisKey key, RedisValue groupName, StreamTrimMode mode, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None) =>
        _batch.StreamAcknowledgeAndDeleteAsync(key, groupName, mode, messageIds, flags);

    public Task<RedisValue> StreamAddAsync(RedisKey key, RedisValue streamField, RedisValue streamValue, RedisValue? messageId, int? maxLength, bool useApproximateMaxLength, CommandFlags flags) =>
        _batch.StreamAddAsync(key, streamField, streamValue, messageId, maxLength, useApproximateMaxLength, flags);

    public Task<RedisValue> StreamAddAsync(RedisKey key, NameValueEntry[] streamPairs, RedisValue? messageId, int? maxLength, bool useApproximateMaxLength, CommandFlags flags) =>
        _batch.StreamAddAsync(key, streamPairs, messageId, maxLength, useApproximateMaxLength, flags);

    public Task<RedisValue> StreamAddAsync(
        RedisKey key,
        RedisValue streamField,
        RedisValue streamValue,
        RedisValue? messageId = default,
        long? maxLength = default,
        bool useApproximateMaxLength = false,
        long? limit = default,
        StreamTrimMode trimMode = StreamTrimMode.KeepReferences,
        CommandFlags flags = CommandFlags.None
    ) => _batch.StreamAddAsync(key, streamField, streamValue, messageId, maxLength, useApproximateMaxLength, limit, trimMode, flags);

    public Task<RedisValue> StreamAddAsync(
        RedisKey key,
        NameValueEntry[] streamPairs,
        RedisValue? messageId = default,
        long? maxLength = default,
        bool useApproximateMaxLength = false,
        long? limit = default,
        StreamTrimMode trimMode = StreamTrimMode.KeepReferences,
        CommandFlags flags = CommandFlags.None
    ) => _batch.StreamAddAsync(key, streamPairs, messageId, maxLength, useApproximateMaxLength, limit, trimMode, flags);

    public Task<RedisValue> StreamAddAsync(
        RedisKey key,
        RedisValue streamField,
        RedisValue streamValue,
        StreamIdempotentId idempotentId,
        long? maxLength = default,
        bool useApproximateMaxLength = false,
        long? limit = default,
        StreamTrimMode trimMode = StreamTrimMode.KeepReferences,
        CommandFlags flags = CommandFlags.None
    ) => _batch.StreamAddAsync(key, streamField, streamValue, idempotentId, maxLength, useApproximateMaxLength, limit, trimMode, flags);

    public Task<RedisValue> StreamAddAsync(
        RedisKey key,
        NameValueEntry[] streamPairs,
        StreamIdempotentId idempotentId,
        long? maxLength = default,
        bool useApproximateMaxLength = false,
        long? limit = default,
        StreamTrimMode trimMode = StreamTrimMode.KeepReferences,
        CommandFlags flags = CommandFlags.None
    ) => _batch.StreamAddAsync(key, streamPairs, idempotentId, maxLength, useApproximateMaxLength, limit, trimMode, flags);

    public System.Threading.Tasks.Task StreamConfigureAsync(RedisKey key, StreamConfiguration configuration, CommandFlags flags = CommandFlags.None) => _batch.StreamConfigureAsync(key, configuration, flags);

    public Task<StreamAutoClaimResult> StreamAutoClaimAsync(RedisKey key, RedisValue consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, RedisValue startAtId, int? count = default, CommandFlags flags = CommandFlags.None) =>
        _batch.StreamAutoClaimAsync(key, consumerGroup, claimingConsumer, minIdleTimeInMs, startAtId, count, flags);

    public Task<StreamAutoClaimIdsOnlyResult> StreamAutoClaimIdsOnlyAsync(RedisKey key, RedisValue consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, RedisValue startAtId, int? count = default, CommandFlags flags = CommandFlags.None) =>
        _batch.StreamAutoClaimIdsOnlyAsync(key, consumerGroup, claimingConsumer, minIdleTimeInMs, startAtId, count, flags);

    public Task<StreamEntry[]> StreamClaimAsync(RedisKey key, RedisValue consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None) =>
        _batch.StreamClaimAsync(key, consumerGroup, claimingConsumer, minIdleTimeInMs, messageIds, flags);

    public Task<RedisValue[]> StreamClaimIdsOnlyAsync(RedisKey key, RedisValue consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None) =>
        _batch.StreamClaimIdsOnlyAsync(key, consumerGroup, claimingConsumer, minIdleTimeInMs, messageIds, flags);

    public Task<bool> StreamConsumerGroupSetPositionAsync(RedisKey key, RedisValue groupName, RedisValue position, CommandFlags flags = CommandFlags.None) => _batch.StreamConsumerGroupSetPositionAsync(key, groupName, position, flags);

    public Task<StreamConsumerInfo[]> StreamConsumerInfoAsync(RedisKey key, RedisValue groupName, CommandFlags flags = CommandFlags.None) => _batch.StreamConsumerInfoAsync(key, groupName, flags);

    public Task<bool> StreamCreateConsumerGroupAsync(RedisKey key, RedisValue groupName, RedisValue? position, CommandFlags flags) => _batch.StreamCreateConsumerGroupAsync(key, groupName, position, flags);

    public Task<bool> StreamCreateConsumerGroupAsync(RedisKey key, RedisValue groupName, RedisValue? position = default, bool createStream = true, CommandFlags flags = CommandFlags.None) =>
        _batch.StreamCreateConsumerGroupAsync(key, groupName, position, createStream, flags);

    public Task<long> StreamDeleteAsync(RedisKey key, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None) => _batch.StreamDeleteAsync(key, messageIds, flags);

    public Task<StreamTrimResult[]> StreamDeleteAsync(RedisKey key, RedisValue[] messageIds, StreamTrimMode mode, CommandFlags flags = CommandFlags.None) => _batch.StreamDeleteAsync(key, messageIds, mode, flags);

    public Task<long> StreamDeleteConsumerAsync(RedisKey key, RedisValue groupName, RedisValue consumerName, CommandFlags flags = CommandFlags.None) => _batch.StreamDeleteConsumerAsync(key, groupName, consumerName, flags);

    public Task<bool> StreamDeleteConsumerGroupAsync(RedisKey key, RedisValue groupName, CommandFlags flags = CommandFlags.None) => _batch.StreamDeleteConsumerGroupAsync(key, groupName, flags);

    public Task<StreamGroupInfo[]> StreamGroupInfoAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.StreamGroupInfoAsync(key, flags);

    public Task<StreamInfo> StreamInfoAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.StreamInfoAsync(key, flags);

    public Task<long> StreamLengthAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.StreamLengthAsync(key, flags);

    public Task<StreamPendingInfo> StreamPendingAsync(RedisKey key, RedisValue groupName, CommandFlags flags = CommandFlags.None) => _batch.StreamPendingAsync(key, groupName, flags);

    public Task<StreamPendingMessageInfo[]> StreamPendingMessagesAsync(RedisKey key, RedisValue groupName, int count, RedisValue consumerName, RedisValue? minId, RedisValue? maxId, CommandFlags flags) =>
        _batch.StreamPendingMessagesAsync(key, groupName, count, consumerName, minId, maxId, flags);

    public Task<StreamPendingMessageInfo[]> StreamPendingMessagesAsync(
        RedisKey key,
        RedisValue groupName,
        int count,
        RedisValue consumerName,
        RedisValue? minId = default,
        RedisValue? maxId = default,
        long? minIdleTimeInMs = default,
        CommandFlags flags = CommandFlags.None
    ) => _batch.StreamPendingMessagesAsync(key, groupName, count, consumerName, minId, maxId, minIdleTimeInMs, flags);

    public Task<StreamEntry[]> StreamRangeAsync(RedisKey key, RedisValue? minId = default, RedisValue? maxId = default, int? count = default, Order messageOrder = Order.Ascending, CommandFlags flags = CommandFlags.None) =>
        _batch.StreamRangeAsync(key, minId, maxId, count, messageOrder, flags);

    public Task<StreamEntry[]> StreamReadAsync(RedisKey key, RedisValue position, int? count = default, CommandFlags flags = CommandFlags.None) => _batch.StreamReadAsync(key, position, count, flags);

    public Task<RedisStream[]> StreamReadAsync(StreamPosition[] streamPositions, int? countPerStream = default, CommandFlags flags = CommandFlags.None) => _batch.StreamReadAsync(streamPositions, countPerStream, flags);

    public Task<StreamEntry[]> StreamReadGroupAsync(RedisKey key, RedisValue groupName, RedisValue consumerName, RedisValue? position, int? count, CommandFlags flags) => _batch.StreamReadGroupAsync(key, groupName, consumerName, position, count, flags);

    public Task<StreamEntry[]> StreamReadGroupAsync(RedisKey key, RedisValue groupName, RedisValue consumerName, RedisValue? position, int? count, bool noAck, CommandFlags flags) =>
        _batch.StreamReadGroupAsync(key, groupName, consumerName, position, count, noAck, flags);

    public Task<StreamEntry[]> StreamReadGroupAsync(
        RedisKey key,
        RedisValue groupName,
        RedisValue consumerName,
        RedisValue? position = default,
        int? count = default,
        bool noAck = false,
        System.TimeSpan? claimMinIdleTime = default,
        CommandFlags flags = CommandFlags.None
    ) => _batch.StreamReadGroupAsync(key, groupName, consumerName, position, count, noAck, claimMinIdleTime, flags);

    public Task<RedisStream[]> StreamReadGroupAsync(StreamPosition[] streamPositions, RedisValue groupName, RedisValue consumerName, int? countPerStream, CommandFlags flags) =>
        _batch.StreamReadGroupAsync(streamPositions, groupName, consumerName, countPerStream, flags);

    public Task<RedisStream[]> StreamReadGroupAsync(StreamPosition[] streamPositions, RedisValue groupName, RedisValue consumerName, int? countPerStream, bool noAck, CommandFlags flags) =>
        _batch.StreamReadGroupAsync(streamPositions, groupName, consumerName, countPerStream, noAck, flags);

    public Task<long> StreamTrimAsync(RedisKey key, int maxLength, bool useApproximateMaxLength, CommandFlags flags) => _batch.StreamTrimAsync(key, maxLength, useApproximateMaxLength, flags);

    public Task<long> StreamTrimAsync(RedisKey key, long maxLength, bool useApproximateMaxLength = false, long? limit = default, StreamTrimMode mode = StreamTrimMode.KeepReferences, CommandFlags flags = CommandFlags.None) =>
        _batch.StreamTrimAsync(key, maxLength, useApproximateMaxLength, limit, mode, flags);

    public Task<long> StreamTrimByMinIdAsync(RedisKey key, RedisValue minId, bool useApproximateMaxLength = false, long? limit = default, StreamTrimMode mode = StreamTrimMode.KeepReferences, CommandFlags flags = CommandFlags.None) =>
        _batch.StreamTrimByMinIdAsync(key, minId, useApproximateMaxLength, limit, mode, flags);

    public Task<long> StringAppendAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => _batch.StringAppendAsync(key, value, flags);

    public Task<long> StringBitCountAsync(RedisKey key, long start, long end, CommandFlags flags) => _batch.StringBitCountAsync(key, start, end, flags);

    public Task<long> StringBitCountAsync(RedisKey key, long start = 0, long end = -1, StringIndexType indexType = StringIndexType.Byte, CommandFlags flags = CommandFlags.None) => _batch.StringBitCountAsync(key, start, end, indexType, flags);

    public Task<long> StringBitOperationAsync(Bitwise operation, RedisKey destination, RedisKey first, RedisKey second = default, CommandFlags flags = CommandFlags.None) => _batch.StringBitOperationAsync(operation, destination, first, second, flags);

    public Task<long> StringBitOperationAsync(Bitwise operation, RedisKey destination, RedisKey[] keys, CommandFlags flags = CommandFlags.None) => _batch.StringBitOperationAsync(operation, destination, keys, flags);

    public Task<long> StringBitPositionAsync(RedisKey key, bool bit, long start, long end, CommandFlags flags) => _batch.StringBitPositionAsync(key, bit, start, end, flags);

    public Task<long> StringBitPositionAsync(RedisKey key, bool bit, long start = 0, long end = -1, StringIndexType indexType = StringIndexType.Byte, CommandFlags flags = CommandFlags.None) =>
        _batch.StringBitPositionAsync(key, bit, start, end, indexType, flags);

    public Task<long> StringDecrementAsync(RedisKey key, long value = 1, CommandFlags flags = CommandFlags.None) => _batch.StringDecrementAsync(key, value, flags);

    public Task<bool> StringDeleteAsync(RedisKey key, ValueCondition when, CommandFlags flags = CommandFlags.None) => _batch.StringDeleteAsync(key, when, flags);

    public Task<double> StringDecrementAsync(RedisKey key, double value, CommandFlags flags = CommandFlags.None) => _batch.StringDecrementAsync(key, value, flags);

    public Task<ValueCondition?> StringDigestAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.StringDigestAsync(key, flags);

    public Task<GcraRateLimitResult> StringGcraRateLimitAsync(RedisKey key, int maxBurst, int requestsPerPeriod, double periodSeconds = 1D, int count = 1, CommandFlags flags = CommandFlags.None) =>
        _batch.StringGcraRateLimitAsync(key, maxBurst, requestsPerPeriod, periodSeconds, count, flags);

    public Task<RedisValue> StringGetAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.StringGetAsync(key, flags);

    public Task<RedisValue[]> StringGetAsync(RedisKey[] keys, CommandFlags flags = CommandFlags.None) => _batch.StringGetAsync(keys, flags);

    public Task<Lease<byte>> StringGetLeaseAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.StringGetLeaseAsync(key, flags);

    public Task<bool> StringGetBitAsync(RedisKey key, long offset, CommandFlags flags = CommandFlags.None) => _batch.StringGetBitAsync(key, offset, flags);

    public Task<RedisValue> StringGetRangeAsync(RedisKey key, long start, long end, CommandFlags flags = CommandFlags.None) => _batch.StringGetRangeAsync(key, start, end, flags);

    public Task<RedisValue> StringGetSetAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => _batch.StringGetSetAsync(key, value, flags);

    public Task<RedisValue> StringGetSetExpiryAsync(RedisKey key, System.TimeSpan? expiry, CommandFlags flags = CommandFlags.None) => _batch.StringGetSetExpiryAsync(key, expiry, flags);

    public Task<RedisValue> StringGetSetExpiryAsync(RedisKey key, System.DateTime expiry, CommandFlags flags = CommandFlags.None) => _batch.StringGetSetExpiryAsync(key, expiry, flags);

    public Task<RedisValue> StringGetDeleteAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.StringGetDeleteAsync(key, flags);

    public Task<RedisValueWithExpiry> StringGetWithExpiryAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.StringGetWithExpiryAsync(key, flags);

    public Task<long> StringIncrementAsync(RedisKey key, long value = 1, CommandFlags flags = CommandFlags.None) => _batch.StringIncrementAsync(key, value, flags);

    public Task<double> StringIncrementAsync(RedisKey key, double value, CommandFlags flags = CommandFlags.None) => _batch.StringIncrementAsync(key, value, flags);

    public Task<long> StringLengthAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.StringLengthAsync(key, flags);

    public Task<string> StringLongestCommonSubsequenceAsync(RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None) => _batch.StringLongestCommonSubsequenceAsync(first, second, flags);

    public Task<long> StringLongestCommonSubsequenceLengthAsync(RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None) => _batch.StringLongestCommonSubsequenceLengthAsync(first, second, flags);

    public Task<LCSMatchResult> StringLongestCommonSubsequenceWithMatchesAsync(RedisKey first, RedisKey second, long minLength = 0, CommandFlags flags = CommandFlags.None) =>
        _batch.StringLongestCommonSubsequenceWithMatchesAsync(first, second, minLength, flags);

    public Task<bool> StringSetAsync(RedisKey key, RedisValue value, System.TimeSpan? expiry, When when) => _batch.StringSetAsync(key, value, expiry, when);

    public Task<bool> StringSetAsync(RedisKey key, RedisValue value, System.TimeSpan? expiry, When when, CommandFlags flags) => _batch.StringSetAsync(key, value, expiry, when, flags);

    public Task<bool> StringSetAsync(RedisKey key, RedisValue value, System.TimeSpan? expiry, bool keepTtl, When when = When.Always, CommandFlags flags = CommandFlags.None) => _batch.StringSetAsync(key, value, expiry, keepTtl, when, flags);

    public Task<bool> StringSetAsync(RedisKey key, RedisValue value, Expiration expiry = default, ValueCondition when = default, CommandFlags flags = CommandFlags.None) => _batch.StringSetAsync(key, value, expiry, when, flags);

    public Task<bool> StringSetAsync(KeyValuePair<RedisKey, RedisValue>[] values, When when, CommandFlags flags) => _batch.StringSetAsync(values, when, flags);

    public Task<bool> StringSetAsync(KeyValuePair<RedisKey, RedisValue>[] values, When when = When.Always, Expiration expiry = default, CommandFlags flags = CommandFlags.None) => _batch.StringSetAsync(values, when, expiry, flags);

    public Task<RedisValue> StringSetAndGetAsync(RedisKey key, RedisValue value, System.TimeSpan? expiry, When when, CommandFlags flags) => _batch.StringSetAndGetAsync(key, value, expiry, when, flags);

    public Task<RedisValue> StringSetAndGetAsync(RedisKey key, RedisValue value, System.TimeSpan? expiry = default, bool keepTtl = false, When when = When.Always, CommandFlags flags = CommandFlags.None) =>
        _batch.StringSetAndGetAsync(key, value, expiry, keepTtl, when, flags);

    public Task<bool> StringSetBitAsync(RedisKey key, long offset, bool bit, CommandFlags flags = CommandFlags.None) => _batch.StringSetBitAsync(key, offset, bit, flags);

    public Task<RedisValue> StringSetRangeAsync(RedisKey key, long offset, RedisValue value, CommandFlags flags = CommandFlags.None) => _batch.StringSetRangeAsync(key, offset, value, flags);

    public Task<bool> VectorSetAddAsync(RedisKey key, VectorSetAddRequest request, CommandFlags flags = CommandFlags.None) => _batch.VectorSetAddAsync(key, request, flags);

    public Task<long> VectorSetLengthAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.VectorSetLengthAsync(key, flags);

    public Task<int> VectorSetDimensionAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.VectorSetDimensionAsync(key, flags);

    public Task<Lease<float>> VectorSetGetApproximateVectorAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => _batch.VectorSetGetApproximateVectorAsync(key, member, flags);

    public Task<string> VectorSetGetAttributesJsonAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => _batch.VectorSetGetAttributesJsonAsync(key, member, flags);

    public Task<VectorSetInfo?> VectorSetInfoAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.VectorSetInfoAsync(key, flags);

    public Task<bool> VectorSetContainsAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => _batch.VectorSetContainsAsync(key, member, flags);

    public Task<Lease<RedisValue>> VectorSetGetLinksAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => _batch.VectorSetGetLinksAsync(key, member, flags);

    public Task<Lease<VectorSetLink>> VectorSetGetLinksWithScoresAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => _batch.VectorSetGetLinksWithScoresAsync(key, member, flags);

    public Task<RedisValue> VectorSetRandomMemberAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => _batch.VectorSetRandomMemberAsync(key, flags);

    public Task<RedisValue[]> VectorSetRandomMembersAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => _batch.VectorSetRandomMembersAsync(key, count, flags);

    public Task<bool> VectorSetRemoveAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => _batch.VectorSetRemoveAsync(key, member, flags);

    public Task<bool> VectorSetSetAttributesJsonAsync(RedisKey key, RedisValue member, string attributesJson, CommandFlags flags = CommandFlags.None) => _batch.VectorSetSetAttributesJsonAsync(key, member, attributesJson, flags);

    public Task<Lease<VectorSetSimilaritySearchResult>> VectorSetSimilaritySearchAsync(RedisKey key, VectorSetSimilaritySearchRequest query, CommandFlags flags = CommandFlags.None) => _batch.VectorSetSimilaritySearchAsync(key, query, flags);

    public Task<Lease<RedisValue>> VectorSetRangeAsync(RedisKey key, RedisValue start = default, RedisValue end = default, long count = -1, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None) =>
        _batch.VectorSetRangeAsync(key, start, end, count, exclude, flags);

    public IAsyncEnumerable<RedisValue> VectorSetRangeEnumerateAsync(RedisKey key, RedisValue start = default, RedisValue end = default, long count = 100, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None) =>
        _batch.VectorSetRangeEnumerateAsync(key, start, end, count, exclude, flags);

    public Task<RedisStream[]> StreamReadGroupAsync(
        StreamPosition[] streamPositions,
        RedisValue groupName,
        RedisValue consumerName,
        int? countPerStream = default,
        bool noAck = false,
        System.TimeSpan? claimMinIdleTime = default,
        CommandFlags flags = CommandFlags.None
    ) => _batch.StreamReadGroupAsync(streamPositions, groupName, consumerName, countPerStream, noAck, claimMinIdleTime, flags);

    public Task<System.TimeSpan> PingAsync(CommandFlags flags = CommandFlags.None) => _batch.PingAsync(flags);

    public bool TryWait(System.Threading.Tasks.Task task) => _batch.TryWait(task);

    public void Wait(System.Threading.Tasks.Task task) => _batch.Wait(task);

    public T Wait<T>(Task<T> task) => _batch.Wait<T>(task);

    public void WaitAll(System.Threading.Tasks.Task[] tasks) => _batch.WaitAll(tasks);

    public IConnectionMultiplexer Multiplexer => _batch.Multiplexer;
}
