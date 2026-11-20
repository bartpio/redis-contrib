using StackExchange.Redis;
using StackExchangeRedisCache.Contrib;

namespace StackExchangeRedisCache.Contrib.Internal;

internal sealed partial class WrappedBatch(IBatch batch, ICommandFlagsTweaker tweaker) : IBatch { }
