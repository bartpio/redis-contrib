namespace StackExchange.Redis;

/// <summary>
/// Utilities pertaining to <see cref="CommandFlags"/>.
/// </summary>
public static class CommandFlagsExtensions
{
    /// <summary>
    /// Remove flags that control replica selection, and replace them with the specified flags.
    /// </summary>
    /// <param name="flags">Original flags (potentially containing replica selection flags).</param>
    /// <param name="replicaFlags">New replica selection flags.</param>
    /// <returns>New command flags.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="replicaFlags"/> contains any flags not related to replica selection.
    /// </exception>
    public static CommandFlags WithReplacementReplicaFlags(this CommandFlags flags, CommandFlags replicaFlags) =>
        flags.WithoutReplicaFlags() | replicaFlags.AssertReplicaFlags();

    private static CommandFlags AssertReplicaFlags(this CommandFlags replicaFlags)
    {
        if (replicaFlags.WithoutReplicaFlags() != CommandFlags.None)
            throw new ArgumentOutOfRangeException(nameof(replicaFlags), replicaFlags, "Replacement flags must not contain any non-replica flags");

        return replicaFlags;
    }

    /// <summary>
    /// Remove flags that control replica selection.
    /// </summary>
    /// <param name="flags">Original flags.</param>
    /// <returns>New command flags.</returns>
    public static CommandFlags WithoutReplicaFlags(this CommandFlags flags) =>
        flags & ~(CommandFlags.PreferMaster | CommandFlags.DemandMaster | CommandFlags.PreferReplica | CommandFlags.DemandReplica);
}
