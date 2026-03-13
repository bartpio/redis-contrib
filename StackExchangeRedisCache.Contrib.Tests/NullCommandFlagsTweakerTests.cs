using StackExchange.Redis;

namespace StackExchangeRedisCache.Contrib.Tests;

/// <summary>
/// Unit tests for <see cref="NullCommandFlagsTweaker"/>.
/// <para>
/// <see cref="NullCommandFlagsTweaker"/> is a public API — it is the recommended default for callers
/// that want to opt into the wrapping infrastructure without actually changing any flags. These tests
/// confirm its contract (pass-through) and that the singleton <see cref="NullCommandFlagsTweaker.Instance"/>
/// is always the same object, making it safe to use as a long-lived default.
/// </para>
/// </summary>
public class NullCommandFlagsTweakerTests
{
    /// <summary>
    /// TweakGetType must return the caller's flags unchanged for every possible input, including
    /// combined flag values. Verifies it is a true no-op and not accidentally masking any bits.
    /// </summary>
    [Theory]
    [InlineData(CommandFlags.None)]
    [InlineData(CommandFlags.DemandMaster)]
    [InlineData(CommandFlags.PreferReplica)]
    [InlineData(CommandFlags.DemandReplica)]
    [InlineData(CommandFlags.FireAndForget)]
    [InlineData(CommandFlags.FireAndForget | CommandFlags.PreferReplica)]
    [InlineData(CommandFlags.NoRedirect | CommandFlags.NoScriptCache)]
    public void TweakGetType_ReturnsInputFlagsUnchanged(CommandFlags flags)
    {
        Assert.Equal(flags, NullCommandFlagsTweaker.Instance.TweakGetType(flags, default));
    }

    /// <summary>
    /// TweakSetType must return the caller's flags unchanged. Mirrors the TweakGetType requirement;
    /// both methods share the same contract on the null implementation.
    /// </summary>
    [Theory]
    [InlineData(CommandFlags.None)]
    [InlineData(CommandFlags.DemandMaster)]
    [InlineData(CommandFlags.PreferReplica)]
    [InlineData(CommandFlags.DemandReplica)]
    [InlineData(CommandFlags.FireAndForget)]
    [InlineData(CommandFlags.FireAndForget | CommandFlags.PreferReplica)]
    [InlineData(CommandFlags.NoRedirect | CommandFlags.NoScriptCache)]
    public void TweakSetType_ReturnsInputFlagsUnchanged(CommandFlags flags)
    {
        Assert.Equal(flags, NullCommandFlagsTweaker.Instance.TweakSetType(flags, default));
    }

    /// <summary>
    /// Instance must be a singleton — every access returns the same object reference.
    /// Callers pass <see cref="NullCommandFlagsTweaker.Instance"/> through DI and store it in
    /// long-lived fields, so this property must be referentially stable.
    /// </summary>
    [Fact]
    public void Instance_IsSingleton()
    {
        Assert.Same(NullCommandFlagsTweaker.Instance, NullCommandFlagsTweaker.Instance);
    }

    /// <summary>
    /// Instance must be of the concrete type <see cref="NullCommandFlagsTweaker"/>, not a subclass,
    /// so consumers can use <c>is NullCommandFlagsTweaker</c> checks if needed.
    /// </summary>
    [Fact]
    public void Instance_IsExactType()
    {
        Assert.IsType<NullCommandFlagsTweaker>(NullCommandFlagsTweaker.Instance);
    }
}
