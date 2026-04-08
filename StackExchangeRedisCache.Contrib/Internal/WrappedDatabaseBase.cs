#pragma warning disable SER002, SER003, SER006

using System.Net;
using StackExchange.Redis;

namespace StackExchangeRedisCache.Contrib.Internal;

/// <summary>
/// Abstract partial implementation of <see cref="IDatabase"/>.
/// For methods used by (or that speculatively might be used by) <see cref="Microsoft.Extensions.Caching.StackExchangeRedis.RedisCache"/>,
/// provides abstract method declarations that must be implemented by a derived class.
/// For other methods (those deemed likely irrelevant to the operation of <see cref="Microsoft.Extensions.Caching.StackExchangeRedis.RedisCache"/>),
/// provides a straight pass-through to the wrapped <see cref="IDatabase"/>.
/// </summary>
/// <param name="db">Wrapped <see cref="IDatabase"/>.</param>
internal abstract partial class WrappedDatabaseBase(IDatabase db) : IDatabase
{
    public abstract RedisValue HashGet(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None);

    public abstract RedisValue[] HashGet(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None);

    public abstract Task<RedisValue> HashGetAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None);

    public abstract Task<RedisValue[]> HashGetAsync(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None);

    public abstract void HashSet(RedisKey key, HashEntry[] hashFields, CommandFlags flags = CommandFlags.None);

    public abstract bool HashSet(RedisKey key, RedisValue hashField, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None);

    public abstract Task HashSetAsync(RedisKey key, HashEntry[] hashFields, CommandFlags flags = CommandFlags.None);

    public abstract Task<bool> HashSetAsync(RedisKey key, RedisValue hashField, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None);

    public abstract RedisResult ScriptEvaluate(string script, RedisKey[]? keys = null, RedisValue[]? values = null, CommandFlags flags = CommandFlags.None);

    public abstract RedisResult ScriptEvaluate(byte[] hash, RedisKey[]? keys = null, RedisValue[]? values = null, CommandFlags flags = CommandFlags.None);

    public abstract Task<RedisResult> ScriptEvaluateAsync(string script, RedisKey[]? keys = null, RedisValue[]? values = null, CommandFlags flags = CommandFlags.None);

    public abstract Task<RedisResult> ScriptEvaluateAsync(byte[] hash, RedisKey[]? keys = null, RedisValue[]? values = null, CommandFlags flags = CommandFlags.None);

    public abstract IBatch CreateBatch(object? asyncState = null);

    public abstract Lease<byte>? HashGetLease(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None);

    public abstract Task<Lease<byte>?> HashGetLeaseAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None);

    public abstract bool KeyDelete(RedisKey key, CommandFlags flags = CommandFlags.None);

    public abstract long KeyDelete(RedisKey[] keys, CommandFlags flags = CommandFlags.None);

    public abstract Task<bool> KeyDeleteAsync(RedisKey key, CommandFlags flags = CommandFlags.None);

    public abstract Task<long> KeyDeleteAsync(RedisKey[] keys, CommandFlags flags = CommandFlags.None);

    public abstract bool KeyExpire(RedisKey key, TimeSpan? expiry, CommandFlags flags);

    public abstract bool KeyExpire(RedisKey key, TimeSpan? expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None);

    public abstract bool KeyExpire(RedisKey key, DateTime? expiry, CommandFlags flags);

    public abstract bool KeyExpire(RedisKey key, DateTime? expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None);

    public abstract Task<bool> KeyExpireAsync(RedisKey key, TimeSpan? expiry, CommandFlags flags);

    public abstract Task<bool> KeyExpireAsync(RedisKey key, TimeSpan? expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None);

    public abstract Task<bool> KeyExpireAsync(RedisKey key, DateTime? expiry, CommandFlags flags);

    public abstract Task<bool> KeyExpireAsync(RedisKey key, DateTime? expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None);

    #region Straight pass-through

    public int Database => db.Database;

    public IConnectionMultiplexer Multiplexer => db.Multiplexer;

    public ITransaction CreateTransaction(object? asyncState = null) => db.CreateTransaction(asyncState);

    public RedisValue DebugObject(RedisKey key, CommandFlags flags = CommandFlags.None) => db.DebugObject(key, flags);

    public Task<RedisValue> DebugObjectAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.DebugObjectAsync(key, flags);

    public RedisResult Execute(string command, params object[] args) => db.Execute(command, args);

    public RedisResult Execute(string command, ICollection<object> args, CommandFlags flags = CommandFlags.None) => db.Execute(command, args, flags);

    public Task<RedisResult> ExecuteAsync(string command, params object[] args) => db.ExecuteAsync(command, args);

    public Task<RedisResult> ExecuteAsync(string command, ICollection<object>? args, CommandFlags flags = CommandFlags.None) => db.ExecuteAsync(command, args, flags);

    public bool GeoAdd(RedisKey key, double longitude, double latitude, RedisValue member, CommandFlags flags = CommandFlags.None) => db.GeoAdd(key, longitude, latitude, member, flags);

    public bool GeoAdd(RedisKey key, GeoEntry value, CommandFlags flags = CommandFlags.None) => db.GeoAdd(key, value, flags);

    public long GeoAdd(RedisKey key, GeoEntry[] values, CommandFlags flags = CommandFlags.None) => db.GeoAdd(key, values, flags);

    public Task<bool> GeoAddAsync(RedisKey key, double longitude, double latitude, RedisValue member, CommandFlags flags = CommandFlags.None) => db.GeoAddAsync(key, longitude, latitude, member, flags);

    public Task<bool> GeoAddAsync(RedisKey key, GeoEntry value, CommandFlags flags = CommandFlags.None) => db.GeoAddAsync(key, value, flags);

    public Task<long> GeoAddAsync(RedisKey key, GeoEntry[] values, CommandFlags flags = CommandFlags.None) => db.GeoAddAsync(key, values, flags);

    public double? GeoDistance(RedisKey key, RedisValue member1, RedisValue member2, GeoUnit unit = GeoUnit.Meters, CommandFlags flags = CommandFlags.None) => db.GeoDistance(key, member1, member2, unit, flags);

    public Task<double?> GeoDistanceAsync(RedisKey key, RedisValue member1, RedisValue member2, GeoUnit unit = GeoUnit.Meters, CommandFlags flags = CommandFlags.None) => db.GeoDistanceAsync(key, member1, member2, unit, flags);

    public string?[] GeoHash(RedisKey key, RedisValue[] members, CommandFlags flags = CommandFlags.None) => db.GeoHash(key, members, flags);

    public string? GeoHash(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.GeoHash(key, member, flags);

    public Task<string?[]> GeoHashAsync(RedisKey key, RedisValue[] members, CommandFlags flags = CommandFlags.None) => db.GeoHashAsync(key, members, flags);

    public Task<string?> GeoHashAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.GeoHashAsync(key, member, flags);

    public GeoPosition?[] GeoPosition(RedisKey key, RedisValue[] members, CommandFlags flags = CommandFlags.None) => db.GeoPosition(key, members, flags);

    public GeoPosition? GeoPosition(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.GeoPosition(key, member, flags);

    public Task<GeoPosition?[]> GeoPositionAsync(RedisKey key, RedisValue[] members, CommandFlags flags = CommandFlags.None) => db.GeoPositionAsync(key, members, flags);

    public Task<GeoPosition?> GeoPositionAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.GeoPositionAsync(key, member, flags);

    public GeoRadiusResult[] GeoRadius(RedisKey key, RedisValue member, double radius, GeoUnit unit = GeoUnit.Meters, int count = -1, Order? order = null, GeoRadiusOptions options = GeoRadiusOptions.Default, CommandFlags flags = CommandFlags.None) =>
        db.GeoRadius(key, member, radius, unit, count, order, options, flags);

    public GeoRadiusResult[] GeoRadius(
        RedisKey key,
        double longitude,
        double latitude,
        double radius,
        GeoUnit unit = GeoUnit.Meters,
        int count = -1,
        Order? order = null,
        GeoRadiusOptions options = GeoRadiusOptions.Default,
        CommandFlags flags = CommandFlags.None
    ) => db.GeoRadius(key, longitude, latitude, radius, unit, count, order, options, flags);

    public Task<GeoRadiusResult[]> GeoRadiusAsync(
        RedisKey key,
        RedisValue member,
        double radius,
        GeoUnit unit = GeoUnit.Meters,
        int count = -1,
        Order? order = null,
        GeoRadiusOptions options = GeoRadiusOptions.Default,
        CommandFlags flags = CommandFlags.None
    ) => db.GeoRadiusAsync(key, member, radius, unit, count, order, options, flags);

    public Task<GeoRadiusResult[]> GeoRadiusAsync(
        RedisKey key,
        double longitude,
        double latitude,
        double radius,
        GeoUnit unit = GeoUnit.Meters,
        int count = -1,
        Order? order = null,
        GeoRadiusOptions options = GeoRadiusOptions.Default,
        CommandFlags flags = CommandFlags.None
    ) => db.GeoRadiusAsync(key, longitude, latitude, radius, unit, count, order, options, flags);

    public bool GeoRemove(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.GeoRemove(key, member, flags);

    public Task<bool> GeoRemoveAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.GeoRemoveAsync(key, member, flags);

    public GeoRadiusResult[] GeoSearch(RedisKey key, RedisValue member, GeoSearchShape shape, int count = -1, bool demandClosest = true, Order? order = null, GeoRadiusOptions options = GeoRadiusOptions.Default, CommandFlags flags = CommandFlags.None) =>
        db.GeoSearch(key, member, shape, count, demandClosest, order, options, flags);

    public GeoRadiusResult[] GeoSearch(
        RedisKey key,
        double longitude,
        double latitude,
        GeoSearchShape shape,
        int count = -1,
        bool demandClosest = true,
        Order? order = null,
        GeoRadiusOptions options = GeoRadiusOptions.Default,
        CommandFlags flags = CommandFlags.None
    ) => db.GeoSearch(key, longitude, latitude, shape, count, demandClosest, order, options, flags);

    public long GeoSearchAndStore(RedisKey sourceKey, RedisKey destinationKey, RedisValue member, GeoSearchShape shape, int count = -1, bool demandClosest = true, Order? order = null, bool storeDistances = false, CommandFlags flags = CommandFlags.None) =>
        db.GeoSearchAndStore(sourceKey, destinationKey, member, shape, count, demandClosest, order, storeDistances, flags);

    public long GeoSearchAndStore(
        RedisKey sourceKey,
        RedisKey destinationKey,
        double longitude,
        double latitude,
        GeoSearchShape shape,
        int count = -1,
        bool demandClosest = true,
        Order? order = null,
        bool storeDistances = false,
        CommandFlags flags = CommandFlags.None
    ) => db.GeoSearchAndStore(sourceKey, destinationKey, longitude, latitude, shape, count, demandClosest, order, storeDistances, flags);

    public Task<long> GeoSearchAndStoreAsync(
        RedisKey sourceKey,
        RedisKey destinationKey,
        RedisValue member,
        GeoSearchShape shape,
        int count = -1,
        bool demandClosest = true,
        Order? order = null,
        bool storeDistances = false,
        CommandFlags flags = CommandFlags.None
    ) => db.GeoSearchAndStoreAsync(sourceKey, destinationKey, member, shape, count, demandClosest, order, storeDistances, flags);

    public Task<long> GeoSearchAndStoreAsync(
        RedisKey sourceKey,
        RedisKey destinationKey,
        double longitude,
        double latitude,
        GeoSearchShape shape,
        int count = -1,
        bool demandClosest = true,
        Order? order = null,
        bool storeDistances = false,
        CommandFlags flags = CommandFlags.None
    ) => db.GeoSearchAndStoreAsync(sourceKey, destinationKey, longitude, latitude, shape, count, demandClosest, order, storeDistances, flags);

    public Task<GeoRadiusResult[]> GeoSearchAsync(
        RedisKey key,
        RedisValue member,
        GeoSearchShape shape,
        int count = -1,
        bool demandClosest = true,
        Order? order = null,
        GeoRadiusOptions options = GeoRadiusOptions.Default,
        CommandFlags flags = CommandFlags.None
    ) => db.GeoSearchAsync(key, member, shape, count, demandClosest, order, options, flags);

    public Task<GeoRadiusResult[]> GeoSearchAsync(
        RedisKey key,
        double longitude,
        double latitude,
        GeoSearchShape shape,
        int count = -1,
        bool demandClosest = true,
        Order? order = null,
        GeoRadiusOptions options = GeoRadiusOptions.Default,
        CommandFlags flags = CommandFlags.None
    ) => db.GeoSearchAsync(key, longitude, latitude, shape, count, demandClosest, order, options, flags);

    public long HashDecrement(RedisKey key, RedisValue hashField, long value = 1, CommandFlags flags = CommandFlags.None) => db.HashDecrement(key, hashField, value, flags);

    public double HashDecrement(RedisKey key, RedisValue hashField, double value, CommandFlags flags = CommandFlags.None) => db.HashDecrement(key, hashField, value, flags);

    public Task<long> HashDecrementAsync(RedisKey key, RedisValue hashField, long value = 1, CommandFlags flags = CommandFlags.None) => db.HashDecrementAsync(key, hashField, value, flags);

    public Task<double> HashDecrementAsync(RedisKey key, RedisValue hashField, double value, CommandFlags flags = CommandFlags.None) => db.HashDecrementAsync(key, hashField, value, flags);

    public bool HashDelete(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None) => db.HashDelete(key, hashField, flags);

    public long HashDelete(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None) => db.HashDelete(key, hashFields, flags);

    public Task<bool> HashDeleteAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None) => db.HashDeleteAsync(key, hashField, flags);

    public Task<long> HashDeleteAsync(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None) => db.HashDeleteAsync(key, hashFields, flags);

    public bool HashExists(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None) => db.HashExists(key, hashField, flags);

    public Task<bool> HashExistsAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None) => db.HashExistsAsync(key, hashField, flags);

    public ExpireResult[] HashFieldExpire(RedisKey key, RedisValue[] hashFields, TimeSpan expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None) => db.HashFieldExpire(key, hashFields, expiry, when, flags);

    public ExpireResult[] HashFieldExpire(RedisKey key, RedisValue[] hashFields, DateTime expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None) => db.HashFieldExpire(key, hashFields, expiry, when, flags);

    public Task<ExpireResult[]> HashFieldExpireAsync(RedisKey key, RedisValue[] hashFields, TimeSpan expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None) => db.HashFieldExpireAsync(key, hashFields, expiry, when, flags);

    public Task<ExpireResult[]> HashFieldExpireAsync(RedisKey key, RedisValue[] hashFields, DateTime expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None) => db.HashFieldExpireAsync(key, hashFields, expiry, when, flags);

    public long[] HashFieldGetExpireDateTime(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None) => db.HashFieldGetExpireDateTime(key, hashFields, flags);

    public Task<long[]> HashFieldGetExpireDateTimeAsync(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None) => db.HashFieldGetExpireDateTimeAsync(key, hashFields, flags);

    public long[] HashFieldGetTimeToLive(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None) => db.HashFieldGetTimeToLive(key, hashFields, flags);

    public Task<long[]> HashFieldGetTimeToLiveAsync(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None) => db.HashFieldGetTimeToLiveAsync(key, hashFields, flags);

    public PersistResult[] HashFieldPersist(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None) => db.HashFieldPersist(key, hashFields, flags);

    public Task<PersistResult[]> HashFieldPersistAsync(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None) => db.HashFieldPersistAsync(key, hashFields, flags);

    public HashEntry[] HashGetAll(RedisKey key, CommandFlags flags = CommandFlags.None) => db.HashGetAll(key, flags);

    public Task<HashEntry[]> HashGetAllAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.HashGetAllAsync(key, flags);

    public long HashIncrement(RedisKey key, RedisValue hashField, long value = 1, CommandFlags flags = CommandFlags.None) => db.HashIncrement(key, hashField, value, flags);

    public double HashIncrement(RedisKey key, RedisValue hashField, double value, CommandFlags flags = CommandFlags.None) => db.HashIncrement(key, hashField, value, flags);

    public Task<long> HashIncrementAsync(RedisKey key, RedisValue hashField, long value = 1, CommandFlags flags = CommandFlags.None) => db.HashIncrementAsync(key, hashField, value, flags);

    public Task<double> HashIncrementAsync(RedisKey key, RedisValue hashField, double value, CommandFlags flags = CommandFlags.None) => db.HashIncrementAsync(key, hashField, value, flags);

    public RedisValue[] HashKeys(RedisKey key, CommandFlags flags = CommandFlags.None) => db.HashKeys(key, flags);

    public Task<RedisValue[]> HashKeysAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.HashKeysAsync(key, flags);

    public long HashLength(RedisKey key, CommandFlags flags = CommandFlags.None) => db.HashLength(key, flags);

    public Task<long> HashLengthAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.HashLengthAsync(key, flags);

    public RedisValue HashRandomField(RedisKey key, CommandFlags flags = CommandFlags.None) => db.HashRandomField(key, flags);

    public Task<RedisValue> HashRandomFieldAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.HashRandomFieldAsync(key, flags);

    public RedisValue[] HashRandomFields(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => db.HashRandomFields(key, count, flags);

    public Task<RedisValue[]> HashRandomFieldsAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => db.HashRandomFieldsAsync(key, count, flags);

    public HashEntry[] HashRandomFieldsWithValues(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => db.HashRandomFieldsWithValues(key, count, flags);

    public Task<HashEntry[]> HashRandomFieldsWithValuesAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => db.HashRandomFieldsWithValuesAsync(key, count, flags);

    public IEnumerable<HashEntry> HashScan(RedisKey key, RedisValue pattern, int pageSize, CommandFlags flags) => db.HashScan(key, pattern, pageSize, flags);

    public IEnumerable<HashEntry> HashScan(RedisKey key, RedisValue pattern = default, int pageSize = 250, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None) => db.HashScan(key, pattern, pageSize, cursor, pageOffset, flags);

    public IAsyncEnumerable<HashEntry> HashScanAsync(RedisKey key, RedisValue pattern = default, int pageSize = 250, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None) =>
        db.HashScanAsync(key, pattern, pageSize, cursor, pageOffset, flags);

    public IEnumerable<RedisValue> HashScanNoValues(RedisKey key, RedisValue pattern = default, int pageSize = 250, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None) =>
        db.HashScanNoValues(key, pattern, pageSize, cursor, pageOffset, flags);

    public IAsyncEnumerable<RedisValue> HashScanNoValuesAsync(RedisKey key, RedisValue pattern = default, int pageSize = 250, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None) =>
        db.HashScanNoValuesAsync(key, pattern, pageSize, cursor, pageOffset, flags);

    public long HashStringLength(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None) => db.HashStringLength(key, hashField, flags);

    public Task<long> HashStringLengthAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None) => db.HashStringLengthAsync(key, hashField, flags);

    public RedisValue[] HashValues(RedisKey key, CommandFlags flags = CommandFlags.None) => db.HashValues(key, flags);

    public Task<RedisValue[]> HashValuesAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.HashValuesAsync(key, flags);

    public bool HyperLogLogAdd(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => db.HyperLogLogAdd(key, value, flags);

    public bool HyperLogLogAdd(RedisKey key, RedisValue[] values, CommandFlags flags = CommandFlags.None) => db.HyperLogLogAdd(key, values, flags);

    public Task<bool> HyperLogLogAddAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => db.HyperLogLogAddAsync(key, value, flags);

    public Task<bool> HyperLogLogAddAsync(RedisKey key, RedisValue[] values, CommandFlags flags = CommandFlags.None) => db.HyperLogLogAddAsync(key, values, flags);

    public long HyperLogLogLength(RedisKey key, CommandFlags flags = CommandFlags.None) => db.HyperLogLogLength(key, flags);

    public long HyperLogLogLength(RedisKey[] keys, CommandFlags flags = CommandFlags.None) => db.HyperLogLogLength(keys, flags);

    public Task<long> HyperLogLogLengthAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.HyperLogLogLengthAsync(key, flags);

    public Task<long> HyperLogLogLengthAsync(RedisKey[] keys, CommandFlags flags = CommandFlags.None) => db.HyperLogLogLengthAsync(keys, flags);

    public void HyperLogLogMerge(RedisKey destination, RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None) => db.HyperLogLogMerge(destination, first, second, flags);

    public void HyperLogLogMerge(RedisKey destination, RedisKey[] sourceKeys, CommandFlags flags = CommandFlags.None) => db.HyperLogLogMerge(destination, sourceKeys, flags);

    public Task HyperLogLogMergeAsync(RedisKey destination, RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None) => db.HyperLogLogMergeAsync(destination, first, second, flags);

    public Task HyperLogLogMergeAsync(RedisKey destination, RedisKey[] sourceKeys, CommandFlags flags = CommandFlags.None) => db.HyperLogLogMergeAsync(destination, sourceKeys, flags);

    public EndPoint? IdentifyEndpoint(RedisKey key = default, CommandFlags flags = CommandFlags.None) => db.IdentifyEndpoint(key, flags);

    public Task<EndPoint?> IdentifyEndpointAsync(RedisKey key = default, CommandFlags flags = CommandFlags.None) => db.IdentifyEndpointAsync(key, flags);

    public bool IsConnected(RedisKey key, CommandFlags flags = CommandFlags.None) => db.IsConnected(key, flags);

    public bool KeyCopy(RedisKey sourceKey, RedisKey destinationKey, int destinationDatabase = -1, bool replace = false, CommandFlags flags = CommandFlags.None) => db.KeyCopy(sourceKey, destinationKey, destinationDatabase, replace, flags);

    public Task<bool> KeyCopyAsync(RedisKey sourceKey, RedisKey destinationKey, int destinationDatabase = -1, bool replace = false, CommandFlags flags = CommandFlags.None) => db.KeyCopyAsync(sourceKey, destinationKey, destinationDatabase, replace, flags);

    public byte[]? KeyDump(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyDump(key, flags);

    public Task<byte[]?> KeyDumpAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyDumpAsync(key, flags);

    public string? KeyEncoding(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyEncoding(key, flags);

    public Task<string?> KeyEncodingAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyEncodingAsync(key, flags);

    public bool KeyExists(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyExists(key, flags);

    public long KeyExists(RedisKey[] keys, CommandFlags flags = CommandFlags.None) => db.KeyExists(keys, flags);

    public Task<bool> KeyExistsAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyExistsAsync(key, flags);

    public Task<long> KeyExistsAsync(RedisKey[] keys, CommandFlags flags = CommandFlags.None) => db.KeyExistsAsync(keys, flags);

    public DateTime? KeyExpireTime(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyExpireTime(key, flags);

    public Task<DateTime?> KeyExpireTimeAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyExpireTimeAsync(key, flags);

    public long? KeyFrequency(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyFrequency(key, flags);

    public Task<long?> KeyFrequencyAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyFrequencyAsync(key, flags);

    public TimeSpan? KeyIdleTime(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyIdleTime(key, flags);

    public Task<TimeSpan?> KeyIdleTimeAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyIdleTimeAsync(key, flags);

    public void KeyMigrate(RedisKey key, EndPoint toServer, int toDatabase = 0, int timeoutMilliseconds = 0, MigrateOptions migrateOptions = MigrateOptions.None, CommandFlags flags = CommandFlags.None) =>
        db.KeyMigrate(key, toServer, toDatabase, timeoutMilliseconds, migrateOptions, flags);

    public Task KeyMigrateAsync(RedisKey key, EndPoint toServer, int toDatabase = 0, int timeoutMilliseconds = 0, MigrateOptions migrateOptions = MigrateOptions.None, CommandFlags flags = CommandFlags.None) =>
        db.KeyMigrateAsync(key, toServer, toDatabase, timeoutMilliseconds, migrateOptions, flags);

    public bool KeyMove(RedisKey key, int database, CommandFlags flags = CommandFlags.None) => db.KeyMove(key, database, flags);

    public Task<bool> KeyMoveAsync(RedisKey key, int database, CommandFlags flags = CommandFlags.None) => db.KeyMoveAsync(key, database, flags);

    public bool KeyPersist(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyPersist(key, flags);

    public Task<bool> KeyPersistAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyPersistAsync(key, flags);

    public RedisKey KeyRandom(CommandFlags flags = CommandFlags.None) => db.KeyRandom(flags);

    public Task<RedisKey> KeyRandomAsync(CommandFlags flags = CommandFlags.None) => db.KeyRandomAsync(flags);

    public long? KeyRefCount(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyRefCount(key, flags);

    public Task<long?> KeyRefCountAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyRefCountAsync(key, flags);

    public bool KeyRename(RedisKey key, RedisKey newKey, When when = When.Always, CommandFlags flags = CommandFlags.None) => db.KeyRename(key, newKey, when, flags);

    public Task<bool> KeyRenameAsync(RedisKey key, RedisKey newKey, When when = When.Always, CommandFlags flags = CommandFlags.None) => db.KeyRenameAsync(key, newKey, when, flags);

    public void KeyRestore(RedisKey key, byte[] value, TimeSpan? expiry = null, CommandFlags flags = CommandFlags.None) => db.KeyRestore(key, value, expiry, flags);

    public Task KeyRestoreAsync(RedisKey key, byte[] value, TimeSpan? expiry = null, CommandFlags flags = CommandFlags.None) => db.KeyRestoreAsync(key, value, expiry, flags);

    public TimeSpan? KeyTimeToLive(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyTimeToLive(key, flags);

    public Task<TimeSpan?> KeyTimeToLiveAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyTimeToLiveAsync(key, flags);

    public bool KeyTouch(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyTouch(key, flags);

    public long KeyTouch(RedisKey[] keys, CommandFlags flags = CommandFlags.None) => db.KeyTouch(keys, flags);

    public Task<bool> KeyTouchAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyTouchAsync(key, flags);

    public Task<long> KeyTouchAsync(RedisKey[] keys, CommandFlags flags = CommandFlags.None) => db.KeyTouchAsync(keys, flags);

    public RedisType KeyType(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyType(key, flags);

    public Task<RedisType> KeyTypeAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.KeyTypeAsync(key, flags);

    public RedisValue ListGetByIndex(RedisKey key, long index, CommandFlags flags = CommandFlags.None) => db.ListGetByIndex(key, index, flags);

    public Task<RedisValue> ListGetByIndexAsync(RedisKey key, long index, CommandFlags flags = CommandFlags.None) => db.ListGetByIndexAsync(key, index, flags);

    public long ListInsertAfter(RedisKey key, RedisValue pivot, RedisValue value, CommandFlags flags = CommandFlags.None) => db.ListInsertAfter(key, pivot, value, flags);

    public Task<long> ListInsertAfterAsync(RedisKey key, RedisValue pivot, RedisValue value, CommandFlags flags = CommandFlags.None) => db.ListInsertAfterAsync(key, pivot, value, flags);

    public long ListInsertBefore(RedisKey key, RedisValue pivot, RedisValue value, CommandFlags flags = CommandFlags.None) => db.ListInsertBefore(key, pivot, value, flags);

    public Task<long> ListInsertBeforeAsync(RedisKey key, RedisValue pivot, RedisValue value, CommandFlags flags = CommandFlags.None) => db.ListInsertBeforeAsync(key, pivot, value, flags);

    public RedisValue ListLeftPop(RedisKey key, CommandFlags flags = CommandFlags.None) => db.ListLeftPop(key, flags);

    public RedisValue[] ListLeftPop(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => db.ListLeftPop(key, count, flags);

    public ListPopResult ListLeftPop(RedisKey[] keys, long count, CommandFlags flags = CommandFlags.None) => db.ListLeftPop(keys, count, flags);

    public Task<RedisValue> ListLeftPopAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.ListLeftPopAsync(key, flags);

    public Task<RedisValue[]> ListLeftPopAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => db.ListLeftPopAsync(key, count, flags);

    public Task<ListPopResult> ListLeftPopAsync(RedisKey[] keys, long count, CommandFlags flags = CommandFlags.None) => db.ListLeftPopAsync(keys, count, flags);

    public long ListLeftPush(RedisKey key, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None) => db.ListLeftPush(key, value, when, flags);

    public long ListLeftPush(RedisKey key, RedisValue[] values, When when = When.Always, CommandFlags flags = CommandFlags.None) => db.ListLeftPush(key, values, when, flags);

    public long ListLeftPush(RedisKey key, RedisValue[] values, CommandFlags flags) => db.ListLeftPush(key, values, flags);

    public Task<long> ListLeftPushAsync(RedisKey key, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None) => db.ListLeftPushAsync(key, value, when, flags);

    public Task<long> ListLeftPushAsync(RedisKey key, RedisValue[] values, When when = When.Always, CommandFlags flags = CommandFlags.None) => db.ListLeftPushAsync(key, values, when, flags);

    public Task<long> ListLeftPushAsync(RedisKey key, RedisValue[] values, CommandFlags flags) => db.ListLeftPushAsync(key, values, flags);

    public long ListLength(RedisKey key, CommandFlags flags = CommandFlags.None) => db.ListLength(key, flags);

    public Task<long> ListLengthAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.ListLengthAsync(key, flags);

    public RedisValue ListMove(RedisKey sourceKey, RedisKey destinationKey, ListSide sourceSide, ListSide destinationSide, CommandFlags flags = CommandFlags.None) => db.ListMove(sourceKey, destinationKey, sourceSide, destinationSide, flags);

    public Task<RedisValue> ListMoveAsync(RedisKey sourceKey, RedisKey destinationKey, ListSide sourceSide, ListSide destinationSide, CommandFlags flags = CommandFlags.None) =>
        db.ListMoveAsync(sourceKey, destinationKey, sourceSide, destinationSide, flags);

    public long ListPosition(RedisKey key, RedisValue element, long rank = 1, long maxLength = 0, CommandFlags flags = CommandFlags.None) => db.ListPosition(key, element, rank, maxLength, flags);

    public Task<long> ListPositionAsync(RedisKey key, RedisValue element, long rank = 1, long maxLength = 0, CommandFlags flags = CommandFlags.None) => db.ListPositionAsync(key, element, rank, maxLength, flags);

    public long[] ListPositions(RedisKey key, RedisValue element, long count, long rank = 1, long maxLength = 0, CommandFlags flags = CommandFlags.None) => db.ListPositions(key, element, count, rank, maxLength, flags);

    public Task<long[]> ListPositionsAsync(RedisKey key, RedisValue element, long count, long rank = 1, long maxLength = 0, CommandFlags flags = CommandFlags.None) => db.ListPositionsAsync(key, element, count, rank, maxLength, flags);

    public RedisValue[] ListRange(RedisKey key, long start = 0, long stop = -1, CommandFlags flags = CommandFlags.None) => db.ListRange(key, start, stop, flags);

    public Task<RedisValue[]> ListRangeAsync(RedisKey key, long start = 0, long stop = -1, CommandFlags flags = CommandFlags.None) => db.ListRangeAsync(key, start, stop, flags);

    public long ListRemove(RedisKey key, RedisValue value, long count = 0, CommandFlags flags = CommandFlags.None) => db.ListRemove(key, value, count, flags);

    public Task<long> ListRemoveAsync(RedisKey key, RedisValue value, long count = 0, CommandFlags flags = CommandFlags.None) => db.ListRemoveAsync(key, value, count, flags);

    public RedisValue ListRightPop(RedisKey key, CommandFlags flags = CommandFlags.None) => db.ListRightPop(key, flags);

    public RedisValue[] ListRightPop(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => db.ListRightPop(key, count, flags);

    public ListPopResult ListRightPop(RedisKey[] keys, long count, CommandFlags flags = CommandFlags.None) => db.ListRightPop(keys, count, flags);

    public Task<RedisValue> ListRightPopAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.ListRightPopAsync(key, flags);

    public Task<RedisValue[]> ListRightPopAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => db.ListRightPopAsync(key, count, flags);

    public Task<ListPopResult> ListRightPopAsync(RedisKey[] keys, long count, CommandFlags flags = CommandFlags.None) => db.ListRightPopAsync(keys, count, flags);

    public RedisValue ListRightPopLeftPush(RedisKey source, RedisKey destination, CommandFlags flags = CommandFlags.None) => db.ListRightPopLeftPush(source, destination, flags);

    public Task<RedisValue> ListRightPopLeftPushAsync(RedisKey source, RedisKey destination, CommandFlags flags = CommandFlags.None) => db.ListRightPopLeftPushAsync(source, destination, flags);

    public long ListRightPush(RedisKey key, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None) => db.ListRightPush(key, value, when, flags);

    public long ListRightPush(RedisKey key, RedisValue[] values, When when = When.Always, CommandFlags flags = CommandFlags.None) => db.ListRightPush(key, values, when, flags);

    public long ListRightPush(RedisKey key, RedisValue[] values, CommandFlags flags) => db.ListRightPush(key, values, flags);

    public Task<long> ListRightPushAsync(RedisKey key, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None) => db.ListRightPushAsync(key, value, when, flags);

    public Task<long> ListRightPushAsync(RedisKey key, RedisValue[] values, When when = When.Always, CommandFlags flags = CommandFlags.None) => db.ListRightPushAsync(key, values, when, flags);

    public Task<long> ListRightPushAsync(RedisKey key, RedisValue[] values, CommandFlags flags) => db.ListRightPushAsync(key, values, flags);

    public void ListSetByIndex(RedisKey key, long index, RedisValue value, CommandFlags flags = CommandFlags.None) => db.ListSetByIndex(key, index, value, flags);

    public Task ListSetByIndexAsync(RedisKey key, long index, RedisValue value, CommandFlags flags = CommandFlags.None) => db.ListSetByIndexAsync(key, index, value, flags);

    public void ListTrim(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None) => db.ListTrim(key, start, stop, flags);

    public Task ListTrimAsync(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None) => db.ListTrimAsync(key, start, stop, flags);

    public bool LockExtend(RedisKey key, RedisValue value, TimeSpan expiry, CommandFlags flags = CommandFlags.None) => db.LockExtend(key, value, expiry, flags);

    public Task<bool> LockExtendAsync(RedisKey key, RedisValue value, TimeSpan expiry, CommandFlags flags = CommandFlags.None) => db.LockExtendAsync(key, value, expiry, flags);

    public RedisValue LockQuery(RedisKey key, CommandFlags flags = CommandFlags.None) => db.LockQuery(key, flags);

    public Task<RedisValue> LockQueryAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.LockQueryAsync(key, flags);

    public bool LockRelease(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => db.LockRelease(key, value, flags);

    public Task<bool> LockReleaseAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => db.LockReleaseAsync(key, value, flags);

    public bool LockTake(RedisKey key, RedisValue value, TimeSpan expiry, CommandFlags flags = CommandFlags.None) => db.LockTake(key, value, expiry, flags);

    public Task<bool> LockTakeAsync(RedisKey key, RedisValue value, TimeSpan expiry, CommandFlags flags = CommandFlags.None) => db.LockTakeAsync(key, value, expiry, flags);

    public TimeSpan Ping(CommandFlags flags = CommandFlags.None) => db.Ping(flags);

    public Task<TimeSpan> PingAsync(CommandFlags flags = CommandFlags.None) => db.PingAsync(flags);

    public long Publish(RedisChannel channel, RedisValue message, CommandFlags flags = CommandFlags.None) => db.Publish(channel, message, flags);

    public Task<long> PublishAsync(RedisChannel channel, RedisValue message, CommandFlags flags = CommandFlags.None) => db.PublishAsync(channel, message, flags);

    public RedisResult ScriptEvaluateReadOnly(string script, RedisKey[]? keys = null, RedisValue[]? values = null, CommandFlags flags = CommandFlags.None) => db.ScriptEvaluateReadOnly(script, keys, values, flags);

    public RedisResult ScriptEvaluateReadOnly(byte[] hash, RedisKey[]? keys = null, RedisValue[]? values = null, CommandFlags flags = CommandFlags.None) => db.ScriptEvaluateReadOnly(hash, keys, values, flags);

    public Task<RedisResult> ScriptEvaluateReadOnlyAsync(string script, RedisKey[]? keys = null, RedisValue[]? values = null, CommandFlags flags = CommandFlags.None) => db.ScriptEvaluateReadOnlyAsync(script, keys, values, flags);

    public Task<RedisResult> ScriptEvaluateReadOnlyAsync(byte[] hash, RedisKey[]? keys = null, RedisValue[]? values = null, CommandFlags flags = CommandFlags.None) => db.ScriptEvaluateReadOnlyAsync(hash, keys, values, flags);

    public bool SetAdd(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => db.SetAdd(key, value, flags);

    public long SetAdd(RedisKey key, RedisValue[] values, CommandFlags flags = CommandFlags.None) => db.SetAdd(key, values, flags);

    public Task<bool> SetAddAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => db.SetAddAsync(key, value, flags);

    public Task<long> SetAddAsync(RedisKey key, RedisValue[] values, CommandFlags flags = CommandFlags.None) => db.SetAddAsync(key, values, flags);

    public RedisValue[] SetCombine(SetOperation operation, RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None) => db.SetCombine(operation, first, second, flags);

    public RedisValue[] SetCombine(SetOperation operation, RedisKey[] keys, CommandFlags flags = CommandFlags.None) => db.SetCombine(operation, keys, flags);

    public long SetCombineAndStore(SetOperation operation, RedisKey destination, RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None) => db.SetCombineAndStore(operation, destination, first, second, flags);

    public long SetCombineAndStore(SetOperation operation, RedisKey destination, RedisKey[] keys, CommandFlags flags = CommandFlags.None) => db.SetCombineAndStore(operation, destination, keys, flags);

    public Task<long> SetCombineAndStoreAsync(SetOperation operation, RedisKey destination, RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None) => db.SetCombineAndStoreAsync(operation, destination, first, second, flags);

    public Task<long> SetCombineAndStoreAsync(SetOperation operation, RedisKey destination, RedisKey[] keys, CommandFlags flags = CommandFlags.None) => db.SetCombineAndStoreAsync(operation, destination, keys, flags);

    public Task<RedisValue[]> SetCombineAsync(SetOperation operation, RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None) => db.SetCombineAsync(operation, first, second, flags);

    public Task<RedisValue[]> SetCombineAsync(SetOperation operation, RedisKey[] keys, CommandFlags flags = CommandFlags.None) => db.SetCombineAsync(operation, keys, flags);

    public bool SetContains(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => db.SetContains(key, value, flags);

    public bool[] SetContains(RedisKey key, RedisValue[] values, CommandFlags flags = CommandFlags.None) => db.SetContains(key, values, flags);

    public Task<bool> SetContainsAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => db.SetContainsAsync(key, value, flags);

    public Task<bool[]> SetContainsAsync(RedisKey key, RedisValue[] values, CommandFlags flags = CommandFlags.None) => db.SetContainsAsync(key, values, flags);

    public long SetIntersectionLength(RedisKey[] keys, long limit = 0, CommandFlags flags = CommandFlags.None) => db.SetIntersectionLength(keys, limit, flags);

    public Task<long> SetIntersectionLengthAsync(RedisKey[] keys, long limit = 0, CommandFlags flags = CommandFlags.None) => db.SetIntersectionLengthAsync(keys, limit, flags);

    public long SetLength(RedisKey key, CommandFlags flags = CommandFlags.None) => db.SetLength(key, flags);

    public Task<long> SetLengthAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.SetLengthAsync(key, flags);

    public RedisValue[] SetMembers(RedisKey key, CommandFlags flags = CommandFlags.None) => db.SetMembers(key, flags);

    public Task<RedisValue[]> SetMembersAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.SetMembersAsync(key, flags);

    public bool SetMove(RedisKey source, RedisKey destination, RedisValue value, CommandFlags flags = CommandFlags.None) => db.SetMove(source, destination, value, flags);

    public Task<bool> SetMoveAsync(RedisKey source, RedisKey destination, RedisValue value, CommandFlags flags = CommandFlags.None) => db.SetMoveAsync(source, destination, value, flags);

    public RedisValue SetPop(RedisKey key, CommandFlags flags = CommandFlags.None) => db.SetPop(key, flags);

    public RedisValue[] SetPop(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => db.SetPop(key, count, flags);

    public Task<RedisValue> SetPopAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.SetPopAsync(key, flags);

    public Task<RedisValue[]> SetPopAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => db.SetPopAsync(key, count, flags);

    public RedisValue SetRandomMember(RedisKey key, CommandFlags flags = CommandFlags.None) => db.SetRandomMember(key, flags);

    public Task<RedisValue> SetRandomMemberAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.SetRandomMemberAsync(key, flags);

    public RedisValue[] SetRandomMembers(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => db.SetRandomMembers(key, count, flags);

    public Task<RedisValue[]> SetRandomMembersAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => db.SetRandomMembersAsync(key, count, flags);

    public bool SetRemove(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => db.SetRemove(key, value, flags);

    public long SetRemove(RedisKey key, RedisValue[] values, CommandFlags flags = CommandFlags.None) => db.SetRemove(key, values, flags);

    public Task<bool> SetRemoveAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => db.SetRemoveAsync(key, value, flags);

    public Task<long> SetRemoveAsync(RedisKey key, RedisValue[] values, CommandFlags flags = CommandFlags.None) => db.SetRemoveAsync(key, values, flags);

    public IEnumerable<RedisValue> SetScan(RedisKey key, RedisValue pattern, int pageSize, CommandFlags flags) => db.SetScan(key, pattern, pageSize, flags);

    public IEnumerable<RedisValue> SetScan(RedisKey key, RedisValue pattern = default, int pageSize = 250, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None) => db.SetScan(key, pattern, pageSize, cursor, pageOffset, flags);

    public IAsyncEnumerable<RedisValue> SetScanAsync(RedisKey key, RedisValue pattern = default, int pageSize = 250, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None) =>
        db.SetScanAsync(key, pattern, pageSize, cursor, pageOffset, flags);

    public RedisValue[] Sort(RedisKey key, long skip = 0, long take = -1, Order order = Order.Ascending, SortType sortType = SortType.Numeric, RedisValue by = default, RedisValue[]? get = null, CommandFlags flags = CommandFlags.None) =>
        db.Sort(key, skip, take, order, sortType, by, get, flags);

    public long SortAndStore(
        RedisKey destination,
        RedisKey key,
        long skip = 0,
        long take = -1,
        Order order = Order.Ascending,
        SortType sortType = SortType.Numeric,
        RedisValue by = default,
        RedisValue[]? get = null,
        CommandFlags flags = CommandFlags.None
    ) => db.SortAndStore(destination, key, skip, take, order, sortType, by, get, flags);

    public Task<long> SortAndStoreAsync(
        RedisKey destination,
        RedisKey key,
        long skip = 0,
        long take = -1,
        Order order = Order.Ascending,
        SortType sortType = SortType.Numeric,
        RedisValue by = default,
        RedisValue[]? get = null,
        CommandFlags flags = CommandFlags.None
    ) => db.SortAndStoreAsync(destination, key, skip, take, order, sortType, by, get, flags);

    public Task<RedisValue[]> SortAsync(RedisKey key, long skip = 0, long take = -1, Order order = Order.Ascending, SortType sortType = SortType.Numeric, RedisValue by = default, RedisValue[]? get = null, CommandFlags flags = CommandFlags.None) =>
        db.SortAsync(key, skip, take, order, sortType, by, get, flags);

    public bool SortedSetAdd(RedisKey key, RedisValue member, double score, CommandFlags flags) => db.SortedSetAdd(key, member, score, flags);

    public bool SortedSetAdd(RedisKey key, RedisValue member, double score, When when, CommandFlags flags = CommandFlags.None) => db.SortedSetAdd(key, member, score, when, flags);

    public bool SortedSetAdd(RedisKey key, RedisValue member, double score, SortedSetWhen when = SortedSetWhen.Always, CommandFlags flags = CommandFlags.None) => db.SortedSetAdd(key, member, score, when, flags);

    public long SortedSetAdd(RedisKey key, SortedSetEntry[] values, CommandFlags flags) => db.SortedSetAdd(key, values, flags);

    public long SortedSetAdd(RedisKey key, SortedSetEntry[] values, When when, CommandFlags flags = CommandFlags.None) => db.SortedSetAdd(key, values, when, flags);

    public long SortedSetAdd(RedisKey key, SortedSetEntry[] values, SortedSetWhen when = SortedSetWhen.Always, CommandFlags flags = CommandFlags.None) => db.SortedSetAdd(key, values, when, flags);

    public Task<bool> SortedSetAddAsync(RedisKey key, RedisValue member, double score, CommandFlags flags) => db.SortedSetAddAsync(key, member, score, flags);

    public Task<bool> SortedSetAddAsync(RedisKey key, RedisValue member, double score, When when, CommandFlags flags = CommandFlags.None) => db.SortedSetAddAsync(key, member, score, when, flags);

    public Task<bool> SortedSetAddAsync(RedisKey key, RedisValue member, double score, SortedSetWhen when = SortedSetWhen.Always, CommandFlags flags = CommandFlags.None) => db.SortedSetAddAsync(key, member, score, when, flags);

    public Task<long> SortedSetAddAsync(RedisKey key, SortedSetEntry[] values, CommandFlags flags) => db.SortedSetAddAsync(key, values, flags);

    public Task<long> SortedSetAddAsync(RedisKey key, SortedSetEntry[] values, When when, CommandFlags flags = CommandFlags.None) => db.SortedSetAddAsync(key, values, when, flags);

    public Task<long> SortedSetAddAsync(RedisKey key, SortedSetEntry[] values, SortedSetWhen when = SortedSetWhen.Always, CommandFlags flags = CommandFlags.None) => db.SortedSetAddAsync(key, values, when, flags);

    public RedisValue[] SortedSetCombine(SetOperation operation, RedisKey[] keys, double[]? weights = null, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None) => db.SortedSetCombine(operation, keys, weights, aggregate, flags);

    public long SortedSetCombineAndStore(SetOperation operation, RedisKey destination, RedisKey first, RedisKey second, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None) =>
        db.SortedSetCombineAndStore(operation, destination, first, second, aggregate, flags);

    public long SortedSetCombineAndStore(SetOperation operation, RedisKey destination, RedisKey[] keys, double[]? weights = null, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None) =>
        db.SortedSetCombineAndStore(operation, destination, keys, weights, aggregate, flags);

    public Task<long> SortedSetCombineAndStoreAsync(SetOperation operation, RedisKey destination, RedisKey first, RedisKey second, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None) =>
        db.SortedSetCombineAndStoreAsync(operation, destination, first, second, aggregate, flags);

    public Task<long> SortedSetCombineAndStoreAsync(SetOperation operation, RedisKey destination, RedisKey[] keys, double[]? weights = null, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None) =>
        db.SortedSetCombineAndStoreAsync(operation, destination, keys, weights, aggregate, flags);

    public Task<RedisValue[]> SortedSetCombineAsync(SetOperation operation, RedisKey[] keys, double[]? weights = null, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None) =>
        db.SortedSetCombineAsync(operation, keys, weights, aggregate, flags);

    public SortedSetEntry[] SortedSetCombineWithScores(SetOperation operation, RedisKey[] keys, double[]? weights = null, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None) =>
        db.SortedSetCombineWithScores(operation, keys, weights, aggregate, flags);

    public Task<SortedSetEntry[]> SortedSetCombineWithScoresAsync(SetOperation operation, RedisKey[] keys, double[]? weights = null, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None) =>
        db.SortedSetCombineWithScoresAsync(operation, keys, weights, aggregate, flags);

    public double SortedSetDecrement(RedisKey key, RedisValue member, double value, CommandFlags flags = CommandFlags.None) => db.SortedSetDecrement(key, member, value, flags);

    public Task<double> SortedSetDecrementAsync(RedisKey key, RedisValue member, double value, CommandFlags flags = CommandFlags.None) => db.SortedSetDecrementAsync(key, member, value, flags);

    public double SortedSetIncrement(RedisKey key, RedisValue member, double value, CommandFlags flags = CommandFlags.None) => db.SortedSetIncrement(key, member, value, flags);

    public Task<double> SortedSetIncrementAsync(RedisKey key, RedisValue member, double value, CommandFlags flags = CommandFlags.None) => db.SortedSetIncrementAsync(key, member, value, flags);

    public long SortedSetIntersectionLength(RedisKey[] keys, long limit = 0, CommandFlags flags = CommandFlags.None) => db.SortedSetIntersectionLength(keys, limit, flags);

    public Task<long> SortedSetIntersectionLengthAsync(RedisKey[] keys, long limit = 0, CommandFlags flags = CommandFlags.None) => db.SortedSetIntersectionLengthAsync(keys, limit, flags);

    public long SortedSetLength(RedisKey key, double min = double.NegativeInfinity, double max = double.PositiveInfinity, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None) => db.SortedSetLength(key, min, max, exclude, flags);

    public Task<long> SortedSetLengthAsync(RedisKey key, double min = double.NegativeInfinity, double max = double.PositiveInfinity, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None) =>
        db.SortedSetLengthAsync(key, min, max, exclude, flags);

    public long SortedSetLengthByValue(RedisKey key, RedisValue min, RedisValue max, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None) => db.SortedSetLengthByValue(key, min, max, exclude, flags);

    public Task<long> SortedSetLengthByValueAsync(RedisKey key, RedisValue min, RedisValue max, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None) => db.SortedSetLengthByValueAsync(key, min, max, exclude, flags);

    public SortedSetEntry? SortedSetPop(RedisKey key, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) => db.SortedSetPop(key, order, flags);

    public SortedSetEntry[] SortedSetPop(RedisKey key, long count, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) => db.SortedSetPop(key, count, order, flags);

    public SortedSetPopResult SortedSetPop(RedisKey[] keys, long count, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) => db.SortedSetPop(keys, count, order, flags);

    public Task<SortedSetEntry?> SortedSetPopAsync(RedisKey key, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) => db.SortedSetPopAsync(key, order, flags);

    public Task<SortedSetEntry[]> SortedSetPopAsync(RedisKey key, long count, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) => db.SortedSetPopAsync(key, count, order, flags);

    public Task<SortedSetPopResult> SortedSetPopAsync(RedisKey[] keys, long count, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) => db.SortedSetPopAsync(keys, count, order, flags);

    public RedisValue SortedSetRandomMember(RedisKey key, CommandFlags flags = CommandFlags.None) => db.SortedSetRandomMember(key, flags);

    public Task<RedisValue> SortedSetRandomMemberAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.SortedSetRandomMemberAsync(key, flags);

    public RedisValue[] SortedSetRandomMembers(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => db.SortedSetRandomMembers(key, count, flags);

    public Task<RedisValue[]> SortedSetRandomMembersAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => db.SortedSetRandomMembersAsync(key, count, flags);

    public SortedSetEntry[] SortedSetRandomMembersWithScores(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => db.SortedSetRandomMembersWithScores(key, count, flags);

    public Task<SortedSetEntry[]> SortedSetRandomMembersWithScoresAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => db.SortedSetRandomMembersWithScoresAsync(key, count, flags);

    public long SortedSetRangeAndStore(
        RedisKey sourceKey,
        RedisKey destinationKey,
        RedisValue start,
        RedisValue stop,
        SortedSetOrder sortedSetOrder = SortedSetOrder.ByRank,
        Exclude exclude = Exclude.None,
        Order order = Order.Ascending,
        long skip = 0,
        long? take = null,
        CommandFlags flags = CommandFlags.None
    ) => db.SortedSetRangeAndStore(sourceKey, destinationKey, start, stop, sortedSetOrder, exclude, order, skip, take, flags);

    public Task<long> SortedSetRangeAndStoreAsync(
        RedisKey sourceKey,
        RedisKey destinationKey,
        RedisValue start,
        RedisValue stop,
        SortedSetOrder sortedSetOrder = SortedSetOrder.ByRank,
        Exclude exclude = Exclude.None,
        Order order = Order.Ascending,
        long skip = 0,
        long? take = null,
        CommandFlags flags = CommandFlags.None
    ) => db.SortedSetRangeAndStoreAsync(sourceKey, destinationKey, start, stop, sortedSetOrder, exclude, order, skip, take, flags);

    public RedisValue[] SortedSetRangeByRank(RedisKey key, long start = 0, long stop = -1, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) => db.SortedSetRangeByRank(key, start, stop, order, flags);

    public Task<RedisValue[]> SortedSetRangeByRankAsync(RedisKey key, long start = 0, long stop = -1, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) => db.SortedSetRangeByRankAsync(key, start, stop, order, flags);

    public SortedSetEntry[] SortedSetRangeByRankWithScores(RedisKey key, long start = 0, long stop = -1, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) => db.SortedSetRangeByRankWithScores(key, start, stop, order, flags);

    public Task<SortedSetEntry[]> SortedSetRangeByRankWithScoresAsync(RedisKey key, long start = 0, long stop = -1, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) =>
        db.SortedSetRangeByRankWithScoresAsync(key, start, stop, order, flags);

    public RedisValue[] SortedSetRangeByScore(
        RedisKey key,
        double start = double.NegativeInfinity,
        double stop = double.PositiveInfinity,
        Exclude exclude = Exclude.None,
        Order order = Order.Ascending,
        long skip = 0,
        long take = -1,
        CommandFlags flags = CommandFlags.None
    ) => db.SortedSetRangeByScore(key, start, stop, exclude, order, skip, take, flags);

    public Task<RedisValue[]> SortedSetRangeByScoreAsync(
        RedisKey key,
        double start = double.NegativeInfinity,
        double stop = double.PositiveInfinity,
        Exclude exclude = Exclude.None,
        Order order = Order.Ascending,
        long skip = 0,
        long take = -1,
        CommandFlags flags = CommandFlags.None
    ) => db.SortedSetRangeByScoreAsync(key, start, stop, exclude, order, skip, take, flags);

    public SortedSetEntry[] SortedSetRangeByScoreWithScores(
        RedisKey key,
        double start = double.NegativeInfinity,
        double stop = double.PositiveInfinity,
        Exclude exclude = Exclude.None,
        Order order = Order.Ascending,
        long skip = 0,
        long take = -1,
        CommandFlags flags = CommandFlags.None
    ) => db.SortedSetRangeByScoreWithScores(key, start, stop, exclude, order, skip, take, flags);

    public Task<SortedSetEntry[]> SortedSetRangeByScoreWithScoresAsync(
        RedisKey key,
        double start = double.NegativeInfinity,
        double stop = double.PositiveInfinity,
        Exclude exclude = Exclude.None,
        Order order = Order.Ascending,
        long skip = 0,
        long take = -1,
        CommandFlags flags = CommandFlags.None
    ) => db.SortedSetRangeByScoreWithScoresAsync(key, start, stop, exclude, order, skip, take, flags);

    public RedisValue[] SortedSetRangeByValue(RedisKey key, RedisValue min, RedisValue max, Exclude exclude, long skip, long take = -1, CommandFlags flags = CommandFlags.None) => db.SortedSetRangeByValue(key, min, max, exclude, skip, take, flags);

    public RedisValue[] SortedSetRangeByValue(RedisKey key, RedisValue min = default, RedisValue max = default, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1, CommandFlags flags = CommandFlags.None) =>
        db.SortedSetRangeByValue(key, min, max, exclude, order, skip, take, flags);

    public Task<RedisValue[]> SortedSetRangeByValueAsync(RedisKey key, RedisValue min, RedisValue max, Exclude exclude, long skip, long take = -1, CommandFlags flags = CommandFlags.None) =>
        db.SortedSetRangeByValueAsync(key, min, max, exclude, skip, take, flags);

    public Task<RedisValue[]> SortedSetRangeByValueAsync(
        RedisKey key,
        RedisValue min = default,
        RedisValue max = default,
        Exclude exclude = Exclude.None,
        Order order = Order.Ascending,
        long skip = 0,
        long take = -1,
        CommandFlags flags = CommandFlags.None
    ) => db.SortedSetRangeByValueAsync(key, min, max, exclude, order, skip, take, flags);

    public long? SortedSetRank(RedisKey key, RedisValue member, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) => db.SortedSetRank(key, member, order, flags);

    public Task<long?> SortedSetRankAsync(RedisKey key, RedisValue member, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) => db.SortedSetRankAsync(key, member, order, flags);

    public bool SortedSetRemove(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.SortedSetRemove(key, member, flags);

    public long SortedSetRemove(RedisKey key, RedisValue[] members, CommandFlags flags = CommandFlags.None) => db.SortedSetRemove(key, members, flags);

    public Task<bool> SortedSetRemoveAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.SortedSetRemoveAsync(key, member, flags);

    public Task<long> SortedSetRemoveAsync(RedisKey key, RedisValue[] members, CommandFlags flags = CommandFlags.None) => db.SortedSetRemoveAsync(key, members, flags);

    public long SortedSetRemoveRangeByRank(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None) => db.SortedSetRemoveRangeByRank(key, start, stop, flags);

    public Task<long> SortedSetRemoveRangeByRankAsync(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None) => db.SortedSetRemoveRangeByRankAsync(key, start, stop, flags);

    public long SortedSetRemoveRangeByScore(RedisKey key, double start, double stop, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None) => db.SortedSetRemoveRangeByScore(key, start, stop, exclude, flags);

    public Task<long> SortedSetRemoveRangeByScoreAsync(RedisKey key, double start, double stop, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None) => db.SortedSetRemoveRangeByScoreAsync(key, start, stop, exclude, flags);

    public long SortedSetRemoveRangeByValue(RedisKey key, RedisValue min, RedisValue max, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None) => db.SortedSetRemoveRangeByValue(key, min, max, exclude, flags);

    public Task<long> SortedSetRemoveRangeByValueAsync(RedisKey key, RedisValue min, RedisValue max, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None) => db.SortedSetRemoveRangeByValueAsync(key, min, max, exclude, flags);

    public IEnumerable<SortedSetEntry> SortedSetScan(RedisKey key, RedisValue pattern, int pageSize, CommandFlags flags) => db.SortedSetScan(key, pattern, pageSize, flags);

    public IEnumerable<SortedSetEntry> SortedSetScan(RedisKey key, RedisValue pattern = default, int pageSize = 250, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None) =>
        db.SortedSetScan(key, pattern, pageSize, cursor, pageOffset, flags);

    public IAsyncEnumerable<SortedSetEntry> SortedSetScanAsync(RedisKey key, RedisValue pattern = default, int pageSize = 250, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None) =>
        db.SortedSetScanAsync(key, pattern, pageSize, cursor, pageOffset, flags);

    public double? SortedSetScore(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.SortedSetScore(key, member, flags);

    public Task<double?> SortedSetScoreAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.SortedSetScoreAsync(key, member, flags);

    public double?[] SortedSetScores(RedisKey key, RedisValue[] members, CommandFlags flags = CommandFlags.None) => db.SortedSetScores(key, members, flags);

    public Task<double?[]> SortedSetScoresAsync(RedisKey key, RedisValue[] members, CommandFlags flags = CommandFlags.None) => db.SortedSetScoresAsync(key, members, flags);

    public bool SortedSetUpdate(RedisKey key, RedisValue member, double score, SortedSetWhen when = SortedSetWhen.Always, CommandFlags flags = CommandFlags.None) => db.SortedSetUpdate(key, member, score, when, flags);

    public long SortedSetUpdate(RedisKey key, SortedSetEntry[] values, SortedSetWhen when = SortedSetWhen.Always, CommandFlags flags = CommandFlags.None) => db.SortedSetUpdate(key, values, when, flags);

    public Task<bool> SortedSetUpdateAsync(RedisKey key, RedisValue member, double score, SortedSetWhen when = SortedSetWhen.Always, CommandFlags flags = CommandFlags.None) => db.SortedSetUpdateAsync(key, member, score, when, flags);

    public Task<long> SortedSetUpdateAsync(RedisKey key, SortedSetEntry[] values, SortedSetWhen when = SortedSetWhen.Always, CommandFlags flags = CommandFlags.None) => db.SortedSetUpdateAsync(key, values, when, flags);

    public long StreamAcknowledge(RedisKey key, RedisValue groupName, RedisValue messageId, CommandFlags flags = CommandFlags.None) => db.StreamAcknowledge(key, groupName, messageId, flags);

    public long StreamAcknowledge(RedisKey key, RedisValue groupName, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None) => db.StreamAcknowledge(key, groupName, messageIds, flags);

    public Task<long> StreamAcknowledgeAsync(RedisKey key, RedisValue groupName, RedisValue messageId, CommandFlags flags = CommandFlags.None) => db.StreamAcknowledgeAsync(key, groupName, messageId, flags);

    public Task<long> StreamAcknowledgeAsync(RedisKey key, RedisValue groupName, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None) => db.StreamAcknowledgeAsync(key, groupName, messageIds, flags);

    public RedisValue StreamAdd(RedisKey key, RedisValue streamField, RedisValue streamValue, RedisValue? messageId = null, int? maxLength = null, bool useApproximateMaxLength = false, CommandFlags flags = CommandFlags.None) =>
        db.StreamAdd(key, streamField, streamValue, messageId, maxLength, useApproximateMaxLength, flags);

    public RedisValue StreamAdd(RedisKey key, NameValueEntry[] streamPairs, RedisValue? messageId = null, int? maxLength = null, bool useApproximateMaxLength = false, CommandFlags flags = CommandFlags.None) =>
        db.StreamAdd(key, streamPairs, messageId, maxLength, useApproximateMaxLength, flags);

    public Task<RedisValue> StreamAddAsync(RedisKey key, RedisValue streamField, RedisValue streamValue, RedisValue? messageId = null, int? maxLength = null, bool useApproximateMaxLength = false, CommandFlags flags = CommandFlags.None) =>
        db.StreamAddAsync(key, streamField, streamValue, messageId, maxLength, useApproximateMaxLength, flags);

    public Task<RedisValue> StreamAddAsync(RedisKey key, NameValueEntry[] streamPairs, RedisValue? messageId = null, int? maxLength = null, bool useApproximateMaxLength = false, CommandFlags flags = CommandFlags.None) =>
        db.StreamAddAsync(key, streamPairs, messageId, maxLength, useApproximateMaxLength, flags);

    public StreamAutoClaimResult StreamAutoClaim(RedisKey key, RedisValue consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, RedisValue startAtId, int? count = null, CommandFlags flags = CommandFlags.None) =>
        db.StreamAutoClaim(key, consumerGroup, claimingConsumer, minIdleTimeInMs, startAtId, count, flags);

    public Task<StreamAutoClaimResult> StreamAutoClaimAsync(RedisKey key, RedisValue consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, RedisValue startAtId, int? count = null, CommandFlags flags = CommandFlags.None) =>
        db.StreamAutoClaimAsync(key, consumerGroup, claimingConsumer, minIdleTimeInMs, startAtId, count, flags);

    public StreamAutoClaimIdsOnlyResult StreamAutoClaimIdsOnly(RedisKey key, RedisValue consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, RedisValue startAtId, int? count = null, CommandFlags flags = CommandFlags.None) =>
        db.StreamAutoClaimIdsOnly(key, consumerGroup, claimingConsumer, minIdleTimeInMs, startAtId, count, flags);

    public Task<StreamAutoClaimIdsOnlyResult> StreamAutoClaimIdsOnlyAsync(RedisKey key, RedisValue consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, RedisValue startAtId, int? count = null, CommandFlags flags = CommandFlags.None) =>
        db.StreamAutoClaimIdsOnlyAsync(key, consumerGroup, claimingConsumer, minIdleTimeInMs, startAtId, count, flags);

    public StreamEntry[] StreamClaim(RedisKey key, RedisValue consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None) =>
        db.StreamClaim(key, consumerGroup, claimingConsumer, minIdleTimeInMs, messageIds, flags);

    public Task<StreamEntry[]> StreamClaimAsync(RedisKey key, RedisValue consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None) =>
        db.StreamClaimAsync(key, consumerGroup, claimingConsumer, minIdleTimeInMs, messageIds, flags);

    public RedisValue[] StreamClaimIdsOnly(RedisKey key, RedisValue consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None) =>
        db.StreamClaimIdsOnly(key, consumerGroup, claimingConsumer, minIdleTimeInMs, messageIds, flags);

    public Task<RedisValue[]> StreamClaimIdsOnlyAsync(RedisKey key, RedisValue consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None) =>
        db.StreamClaimIdsOnlyAsync(key, consumerGroup, claimingConsumer, minIdleTimeInMs, messageIds, flags);

    public bool StreamConsumerGroupSetPosition(RedisKey key, RedisValue groupName, RedisValue position, CommandFlags flags = CommandFlags.None) => db.StreamConsumerGroupSetPosition(key, groupName, position, flags);

    public Task<bool> StreamConsumerGroupSetPositionAsync(RedisKey key, RedisValue groupName, RedisValue position, CommandFlags flags = CommandFlags.None) => db.StreamConsumerGroupSetPositionAsync(key, groupName, position, flags);

    public StreamConsumerInfo[] StreamConsumerInfo(RedisKey key, RedisValue groupName, CommandFlags flags = CommandFlags.None) => db.StreamConsumerInfo(key, groupName, flags);

    public Task<StreamConsumerInfo[]> StreamConsumerInfoAsync(RedisKey key, RedisValue groupName, CommandFlags flags = CommandFlags.None) => db.StreamConsumerInfoAsync(key, groupName, flags);

    public bool StreamCreateConsumerGroup(RedisKey key, RedisValue groupName, RedisValue? position, CommandFlags flags) => db.StreamCreateConsumerGroup(key, groupName, position, flags);

    public bool StreamCreateConsumerGroup(RedisKey key, RedisValue groupName, RedisValue? position = null, bool createStream = true, CommandFlags flags = CommandFlags.None) => db.StreamCreateConsumerGroup(key, groupName, position, createStream, flags);

    public Task<bool> StreamCreateConsumerGroupAsync(RedisKey key, RedisValue groupName, RedisValue? position, CommandFlags flags) => db.StreamCreateConsumerGroupAsync(key, groupName, position, flags);

    public Task<bool> StreamCreateConsumerGroupAsync(RedisKey key, RedisValue groupName, RedisValue? position = null, bool createStream = true, CommandFlags flags = CommandFlags.None) =>
        db.StreamCreateConsumerGroupAsync(key, groupName, position, createStream, flags);

    public long StreamDelete(RedisKey key, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None) => db.StreamDelete(key, messageIds, flags);

    public Task<long> StreamDeleteAsync(RedisKey key, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None) => db.StreamDeleteAsync(key, messageIds, flags);

    public long StreamDeleteConsumer(RedisKey key, RedisValue groupName, RedisValue consumerName, CommandFlags flags = CommandFlags.None) => db.StreamDeleteConsumer(key, groupName, consumerName, flags);

    public Task<long> StreamDeleteConsumerAsync(RedisKey key, RedisValue groupName, RedisValue consumerName, CommandFlags flags = CommandFlags.None) => db.StreamDeleteConsumerAsync(key, groupName, consumerName, flags);

    public bool StreamDeleteConsumerGroup(RedisKey key, RedisValue groupName, CommandFlags flags = CommandFlags.None) => db.StreamDeleteConsumerGroup(key, groupName, flags);

    public Task<bool> StreamDeleteConsumerGroupAsync(RedisKey key, RedisValue groupName, CommandFlags flags = CommandFlags.None) => db.StreamDeleteConsumerGroupAsync(key, groupName, flags);

    public StreamGroupInfo[] StreamGroupInfo(RedisKey key, CommandFlags flags = CommandFlags.None) => db.StreamGroupInfo(key, flags);

    public Task<StreamGroupInfo[]> StreamGroupInfoAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.StreamGroupInfoAsync(key, flags);

    public StreamInfo StreamInfo(RedisKey key, CommandFlags flags = CommandFlags.None) => db.StreamInfo(key, flags);

    public Task<StreamInfo> StreamInfoAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.StreamInfoAsync(key, flags);

    public long StreamLength(RedisKey key, CommandFlags flags = CommandFlags.None) => db.StreamLength(key, flags);

    public Task<long> StreamLengthAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.StreamLengthAsync(key, flags);

    public StreamPendingInfo StreamPending(RedisKey key, RedisValue groupName, CommandFlags flags = CommandFlags.None) => db.StreamPending(key, groupName, flags);

    public Task<StreamPendingInfo> StreamPendingAsync(RedisKey key, RedisValue groupName, CommandFlags flags = CommandFlags.None) => db.StreamPendingAsync(key, groupName, flags);

    public StreamPendingMessageInfo[] StreamPendingMessages(RedisKey key, RedisValue groupName, int count, RedisValue consumerName, RedisValue? minId = null, RedisValue? maxId = null, CommandFlags flags = CommandFlags.None) =>
        db.StreamPendingMessages(key, groupName, count, consumerName, minId, maxId, flags);

    public Task<StreamPendingMessageInfo[]> StreamPendingMessagesAsync(RedisKey key, RedisValue groupName, int count, RedisValue consumerName, RedisValue? minId = null, RedisValue? maxId = null, CommandFlags flags = CommandFlags.None) =>
        db.StreamPendingMessagesAsync(key, groupName, count, consumerName, minId, maxId, flags);

    public StreamEntry[] StreamRange(RedisKey key, RedisValue? minId = null, RedisValue? maxId = null, int? count = null, Order messageOrder = Order.Ascending, CommandFlags flags = CommandFlags.None) =>
        db.StreamRange(key, minId, maxId, count, messageOrder, flags);

    public Task<StreamEntry[]> StreamRangeAsync(RedisKey key, RedisValue? minId = null, RedisValue? maxId = null, int? count = null, Order messageOrder = Order.Ascending, CommandFlags flags = CommandFlags.None) =>
        db.StreamRangeAsync(key, minId, maxId, count, messageOrder, flags);

    public StreamEntry[] StreamRead(RedisKey key, RedisValue position, int? count = null, CommandFlags flags = CommandFlags.None) => db.StreamRead(key, position, count, flags);

    public RedisStream[] StreamRead(StreamPosition[] streamPositions, int? countPerStream = null, CommandFlags flags = CommandFlags.None) => db.StreamRead(streamPositions, countPerStream, flags);

    public Task<StreamEntry[]> StreamReadAsync(RedisKey key, RedisValue position, int? count = null, CommandFlags flags = CommandFlags.None) => db.StreamReadAsync(key, position, count, flags);

    public Task<RedisStream[]> StreamReadAsync(StreamPosition[] streamPositions, int? countPerStream = null, CommandFlags flags = CommandFlags.None) => db.StreamReadAsync(streamPositions, countPerStream, flags);

    public StreamEntry[] StreamReadGroup(RedisKey key, RedisValue groupName, RedisValue consumerName, RedisValue? position, int? count, CommandFlags flags) => db.StreamReadGroup(key, groupName, consumerName, position, count, flags);

    public StreamEntry[] StreamReadGroup(RedisKey key, RedisValue groupName, RedisValue consumerName, RedisValue? position = null, int? count = null, bool noAck = false, CommandFlags flags = CommandFlags.None) =>
        db.StreamReadGroup(key, groupName, consumerName, position, count, noAck, flags);

    public RedisStream[] StreamReadGroup(StreamPosition[] streamPositions, RedisValue groupName, RedisValue consumerName, int? countPerStream, CommandFlags flags) => db.StreamReadGroup(streamPositions, groupName, consumerName, countPerStream, flags);

    public RedisStream[] StreamReadGroup(StreamPosition[] streamPositions, RedisValue groupName, RedisValue consumerName, int? countPerStream = null, bool noAck = false, CommandFlags flags = CommandFlags.None) =>
        db.StreamReadGroup(streamPositions, groupName, consumerName, countPerStream, noAck, flags);

    public Task<StreamEntry[]> StreamReadGroupAsync(RedisKey key, RedisValue groupName, RedisValue consumerName, RedisValue? position, int? count, CommandFlags flags) => db.StreamReadGroupAsync(key, groupName, consumerName, position, count, flags);

    public Task<StreamEntry[]> StreamReadGroupAsync(RedisKey key, RedisValue groupName, RedisValue consumerName, RedisValue? position = null, int? count = null, bool noAck = false, CommandFlags flags = CommandFlags.None) =>
        db.StreamReadGroupAsync(key, groupName, consumerName, position, count, noAck, flags);

    public Task<RedisStream[]> StreamReadGroupAsync(StreamPosition[] streamPositions, RedisValue groupName, RedisValue consumerName, int? countPerStream, CommandFlags flags) =>
        db.StreamReadGroupAsync(streamPositions, groupName, consumerName, countPerStream, flags);

    public Task<RedisStream[]> StreamReadGroupAsync(StreamPosition[] streamPositions, RedisValue groupName, RedisValue consumerName, int? countPerStream = null, bool noAck = false, CommandFlags flags = CommandFlags.None) =>
        db.StreamReadGroupAsync(streamPositions, groupName, consumerName, countPerStream, noAck, flags);

    public long StreamTrim(RedisKey key, int maxLength, bool useApproximateMaxLength = false, CommandFlags flags = CommandFlags.None) => db.StreamTrim(key, maxLength, useApproximateMaxLength, flags);

    public Task<long> StreamTrimAsync(RedisKey key, int maxLength, bool useApproximateMaxLength = false, CommandFlags flags = CommandFlags.None) => db.StreamTrimAsync(key, maxLength, useApproximateMaxLength, flags);

    public long StringAppend(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => db.StringAppend(key, value, flags);

    public Task<long> StringAppendAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => db.StringAppendAsync(key, value, flags);

    public long StringBitCount(RedisKey key, long start, long end, CommandFlags flags) => db.StringBitCount(key, start, end, flags);

    public long StringBitCount(RedisKey key, long start = 0, long end = -1, StringIndexType indexType = StringIndexType.Byte, CommandFlags flags = CommandFlags.None) => db.StringBitCount(key, start, end, indexType, flags);

    public Task<long> StringBitCountAsync(RedisKey key, long start, long end, CommandFlags flags) => db.StringBitCountAsync(key, start, end, flags);

    public Task<long> StringBitCountAsync(RedisKey key, long start = 0, long end = -1, StringIndexType indexType = StringIndexType.Byte, CommandFlags flags = CommandFlags.None) => db.StringBitCountAsync(key, start, end, indexType, flags);

    public long StringBitOperation(Bitwise operation, RedisKey destination, RedisKey first, RedisKey second = default, CommandFlags flags = CommandFlags.None) => db.StringBitOperation(operation, destination, first, second, flags);

    public long StringBitOperation(Bitwise operation, RedisKey destination, RedisKey[] keys, CommandFlags flags = CommandFlags.None) => db.StringBitOperation(operation, destination, keys, flags);

    public Task<long> StringBitOperationAsync(Bitwise operation, RedisKey destination, RedisKey first, RedisKey second = default, CommandFlags flags = CommandFlags.None) => db.StringBitOperationAsync(operation, destination, first, second, flags);

    public Task<long> StringBitOperationAsync(Bitwise operation, RedisKey destination, RedisKey[] keys, CommandFlags flags = CommandFlags.None) => db.StringBitOperationAsync(operation, destination, keys, flags);

    public long StringBitPosition(RedisKey key, bool bit, long start, long end, CommandFlags flags) => db.StringBitPosition(key, bit, start, end, flags);

    public long StringBitPosition(RedisKey key, bool bit, long start = 0, long end = -1, StringIndexType indexType = StringIndexType.Byte, CommandFlags flags = CommandFlags.None) => db.StringBitPosition(key, bit, start, end, indexType, flags);

    public Task<long> StringBitPositionAsync(RedisKey key, bool bit, long start, long end, CommandFlags flags) => db.StringBitPositionAsync(key, bit, start, end, flags);

    public Task<long> StringBitPositionAsync(RedisKey key, bool bit, long start = 0, long end = -1, StringIndexType indexType = StringIndexType.Byte, CommandFlags flags = CommandFlags.None) =>
        db.StringBitPositionAsync(key, bit, start, end, indexType, flags);

    public long StringDecrement(RedisKey key, long value = 1, CommandFlags flags = CommandFlags.None) => db.StringDecrement(key, value, flags);

    public double StringDecrement(RedisKey key, double value, CommandFlags flags = CommandFlags.None) => db.StringDecrement(key, value, flags);

    public Task<long> StringDecrementAsync(RedisKey key, long value = 1, CommandFlags flags = CommandFlags.None) => db.StringDecrementAsync(key, value, flags);

    public Task<double> StringDecrementAsync(RedisKey key, double value, CommandFlags flags = CommandFlags.None) => db.StringDecrementAsync(key, value, flags);

    public RedisValue StringGet(RedisKey key, CommandFlags flags = CommandFlags.None) => db.StringGet(key, flags);

    public RedisValue[] StringGet(RedisKey[] keys, CommandFlags flags = CommandFlags.None) => db.StringGet(keys, flags);

    public Task<RedisValue> StringGetAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.StringGetAsync(key, flags);

    public Task<RedisValue[]> StringGetAsync(RedisKey[] keys, CommandFlags flags = CommandFlags.None) => db.StringGetAsync(keys, flags);

    public bool StringGetBit(RedisKey key, long offset, CommandFlags flags = CommandFlags.None) => db.StringGetBit(key, offset, flags);

    public Task<bool> StringGetBitAsync(RedisKey key, long offset, CommandFlags flags = CommandFlags.None) => db.StringGetBitAsync(key, offset, flags);

    public RedisValue StringGetDelete(RedisKey key, CommandFlags flags = CommandFlags.None) => db.StringGetDelete(key, flags);

    public Task<RedisValue> StringGetDeleteAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.StringGetDeleteAsync(key, flags);

    public Lease<byte>? StringGetLease(RedisKey key, CommandFlags flags = CommandFlags.None) => db.StringGetLease(key, flags);

    public Task<Lease<byte>?> StringGetLeaseAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.StringGetLeaseAsync(key, flags);

    public RedisValue StringGetRange(RedisKey key, long start, long end, CommandFlags flags = CommandFlags.None) => db.StringGetRange(key, start, end, flags);

    public Task<RedisValue> StringGetRangeAsync(RedisKey key, long start, long end, CommandFlags flags = CommandFlags.None) => db.StringGetRangeAsync(key, start, end, flags);

    public RedisValue StringGetSet(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => db.StringGetSet(key, value, flags);

    public Task<RedisValue> StringGetSetAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None) => db.StringGetSetAsync(key, value, flags);

    public RedisValue StringGetSetExpiry(RedisKey key, TimeSpan? expiry, CommandFlags flags = CommandFlags.None) => db.StringGetSetExpiry(key, expiry, flags);

    public RedisValue StringGetSetExpiry(RedisKey key, DateTime expiry, CommandFlags flags = CommandFlags.None) => db.StringGetSetExpiry(key, expiry, flags);

    public Task<RedisValue> StringGetSetExpiryAsync(RedisKey key, TimeSpan? expiry, CommandFlags flags = CommandFlags.None) => db.StringGetSetExpiryAsync(key, expiry, flags);

    public Task<RedisValue> StringGetSetExpiryAsync(RedisKey key, DateTime expiry, CommandFlags flags = CommandFlags.None) => db.StringGetSetExpiryAsync(key, expiry, flags);

    public RedisValueWithExpiry StringGetWithExpiry(RedisKey key, CommandFlags flags = CommandFlags.None) => db.StringGetWithExpiry(key, flags);

    public Task<RedisValueWithExpiry> StringGetWithExpiryAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.StringGetWithExpiryAsync(key, flags);

    public long StringIncrement(RedisKey key, long value = 1, CommandFlags flags = CommandFlags.None) => db.StringIncrement(key, value, flags);

    public double StringIncrement(RedisKey key, double value, CommandFlags flags = CommandFlags.None) => db.StringIncrement(key, value, flags);

    public Task<long> StringIncrementAsync(RedisKey key, long value = 1, CommandFlags flags = CommandFlags.None) => db.StringIncrementAsync(key, value, flags);

    public Task<double> StringIncrementAsync(RedisKey key, double value, CommandFlags flags = CommandFlags.None) => db.StringIncrementAsync(key, value, flags);

    public long StringLength(RedisKey key, CommandFlags flags = CommandFlags.None) => db.StringLength(key, flags);

    public Task<long> StringLengthAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.StringLengthAsync(key, flags);

    public string? StringLongestCommonSubsequence(RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None) => db.StringLongestCommonSubsequence(first, second, flags);

    public Task<string?> StringLongestCommonSubsequenceAsync(RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None) => db.StringLongestCommonSubsequenceAsync(first, second, flags);

    public long StringLongestCommonSubsequenceLength(RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None) => db.StringLongestCommonSubsequenceLength(first, second, flags);

    public Task<long> StringLongestCommonSubsequenceLengthAsync(RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None) => db.StringLongestCommonSubsequenceLengthAsync(first, second, flags);

    public LCSMatchResult StringLongestCommonSubsequenceWithMatches(RedisKey first, RedisKey second, long minLength = 0, CommandFlags flags = CommandFlags.None) => db.StringLongestCommonSubsequenceWithMatches(first, second, minLength, flags);

    public Task<LCSMatchResult> StringLongestCommonSubsequenceWithMatchesAsync(RedisKey first, RedisKey second, long minLength = 0, CommandFlags flags = CommandFlags.None) =>
        db.StringLongestCommonSubsequenceWithMatchesAsync(first, second, minLength, flags);

    public bool StringSet(RedisKey key, RedisValue value, TimeSpan? expiry, When when) => db.StringSet(key, value, expiry, when);

    public bool StringSet(RedisKey key, RedisValue value, TimeSpan? expiry, When when, CommandFlags flags) => db.StringSet(key, value, expiry, when, flags);

    public bool StringSet(RedisKey key, RedisValue value, TimeSpan? expiry = null, bool keepTtl = false, When when = When.Always, CommandFlags flags = CommandFlags.None) => db.StringSet(key, value, expiry, keepTtl, when, flags);

    public bool StringSet(KeyValuePair<RedisKey, RedisValue>[] values, When when = When.Always, CommandFlags flags = CommandFlags.None) => db.StringSet(values, when, flags);

    public RedisValue StringSetAndGet(RedisKey key, RedisValue value, TimeSpan? expiry, When when, CommandFlags flags) => db.StringSetAndGet(key, value, expiry, when, flags);

    public RedisValue StringSetAndGet(RedisKey key, RedisValue value, TimeSpan? expiry = null, bool keepTtl = false, When when = When.Always, CommandFlags flags = CommandFlags.None) => db.StringSetAndGet(key, value, expiry, keepTtl, when, flags);

    public Task<RedisValue> StringSetAndGetAsync(RedisKey key, RedisValue value, TimeSpan? expiry, When when, CommandFlags flags) => db.StringSetAndGetAsync(key, value, expiry, when, flags);

    public Task<RedisValue> StringSetAndGetAsync(RedisKey key, RedisValue value, TimeSpan? expiry = null, bool keepTtl = false, When when = When.Always, CommandFlags flags = CommandFlags.None) =>
        db.StringSetAndGetAsync(key, value, expiry, keepTtl, when, flags);

    public Task<bool> StringSetAsync(RedisKey key, RedisValue value, TimeSpan? expiry, When when) => db.StringSetAsync(key, value, expiry, when);

    public Task<bool> StringSetAsync(RedisKey key, RedisValue value, TimeSpan? expiry, When when, CommandFlags flags) => db.StringSetAsync(key, value, expiry, when, flags);

    public Task<bool> StringSetAsync(RedisKey key, RedisValue value, TimeSpan? expiry = null, bool keepTtl = false, When when = When.Always, CommandFlags flags = CommandFlags.None) => db.StringSetAsync(key, value, expiry, keepTtl, when, flags);

    public Task<bool> StringSetAsync(KeyValuePair<RedisKey, RedisValue>[] values, When when = When.Always, CommandFlags flags = CommandFlags.None) => db.StringSetAsync(values, when, flags);

    public bool StringSetBit(RedisKey key, long offset, bool bit, CommandFlags flags = CommandFlags.None) => db.StringSetBit(key, offset, bit, flags);

    public Task<bool> StringSetBitAsync(RedisKey key, long offset, bool bit, CommandFlags flags = CommandFlags.None) => db.StringSetBitAsync(key, offset, bit, flags);

    public RedisValue StringSetRange(RedisKey key, long offset, RedisValue value, CommandFlags flags = CommandFlags.None) => db.StringSetRange(key, offset, value, flags);

    public Task<RedisValue> StringSetRangeAsync(RedisKey key, long offset, RedisValue value, CommandFlags flags = CommandFlags.None) => db.StringSetRangeAsync(key, offset, value, flags);

    public bool TryWait(Task task) => db.TryWait(task);

    public void Wait(Task task) => db.Wait(task);

    public T Wait<T>(Task<T> task) => db.Wait(task);

    public void WaitAll(params Task[] tasks) => db.WaitAll(tasks);

    public RedisResult ScriptEvaluate(LuaScript script, object? parameters = null, CommandFlags flags = CommandFlags.None) => db.ScriptEvaluate(script, parameters, flags);

    public RedisResult ScriptEvaluate(LoadedLuaScript script, object? parameters = null, CommandFlags flags = CommandFlags.None) => db.ScriptEvaluate(script, parameters, flags);

    public Task<RedisResult> ScriptEvaluateAsync(LuaScript script, object? parameters = null, CommandFlags flags = CommandFlags.None) => db.ScriptEvaluateAsync(script, parameters, flags);

    public Task<RedisResult> ScriptEvaluateAsync(LoadedLuaScript script, object? parameters = null, CommandFlags flags = CommandFlags.None) => db.ScriptEvaluateAsync(script, parameters, flags);

    // even more Straight pass-through ...

    public RedisValue HashFieldGetAndDelete(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None) => db.HashFieldGetAndDelete(key, hashField, flags);

    public Lease<byte> HashFieldGetLeaseAndDelete(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None) => db.HashFieldGetLeaseAndDelete(key, hashField, flags);

    public RedisValue[] HashFieldGetAndDelete(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None) => db.HashFieldGetAndDelete(key, hashFields, flags);

    public RedisValue HashFieldGetAndSetExpiry(RedisKey key, RedisValue hashField, System.TimeSpan? expiry = default, bool persist = false, CommandFlags flags = CommandFlags.None) => db.HashFieldGetAndSetExpiry(key, hashField, expiry, persist, flags);

    public RedisValue HashFieldGetAndSetExpiry(RedisKey key, RedisValue hashField, System.DateTime expiry, CommandFlags flags = CommandFlags.None) => db.HashFieldGetAndSetExpiry(key, hashField, expiry, flags);

    public Lease<byte> HashFieldGetLeaseAndSetExpiry(RedisKey key, RedisValue hashField, System.TimeSpan? expiry = default, bool persist = false, CommandFlags flags = CommandFlags.None) =>
        db.HashFieldGetLeaseAndSetExpiry(key, hashField, expiry, persist, flags);

    public Lease<byte> HashFieldGetLeaseAndSetExpiry(RedisKey key, RedisValue hashField, System.DateTime expiry, CommandFlags flags = CommandFlags.None) => db.HashFieldGetLeaseAndSetExpiry(key, hashField, expiry, flags);

    public RedisValue[] HashFieldGetAndSetExpiry(RedisKey key, RedisValue[] hashFields, System.TimeSpan? expiry = default, bool persist = false, CommandFlags flags = CommandFlags.None) =>
        db.HashFieldGetAndSetExpiry(key, hashFields, expiry, persist, flags);

    public RedisValue[] HashFieldGetAndSetExpiry(RedisKey key, RedisValue[] hashFields, System.DateTime expiry, CommandFlags flags = CommandFlags.None) => db.HashFieldGetAndSetExpiry(key, hashFields, expiry, flags);

    public RedisValue HashFieldSetAndSetExpiry(RedisKey key, RedisValue field, RedisValue value, System.TimeSpan? expiry = default, bool keepTtl = false, When when = When.Always, CommandFlags flags = CommandFlags.None) =>
        db.HashFieldSetAndSetExpiry(key, field, value, expiry, keepTtl, when, flags);

    public RedisValue HashFieldSetAndSetExpiry(RedisKey key, RedisValue field, RedisValue value, System.DateTime expiry, When when = When.Always, CommandFlags flags = CommandFlags.None) =>
        db.HashFieldSetAndSetExpiry(key, field, value, expiry, when, flags);

    public RedisValue HashFieldSetAndSetExpiry(RedisKey key, HashEntry[] hashFields, System.TimeSpan? expiry = default, bool keepTtl = false, When when = When.Always, CommandFlags flags = CommandFlags.None) =>
        db.HashFieldSetAndSetExpiry(key, hashFields, expiry, keepTtl, when, flags);

    public RedisValue HashFieldSetAndSetExpiry(RedisKey key, HashEntry[] hashFields, System.DateTime expiry, When when = When.Always, CommandFlags flags = CommandFlags.None) => db.HashFieldSetAndSetExpiry(key, hashFields, expiry, when, flags);

    public StreamTrimResult StreamAcknowledgeAndDelete(RedisKey key, RedisValue groupName, StreamTrimMode mode, RedisValue messageId, CommandFlags flags = CommandFlags.None) => db.StreamAcknowledgeAndDelete(key, groupName, mode, messageId, flags);

    public StreamTrimResult[] StreamAcknowledgeAndDelete(RedisKey key, RedisValue groupName, StreamTrimMode mode, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None) => db.StreamAcknowledgeAndDelete(key, groupName, mode, messageIds, flags);

    public RedisValue StreamAdd(
        RedisKey key,
        RedisValue streamField,
        RedisValue streamValue,
        RedisValue? messageId = default,
        long? maxLength = default,
        bool useApproximateMaxLength = false,
        long? limit = default,
        StreamTrimMode trimMode = StreamTrimMode.KeepReferences,
        CommandFlags flags = CommandFlags.None
    ) => db.StreamAdd(key, streamField, streamValue, messageId, maxLength, useApproximateMaxLength, limit, trimMode, flags);

    public RedisValue StreamAdd(
        RedisKey key,
        RedisValue streamField,
        RedisValue streamValue,
        StreamIdempotentId idempotentId,
        long? maxLength = default,
        bool useApproximateMaxLength = false,
        long? limit = default,
        StreamTrimMode trimMode = StreamTrimMode.KeepReferences,
        CommandFlags flags = CommandFlags.None
    ) => db.StreamAdd(key, streamField, streamValue, idempotentId, maxLength, useApproximateMaxLength, limit, trimMode, flags);

    public RedisValue StreamAdd(
        RedisKey key,
        NameValueEntry[] streamPairs,
        RedisValue? messageId = default,
        long? maxLength = default,
        bool useApproximateMaxLength = false,
        long? limit = default,
        StreamTrimMode trimMode = StreamTrimMode.KeepReferences,
        CommandFlags flags = CommandFlags.None
    ) => db.StreamAdd(key, streamPairs, messageId, maxLength, useApproximateMaxLength, limit, trimMode, flags);

    public RedisValue StreamAdd(
        RedisKey key,
        NameValueEntry[] streamPairs,
        StreamIdempotentId idempotentId,
        long? maxLength = default,
        bool useApproximateMaxLength = false,
        long? limit = default,
        StreamTrimMode trimMode = StreamTrimMode.KeepReferences,
        CommandFlags flags = CommandFlags.None
    ) => db.StreamAdd(key, streamPairs, idempotentId, maxLength, useApproximateMaxLength, limit, trimMode, flags);

    public void StreamConfigure(RedisKey key, StreamConfiguration configuration, CommandFlags flags = CommandFlags.None) => db.StreamConfigure(key, configuration, flags);

    public StreamTrimResult[] StreamDelete(RedisKey key, RedisValue[] messageIds, StreamTrimMode mode, CommandFlags flags = CommandFlags.None) => db.StreamDelete(key, messageIds, mode, flags);

    public StreamPendingMessageInfo[] StreamPendingMessages(
        RedisKey key,
        RedisValue groupName,
        int count,
        RedisValue consumerName,
        RedisValue? minId = default,
        RedisValue? maxId = default,
        long? minIdleTimeInMs = default,
        CommandFlags flags = CommandFlags.None
    ) => db.StreamPendingMessages(key, groupName, count, consumerName, minId, maxId, minIdleTimeInMs, flags);

    public StreamEntry[] StreamReadGroup(
        RedisKey key,
        RedisValue groupName,
        RedisValue consumerName,
        RedisValue? position = default,
        int? count = default,
        bool noAck = false,
        System.TimeSpan? claimMinIdleTime = default,
        CommandFlags flags = CommandFlags.None
    ) => db.StreamReadGroup(key, groupName, consumerName, position, count, noAck, claimMinIdleTime, flags);

    public RedisStream[] StreamReadGroup(
        StreamPosition[] streamPositions,
        RedisValue groupName,
        RedisValue consumerName,
        int? countPerStream = default,
        bool noAck = false,
        System.TimeSpan? claimMinIdleTime = default,
        CommandFlags flags = CommandFlags.None
    ) => db.StreamReadGroup(streamPositions, groupName, consumerName, countPerStream, noAck, claimMinIdleTime, flags);

    public long StreamTrim(RedisKey key, long maxLength, bool useApproximateMaxLength = false, long? limit = default, StreamTrimMode mode = StreamTrimMode.KeepReferences, CommandFlags flags = CommandFlags.None) =>
        db.StreamTrim(key, maxLength, useApproximateMaxLength, limit, mode, flags);

    public long StreamTrimByMinId(RedisKey key, RedisValue minId, bool useApproximateMaxLength = false, long? limit = default, StreamTrimMode mode = StreamTrimMode.KeepReferences, CommandFlags flags = CommandFlags.None) =>
        db.StreamTrimByMinId(key, minId, useApproximateMaxLength, limit, mode, flags);

    public bool StringDelete(RedisKey key, ValueCondition when, CommandFlags flags = CommandFlags.None) => db.StringDelete(key, when, flags);

    public ValueCondition? StringDigest(RedisKey key, CommandFlags flags = CommandFlags.None) => db.StringDigest(key, flags);

    public GcraRateLimitResult StringGcraRateLimit(RedisKey key, int maxBurst, int requestsPerPeriod, double periodSeconds = 1D, int count = 1, CommandFlags flags = CommandFlags.None) =>
        db.StringGcraRateLimit(key, maxBurst, requestsPerPeriod, periodSeconds, count, flags);

    public bool StringSet(RedisKey key, RedisValue value, Expiration expiry = default, ValueCondition when = default, CommandFlags flags = CommandFlags.None) => db.StringSet(key, value, expiry, when, flags);

    public bool StringSet(KeyValuePair<RedisKey, RedisValue>[] values, When when = When.Always, Expiration expiry = default, CommandFlags flags = CommandFlags.None) => db.StringSet(values, when, expiry, flags);

    public bool VectorSetAdd(RedisKey key, VectorSetAddRequest request, CommandFlags flags = CommandFlags.None) => db.VectorSetAdd(key, request, flags);

    public long VectorSetLength(RedisKey key, CommandFlags flags = CommandFlags.None) => db.VectorSetLength(key, flags);

    public int VectorSetDimension(RedisKey key, CommandFlags flags = CommandFlags.None) => db.VectorSetDimension(key, flags);

    public Lease<float> VectorSetGetApproximateVector(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.VectorSetGetApproximateVector(key, member, flags);

    public string VectorSetGetAttributesJson(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.VectorSetGetAttributesJson(key, member, flags);

    public VectorSetInfo? VectorSetInfo(RedisKey key, CommandFlags flags = CommandFlags.None) => db.VectorSetInfo(key, flags);

    public bool VectorSetContains(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.VectorSetContains(key, member, flags);

    public Lease<RedisValue> VectorSetGetLinks(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.VectorSetGetLinks(key, member, flags);

    public Lease<VectorSetLink> VectorSetGetLinksWithScores(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.VectorSetGetLinksWithScores(key, member, flags);

    public RedisValue VectorSetRandomMember(RedisKey key, CommandFlags flags = CommandFlags.None) => db.VectorSetRandomMember(key, flags);

    public RedisValue[] VectorSetRandomMembers(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => db.VectorSetRandomMembers(key, count, flags);

    public bool VectorSetRemove(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.VectorSetRemove(key, member, flags);

    public bool VectorSetSetAttributesJson(RedisKey key, RedisValue member, string attributesJson, CommandFlags flags = CommandFlags.None) => db.VectorSetSetAttributesJson(key, member, attributesJson, flags);

    public Lease<VectorSetSimilaritySearchResult> VectorSetSimilaritySearch(RedisKey key, VectorSetSimilaritySearchRequest query, CommandFlags flags = CommandFlags.None) => db.VectorSetSimilaritySearch(key, query, flags);

    public Lease<RedisValue> VectorSetRange(RedisKey key, RedisValue start = default, RedisValue end = default, long count = -1, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None) =>
        db.VectorSetRange(key, start, end, count, exclude, flags);

    public IEnumerable<RedisValue> VectorSetRangeEnumerate(RedisKey key, RedisValue start = default, RedisValue end = default, long count = 100, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None) =>
        db.VectorSetRangeEnumerate(key, start, end, count, exclude, flags);

    public Task<RedisValue> HashFieldGetAndDeleteAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None) => db.HashFieldGetAndDeleteAsync(key, hashField, flags);

    public Task<Lease<byte>> HashFieldGetLeaseAndDeleteAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None) => db.HashFieldGetLeaseAndDeleteAsync(key, hashField, flags);

    public Task<RedisValue[]> HashFieldGetAndDeleteAsync(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None) => db.HashFieldGetAndDeleteAsync(key, hashFields, flags);

    public Task<RedisValue> HashFieldGetAndSetExpiryAsync(RedisKey key, RedisValue hashField, System.TimeSpan? expiry = default, bool persist = false, CommandFlags flags = CommandFlags.None) =>
        db.HashFieldGetAndSetExpiryAsync(key, hashField, expiry, persist, flags);

    public Task<RedisValue> HashFieldGetAndSetExpiryAsync(RedisKey key, RedisValue hashField, System.DateTime expiry, CommandFlags flags = CommandFlags.None) => db.HashFieldGetAndSetExpiryAsync(key, hashField, expiry, flags);

    public Task<Lease<byte>> HashFieldGetLeaseAndSetExpiryAsync(RedisKey key, RedisValue hashField, System.TimeSpan? expiry = default, bool persist = false, CommandFlags flags = CommandFlags.None) =>
        db.HashFieldGetLeaseAndSetExpiryAsync(key, hashField, expiry, persist, flags);

    public Task<Lease<byte>> HashFieldGetLeaseAndSetExpiryAsync(RedisKey key, RedisValue hashField, System.DateTime expiry, CommandFlags flags = CommandFlags.None) => db.HashFieldGetLeaseAndSetExpiryAsync(key, hashField, expiry, flags);

    public Task<RedisValue[]> HashFieldGetAndSetExpiryAsync(RedisKey key, RedisValue[] hashFields, System.TimeSpan? expiry = default, bool persist = false, CommandFlags flags = CommandFlags.None) =>
        db.HashFieldGetAndSetExpiryAsync(key, hashFields, expiry, persist, flags);

    public Task<RedisValue[]> HashFieldGetAndSetExpiryAsync(RedisKey key, RedisValue[] hashFields, System.DateTime expiry, CommandFlags flags = CommandFlags.None) => db.HashFieldGetAndSetExpiryAsync(key, hashFields, expiry, flags);

    public Task<RedisValue> HashFieldSetAndSetExpiryAsync(RedisKey key, RedisValue field, RedisValue value, System.TimeSpan? expiry = default, bool keepTtl = false, When when = When.Always, CommandFlags flags = CommandFlags.None) =>
        db.HashFieldSetAndSetExpiryAsync(key, field, value, expiry, keepTtl, when, flags);

    public Task<RedisValue> HashFieldSetAndSetExpiryAsync(RedisKey key, RedisValue field, RedisValue value, System.DateTime expiry, When when = When.Always, CommandFlags flags = CommandFlags.None) =>
        db.HashFieldSetAndSetExpiryAsync(key, field, value, expiry, when, flags);

    public Task<RedisValue> HashFieldSetAndSetExpiryAsync(RedisKey key, HashEntry[] hashFields, System.TimeSpan? expiry = default, bool keepTtl = false, When when = When.Always, CommandFlags flags = CommandFlags.None) =>
        db.HashFieldSetAndSetExpiryAsync(key, hashFields, expiry, keepTtl, when, flags);

    public Task<RedisValue> HashFieldSetAndSetExpiryAsync(RedisKey key, HashEntry[] hashFields, System.DateTime expiry, When when = When.Always, CommandFlags flags = CommandFlags.None) =>
        db.HashFieldSetAndSetExpiryAsync(key, hashFields, expiry, when, flags);

    public Task<StreamTrimResult> StreamAcknowledgeAndDeleteAsync(RedisKey key, RedisValue groupName, StreamTrimMode mode, RedisValue messageId, CommandFlags flags = CommandFlags.None) =>
        db.StreamAcknowledgeAndDeleteAsync(key, groupName, mode, messageId, flags);

    public Task<StreamTrimResult[]> StreamAcknowledgeAndDeleteAsync(RedisKey key, RedisValue groupName, StreamTrimMode mode, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None) =>
        db.StreamAcknowledgeAndDeleteAsync(key, groupName, mode, messageIds, flags);

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
    ) => db.StreamAddAsync(key, streamField, streamValue, messageId, maxLength, useApproximateMaxLength, limit, trimMode, flags);

    public Task<RedisValue> StreamAddAsync(
        RedisKey key,
        NameValueEntry[] streamPairs,
        RedisValue? messageId = default,
        long? maxLength = default,
        bool useApproximateMaxLength = false,
        long? limit = default,
        StreamTrimMode trimMode = StreamTrimMode.KeepReferences,
        CommandFlags flags = CommandFlags.None
    ) => db.StreamAddAsync(key, streamPairs, messageId, maxLength, useApproximateMaxLength, limit, trimMode, flags);

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
    ) => db.StreamAddAsync(key, streamField, streamValue, idempotentId, maxLength, useApproximateMaxLength, limit, trimMode, flags);

    public Task<RedisValue> StreamAddAsync(
        RedisKey key,
        NameValueEntry[] streamPairs,
        StreamIdempotentId idempotentId,
        long? maxLength = default,
        bool useApproximateMaxLength = false,
        long? limit = default,
        StreamTrimMode trimMode = StreamTrimMode.KeepReferences,
        CommandFlags flags = CommandFlags.None
    ) => db.StreamAddAsync(key, streamPairs, idempotentId, maxLength, useApproximateMaxLength, limit, trimMode, flags);

    public System.Threading.Tasks.Task StreamConfigureAsync(RedisKey key, StreamConfiguration configuration, CommandFlags flags = CommandFlags.None) => db.StreamConfigureAsync(key, configuration, flags);

    public Task<StreamTrimResult[]> StreamDeleteAsync(RedisKey key, RedisValue[] messageIds, StreamTrimMode mode, CommandFlags flags = CommandFlags.None) => db.StreamDeleteAsync(key, messageIds, mode, flags);

    public Task<StreamPendingMessageInfo[]> StreamPendingMessagesAsync(
        RedisKey key,
        RedisValue groupName,
        int count,
        RedisValue consumerName,
        RedisValue? minId = default,
        RedisValue? maxId = default,
        long? minIdleTimeInMs = default,
        CommandFlags flags = CommandFlags.None
    ) => db.StreamPendingMessagesAsync(key, groupName, count, consumerName, minId, maxId, minIdleTimeInMs, flags);

    public Task<StreamEntry[]> StreamReadGroupAsync(
        RedisKey key,
        RedisValue groupName,
        RedisValue consumerName,
        RedisValue? position = default,
        int? count = default,
        bool noAck = false,
        System.TimeSpan? claimMinIdleTime = default,
        CommandFlags flags = CommandFlags.None
    ) => db.StreamReadGroupAsync(key, groupName, consumerName, position, count, noAck, claimMinIdleTime, flags);

    public Task<long> StreamTrimAsync(RedisKey key, long maxLength, bool useApproximateMaxLength = false, long? limit = default, StreamTrimMode mode = StreamTrimMode.KeepReferences, CommandFlags flags = CommandFlags.None) =>
        db.StreamTrimAsync(key, maxLength, useApproximateMaxLength, limit, mode, flags);

    public Task<long> StreamTrimByMinIdAsync(RedisKey key, RedisValue minId, bool useApproximateMaxLength = false, long? limit = default, StreamTrimMode mode = StreamTrimMode.KeepReferences, CommandFlags flags = CommandFlags.None) =>
        db.StreamTrimByMinIdAsync(key, minId, useApproximateMaxLength, limit, mode, flags);

    public Task<bool> StringDeleteAsync(RedisKey key, ValueCondition when, CommandFlags flags = CommandFlags.None) => db.StringDeleteAsync(key, when, flags);

    public Task<ValueCondition?> StringDigestAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.StringDigestAsync(key, flags);

    public Task<GcraRateLimitResult> StringGcraRateLimitAsync(RedisKey key, int maxBurst, int requestsPerPeriod, double periodSeconds = 1D, int count = 1, CommandFlags flags = CommandFlags.None) =>
        db.StringGcraRateLimitAsync(key, maxBurst, requestsPerPeriod, periodSeconds, count, flags);

    public Task<bool> StringSetAsync(RedisKey key, RedisValue value, Expiration expiry = default, ValueCondition when = default, CommandFlags flags = CommandFlags.None) => db.StringSetAsync(key, value, expiry, when, flags);

    public Task<bool> StringSetAsync(KeyValuePair<RedisKey, RedisValue>[] values, When when = When.Always, Expiration expiry = default, CommandFlags flags = CommandFlags.None) => db.StringSetAsync(values, when, expiry, flags);

    public Task<bool> VectorSetAddAsync(RedisKey key, VectorSetAddRequest request, CommandFlags flags = CommandFlags.None) => db.VectorSetAddAsync(key, request, flags);

    public Task<long> VectorSetLengthAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.VectorSetLengthAsync(key, flags);

    public Task<int> VectorSetDimensionAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.VectorSetDimensionAsync(key, flags);

    public Task<Lease<float>> VectorSetGetApproximateVectorAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.VectorSetGetApproximateVectorAsync(key, member, flags);

    public Task<string> VectorSetGetAttributesJsonAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.VectorSetGetAttributesJsonAsync(key, member, flags);

    public Task<VectorSetInfo?> VectorSetInfoAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.VectorSetInfoAsync(key, flags);

    public Task<bool> VectorSetContainsAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.VectorSetContainsAsync(key, member, flags);

    public Task<Lease<RedisValue>> VectorSetGetLinksAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.VectorSetGetLinksAsync(key, member, flags);

    public Task<Lease<VectorSetLink>> VectorSetGetLinksWithScoresAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.VectorSetGetLinksWithScoresAsync(key, member, flags);

    public Task<RedisValue> VectorSetRandomMemberAsync(RedisKey key, CommandFlags flags = CommandFlags.None) => db.VectorSetRandomMemberAsync(key, flags);

    public Task<RedisValue[]> VectorSetRandomMembersAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None) => db.VectorSetRandomMembersAsync(key, count, flags);

    public Task<bool> VectorSetRemoveAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None) => db.VectorSetRemoveAsync(key, member, flags);

    public Task<bool> VectorSetSetAttributesJsonAsync(RedisKey key, RedisValue member, string attributesJson, CommandFlags flags = CommandFlags.None) => db.VectorSetSetAttributesJsonAsync(key, member, attributesJson, flags);

    public Task<Lease<VectorSetSimilaritySearchResult>> VectorSetSimilaritySearchAsync(RedisKey key, VectorSetSimilaritySearchRequest query, CommandFlags flags = CommandFlags.None) => db.VectorSetSimilaritySearchAsync(key, query, flags);

    public Task<Lease<RedisValue>> VectorSetRangeAsync(RedisKey key, RedisValue start = default, RedisValue end = default, long count = -1, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None) =>
        db.VectorSetRangeAsync(key, start, end, count, exclude, flags);

    public IAsyncEnumerable<RedisValue> VectorSetRangeEnumerateAsync(RedisKey key, RedisValue start = default, RedisValue end = default, long count = 100, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None) =>
        db.VectorSetRangeEnumerateAsync(key, start, end, count, exclude, flags);

    public Task<RedisStream[]> StreamReadGroupAsync(
        StreamPosition[] streamPositions,
        RedisValue groupName,
        RedisValue consumerName,
        int? countPerStream = default,
        bool noAck = false,
        System.TimeSpan? claimMinIdleTime = default,
        CommandFlags flags = CommandFlags.None
    ) => db.StreamReadGroupAsync(streamPositions, groupName, consumerName, countPerStream, noAck, claimMinIdleTime, flags);

    #endregion Straight pass-through
}
