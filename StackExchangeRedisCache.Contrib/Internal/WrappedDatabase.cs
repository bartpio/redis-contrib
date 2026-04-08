using StackExchange.Redis;

namespace StackExchangeRedisCache.Contrib.Internal;

/// <summary>
/// Wraps a <see cref="IDatabase"/>, passing actions through to the wrapped instance.
/// For some methods (see <see cref="WrappedDatabaseBase"/>), tweaks command flags using the supplied <see cref="ICommandFlagsTweaker"/>,
/// prior to passing through to the wrapped instance.
/// </summary>
internal sealed class WrappedDatabase : WrappedDatabaseBase
{
    private readonly IDatabase _db;
    private readonly ICommandFlagsTweaker _tweaker;

    public WrappedDatabase(IDatabase db, ICommandFlagsTweaker tweaker)
        : base(db)
    {
        _db = db;
        _tweaker = tweaker;
    }

    public override IBatch CreateBatch(object? asyncState = null) => new WrappedBatch(_db.CreateBatch(asyncState), _tweaker);

    public override RedisValue HashGet(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakGetType(flags, key);
        return _db.HashGet(key, hashField, flags);
    }

    public override RedisValue[] HashGet(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakGetType(flags, key);
        return _db.HashGet(key, hashFields, flags);
    }

    public override async Task<RedisValue> HashGetAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakGetType(flags, key);
        return await _db.HashGetAsync(key, hashField, flags).ConfigureAwait(false);
    }

    public override async Task<RedisValue[]> HashGetAsync(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakGetType(flags, key);
        return await _db.HashGetAsync(key, hashFields, flags).ConfigureAwait(false);
    }

    public override Lease<byte>? HashGetLease(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakGetType(flags, key);
        return _db.HashGetLease(key, hashField, flags);
    }

    public override async Task<Lease<byte>?> HashGetLeaseAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakGetType(flags, key);
        return await _db.HashGetLeaseAsync(key, hashField, flags).ConfigureAwait(false);
    }

    public override void HashSet(RedisKey key, HashEntry[] hashFields, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakSetType(flags, key);
        _db.HashSet(key, hashFields, flags);
    }

    public override bool HashSet(RedisKey key, RedisValue hashField, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakSetType(flags, key);
        return _db.HashSet(key, hashField, value, when, flags);
    }

    public override async Task HashSetAsync(RedisKey key, HashEntry[] hashFields, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakSetType(flags, key);
        await _db.HashSetAsync(key, hashFields, flags).ConfigureAwait(false);
    }

    public override async Task<bool> HashSetAsync(RedisKey key, RedisValue hashField, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakSetType(flags, key);
        return await _db.HashSetAsync(key, hashField, value, when, flags).ConfigureAwait(false);
    }

    public override bool KeyDelete(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakSetType(flags, key);
        return _db.KeyDelete(key, flags);
    }

    public override long KeyDelete(RedisKey[] keys, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakSetTypeForKeys(flags, keys);
        return _db.KeyDelete(keys, flags);
    }

    public override async Task<bool> KeyDeleteAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakSetType(flags, key);
        return await _db.KeyDeleteAsync(key, flags).ConfigureAwait(false);
    }

    public override async Task<long> KeyDeleteAsync(RedisKey[] keys, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakSetTypeForKeys(flags, keys);
        return await _db.KeyDeleteAsync(keys, flags).ConfigureAwait(false);
    }

    public override bool KeyExpire(RedisKey key, TimeSpan? expiry, CommandFlags flags)
    {
        flags = TweakSetType(flags, key);
        return _db.KeyExpire(key, expiry, flags);
    }

    public override bool KeyExpire(RedisKey key, TimeSpan? expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakSetType(flags, key);
        return _db.KeyExpire(key, expiry, when, flags);
    }

    public override bool KeyExpire(RedisKey key, DateTime? expiry, CommandFlags flags)
    {
        flags = TweakSetType(flags, key);
        return _db.KeyExpire(key, expiry, flags);
    }

    public override bool KeyExpire(RedisKey key, DateTime? expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakSetType(flags, key);
        return _db.KeyExpire(key, expiry, when, flags);
    }

    public override async Task<bool> KeyExpireAsync(RedisKey key, TimeSpan? expiry, CommandFlags flags)
    {
        flags = TweakSetType(flags, key);
        return await _db.KeyExpireAsync(key, expiry, flags).ConfigureAwait(false);
    }

    public override async Task<bool> KeyExpireAsync(RedisKey key, TimeSpan? expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakSetType(flags, key);
        return await _db.KeyExpireAsync(key, expiry, when, flags).ConfigureAwait(false);
    }

    public override async Task<bool> KeyExpireAsync(RedisKey key, DateTime? expiry, CommandFlags flags)
    {
        flags = TweakSetType(flags, key);
        return await _db.KeyExpireAsync(key, expiry, flags).ConfigureAwait(false);
    }

    public override async Task<bool> KeyExpireAsync(RedisKey key, DateTime? expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakSetType(flags, key);
        return await _db.KeyExpireAsync(key, expiry, when, flags).ConfigureAwait(false);
    }

    public override RedisResult ScriptEvaluate(string script, RedisKey[]? keys = null, RedisValue[]? values = null, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakSetType(flags, keys);
        return _db.ScriptEvaluate(script, keys, values, flags);
    }

    public override RedisResult ScriptEvaluate(byte[] hash, RedisKey[]? keys = null, RedisValue[]? values = null, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakSetType(flags, keys);
        return _db.ScriptEvaluate(hash, keys, values, flags);
    }

    public override async Task<RedisResult> ScriptEvaluateAsync(string script, RedisKey[]? keys = null, RedisValue[]? values = null, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakSetType(flags, keys);
        return await _db.ScriptEvaluateAsync(script, keys, values, flags).ConfigureAwait(false);
    }

    public override async Task<RedisResult> ScriptEvaluateAsync(byte[] hash, RedisKey[]? keys = null, RedisValue[]? values = null, CommandFlags flags = CommandFlags.None)
    {
        flags = TweakSetType(flags, keys);
        return await _db.ScriptEvaluateAsync(hash, keys, values, flags).ConfigureAwait(false);
    }

    private CommandFlags TweakGetType(CommandFlags flags, RedisKey key) => _tweaker.TweakGetType(flags, key);

    private CommandFlags TweakSetType(CommandFlags flags, RedisKey[]? keys) => keys is [RedisKey key] ? TweakSetType(flags, key) : TweakSetType(flags, default(RedisKey));

    private CommandFlags TweakSetTypeForKeys(CommandFlags flags, RedisKey[] keys) => keys.Length > 0 ? TweakSetType(flags, keys[0]) : TweakSetType(flags, default(RedisKey));

    private CommandFlags TweakSetType(CommandFlags flags, RedisKey key) => _tweaker.TweakSetType(flags, key);
}
