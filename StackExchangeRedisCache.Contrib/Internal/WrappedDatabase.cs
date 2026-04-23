using StackExchange.Redis;
using StackExchangeRedisCache.Contrib;

namespace StackExchangeRedisCache.Contrib.Internal;

internal sealed partial class WrappedDatabase(IDatabase db, ICommandFlagsTweaker tweaker) : IDatabase
{
    public IBatch CreateBatch(object? asyncState = null) =>
        new WrappedBatch(db.CreateBatch(asyncState), tweaker);
}
