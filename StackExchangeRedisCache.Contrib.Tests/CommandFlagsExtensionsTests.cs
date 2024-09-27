using StackExchange.Redis;
using static StackExchange.Redis.CommandFlags;

namespace StackExchangeRedisCache.Contrib.Tests;

public class CommandFlagsExtensionsTests
{
    [Fact]
    public void WithoutReplicaFlags_RemovesAsApplicable()
    {
        CommandFlags originals;

        originals = None;
        Assert.Equal(None, originals.WithoutReplicaFlags());

        originals = FireAndForget | NoRedirect | NoScriptCache | PreferMaster;
        Assert.Equal(FireAndForget | NoRedirect | NoScriptCache, originals.WithoutReplicaFlags());

        originals = FireAndForget | NoRedirect | NoScriptCache | PreferReplica;
        Assert.Equal(FireAndForget | NoRedirect | NoScriptCache, originals.WithoutReplicaFlags());

        originals = NoScriptCache | DemandMaster;
        Assert.Equal(NoScriptCache, originals.WithoutReplicaFlags());
    }

    [Fact]
    public void WithReplacementReplicaFlags_ReplacesAsApplicable()
    {
        CommandFlags originals;

        originals = None;
        Assert.Equal(PreferMaster, originals.WithReplacementReplicaFlags(PreferMaster));

        originals = None;
        Assert.Equal(PreferReplica, originals.WithReplacementReplicaFlags(PreferReplica));

        originals = FireAndForget | NoRedirect | NoScriptCache;
        Assert.Equal(FireAndForget | NoRedirect | NoScriptCache | PreferReplica, originals.WithReplacementReplicaFlags(PreferReplica));

        originals = FireAndForget | NoRedirect | NoScriptCache | PreferMaster;
        Assert.Equal(FireAndForget | NoRedirect | NoScriptCache | PreferReplica, originals.WithReplacementReplicaFlags(PreferReplica));

        originals = FireAndForget | NoRedirect | NoScriptCache | PreferReplica;
        Assert.Equal(FireAndForget | NoRedirect | NoScriptCache | PreferReplica, originals.WithReplacementReplicaFlags(PreferReplica));

        originals = FireAndForget | NoRedirect | NoScriptCache | DemandMaster;
        Assert.Equal(FireAndForget | NoRedirect | NoScriptCache | PreferReplica, originals.WithReplacementReplicaFlags(PreferReplica));

        originals = FireAndForget | NoRedirect | NoScriptCache | DemandReplica;
        Assert.Equal(FireAndForget | NoRedirect | NoScriptCache | PreferReplica, originals.WithReplacementReplicaFlags(PreferReplica));

        originals = FireAndForget | NoRedirect | NoScriptCache | DemandReplica;
        Assert.Equal(FireAndForget | NoRedirect | NoScriptCache, originals.WithReplacementReplicaFlags(None));
    }

    [Fact]
    public void WithReplacementReplicaFlags_ThrowsWhenArgumentBad()
    {
        var originals = None;

        Assert.Throws<ArgumentOutOfRangeException>(() => originals.WithReplacementReplicaFlags(FireAndForget));
        Assert.Throws<ArgumentOutOfRangeException>(() => originals.WithReplacementReplicaFlags(NoRedirect));
        Assert.Throws<ArgumentOutOfRangeException>(() => originals.WithReplacementReplicaFlags(NoScriptCache));
    }
}
