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

    public WrappedDatabase(IDatabase db, ICommandFlagsTweaker tweaker) : base(db)
    {
        _db = db;
        _tweaker = tweaker;
    }

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

    private CommandFlags TweakGetType(CommandFlags flags, RedisKey key) =>
        _tweaker.TweakGetType(flags, key);

    private CommandFlags TweakSetType(CommandFlags flags, RedisKey[]? keys) =>
        keys is [RedisKey key] ? TweakSetType(flags, key) : TweakSetType(flags, default(RedisKey));

    private CommandFlags TweakSetType(CommandFlags flags, RedisKey key) =>
        _tweaker.TweakSetType(flags, key);
}
