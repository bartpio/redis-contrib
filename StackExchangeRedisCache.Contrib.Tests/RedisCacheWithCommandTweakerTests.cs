// Portions used under MIT license from the .NET Foundation

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using StackExchange.Redis;

namespace StackExchangeRedisCache.Contrib.Tests
{
    public class RedisCacheWithCommandTweakerTests : IDisposable
    {
        public const string SkipReason = null;

        public const string InstanceName = "RedisCacheWithCommandTweakerTests";

        private readonly ICommandFlagsTweaker _tweaker;
        private readonly ServiceProvider _sp;
        private readonly IDistributedCache _cache;

        public RedisCacheWithCommandTweakerTests()
        {
            var services = new ServiceCollection();

            _tweaker = Substitute.For<ICommandFlagsTweaker>();
            _tweaker.TweakGetType(default, default).ReturnsForAnyArgs(CommandFlags.PreferMaster);
            _tweaker.TweakSetType(default, default).ReturnsForAnyArgs(CommandFlags.FireAndForget);

            string primary = Environment.GetEnvironmentVariable("StackExchangeRedisCache_Contrib_Tests_PRIMARY") ?? "localhost:6379";
            string? replica = Environment.GetEnvironmentVariable("StackExchangeRedisCache_Contrib_Tests_REPLICA") ?? "localhost:6380";

            if (replica == "NONE")
                replica = null;

#if true
            services.AddStackExchangeRedisCache(rco =>
            {
                rco.ConfigurationOptions = new()
                {
                    EndPoints = new()
                    {
                        primary ?? throw new UnreachableException("primary redis connection required for testing")
                    }
                };

                if (replica is not null)
                {
                    rco.ConfigurationOptions.EndPoints.Add(replica);
                }

                rco.InstanceName = InstanceName;
            }, _tweaker);
#endif

#if false
            services.AddStackExchangeRedisCache(rco =>
            {
                rco.ConnectionMultiplexerFactory = async () =>
                {
                    var co = new ConfigurationOptions()
                    {
                        EndPoints = new()
                        {
                            primary
                        }
                    };

                    if (replica is not null)
                    {
                        co.EndPoints.Add(replica);
                    }

                    return await ConnectionMultiplexer.ConnectAsync(co).ConfigureAwait(false);
                };

                rco.InstanceName = InstanceName;
            }, _tweaker);
#endif


#if false
            services.AddStackExchangeRedisCache(rco =>
            {
                rco.Configuration = primary;

                rco.InstanceName = InstanceName;
            }, _tweaker);
#endif


            _sp = services.BuildServiceProvider();
            _cache = _sp.GetRequiredService<IDistributedCache>();
        }

        [Fact(Skip = SkipReason)]
        public async Task SetAndGetReturnsObjectUsingDemandReplicaAsync()
        {
            _tweaker.TweakGetType(default, default).ReturnsForAnyArgs(CommandFlags.DemandReplica);
            _tweaker.TweakSetType(default, default).ReturnsForAnyArgs(CommandFlags.FireAndForget);

            var cache = _cache;
            var value = new byte[1];
            string key = "myKey";

            await cache.SetAsync(key, value);
            _tweaker.DidNotReceiveWithAnyArgs().TweakGetType(default, default);
            _tweaker.Received(1).TweakSetType(CommandFlags.None, new RedisKey("myKey").Prepend(InstanceName));
            _tweaker.ClearReceivedCalls();

            await Task.Delay(10); // replica lag

            var result = await cache.GetAsync(key);
            _tweaker.Received(1).TweakGetType(CommandFlags.None, new RedisKey("myKey").Prepend(InstanceName));
            _tweaker.DidNotReceiveWithAnyArgs().TweakSetType(default, default);
            Assert.Equal(value, result);
        }

        [Fact(Skip = SkipReason)]
        public async Task SetAndGetReturnsObjectUsingPreferReplicaAsync()
        {
            _tweaker.TweakGetType(default, default).ReturnsForAnyArgs(CommandFlags.PreferReplica);
            _tweaker.TweakSetType(default, default).ReturnsForAnyArgs(CommandFlags.FireAndForget);

            var cache = _cache;
            var value = new byte[1];
            string key = "myKey";

            await cache.SetAsync(key, value);
            _tweaker.DidNotReceiveWithAnyArgs().TweakGetType(default, default);
            _tweaker.Received(1).TweakSetType(CommandFlags.None, new RedisKey("myKey").Prepend(InstanceName));
            _tweaker.ClearReceivedCalls();

            await Task.Delay(10); // replica lag

            var result = await cache.GetAsync(key);
            _tweaker.Received(1).TweakGetType(CommandFlags.None, new RedisKey("myKey").Prepend(InstanceName));
            _tweaker.DidNotReceiveWithAnyArgs().TweakSetType(default, default);
            Assert.Equal(value, result);
        }

        [Fact]
        public void TestRedisKey()
        {
            var key = new RedisKey("myKey").Prepend(InstanceName);
            Assert.Equal($"{InstanceName}myKey", key.ToString());
        }

        public void Dispose()
        {
            _sp.Dispose();
        }

        #region RedisCacheSetAndRemoveTests from upstream

        [Fact(Skip = SkipReason)]
        public void GetMissingKeyReturnsNull()
        {
            var cache = _cache;
            string key = "non-existent-key";

            var result = cache.Get(key);
            Assert.Null(result);
        }

        [Fact(Skip = SkipReason)]
        public void SetAndGetReturnsObject()
        {
            var cache = _cache;
            var value = new byte[1];
            string key = "myKey";

            cache.Set(key, value);

            var result = cache.Get(key);
            Assert.Equal(value, result);
        }

        [Fact(Skip = SkipReason)]
        public void SetAndGetWorksWithCaseSensitiveKeys()
        {
            var cache = _cache;
            var value = new byte[1];
            string key1 = "myKey";
            string key2 = "Mykey";

            cache.Set(key1, value);

            var result = cache.Get(key1);
            Assert.Equal(value, result);

            result = cache.Get(key2);
            Assert.Null(result);
        }

        [Fact(Skip = SkipReason)]
        public void SetAlwaysOverwrites()
        {
            var cache = _cache;
            var value1 = new byte[1] { 1 };
            string key = "myKey";

            cache.Set(key, value1);
            var result = cache.Get(key);
            Assert.Equal(value1, result);

            var value2 = new byte[1] { 2 };
            cache.Set(key, value2);
            result = cache.Get(key);
            Assert.Equal(value2, result);
        }

        [Fact(Skip = SkipReason)]
        public void RemoveRemoves()
        {
            var cache = _cache;
            var value = new byte[1];
            string key = "myKey";

            cache.Set(key, value);
            var result = cache.Get(key);
            Assert.Equal(value, result);

            cache.Remove(key);
            result = cache.Get(key);
            Assert.Null(result);
        }

        [Fact(Skip = SkipReason)]
        public void SetNullValueThrows()
        {
            var cache = _cache;
            byte[] value = null;
            string key = "myKey";

            Assert.Throws<ArgumentNullException>(() => cache.Set(key, value));
        }

        [Fact(Skip = SkipReason)]
        public void SetGetEmptyNonNullBuffer()
        {
            var cache = _cache;
            var key = Me();
            cache.Remove(key); // known state
            Assert.Null(cache.Get(key)); // expect null

            cache.Set(key, Array.Empty<byte>());
            var arr = cache.Get(key);
            Assert.NotNull(arr);
            Assert.Empty(arr);
        }

        [Fact(Skip = SkipReason)]
        public async Task SetGetEmptyNonNullBufferAsync()
        {
            var cache = _cache;
            var key = Me();
            await cache.RemoveAsync(key); // known state
            Assert.Null(await cache.GetAsync(key)); // expect null

            await cache.SetAsync(key, Array.Empty<byte>());
            var arr = await cache.GetAsync(key);
            Assert.NotNull(arr);
            Assert.Empty(arr);
        }

        [Theory(Skip = SkipReason)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("abc")]
        public void SetGetNonNullString(string payload)
        {
            var cache = _cache;
            var key = Me();
            cache.Remove(key); // known state
            Assert.Null(cache.Get(key)); // expect null
            cache.SetString(key, payload);

            // check raw bytes
            var raw = cache.Get(key);
            Assert.Equal(Hex(payload), Hex(raw));

            // check via string API
            var value = cache.GetString(key);
            Assert.NotNull(value);
            Assert.Equal(payload, value);
        }

        [Theory(Skip = SkipReason)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("abc")]
        [InlineData("abc def ghi jkl mno pqr stu vwx yz!")]
        public async Task SetGetNonNullStringAsync(string payload)
        {
            var cache = _cache;
            var key = Me();
            await cache.RemoveAsync(key); // known state
            Assert.Null(await cache.GetAsync(key)); // expect null
            await cache.SetStringAsync(key, payload);

            // check raw bytes
            var raw = await cache.GetAsync(key);
            Assert.Equal(Hex(payload), Hex(raw));

            // check via string API
            var value = await cache.GetStringAsync(key);
            Assert.NotNull(value);
            Assert.Equal(payload, value);
        }

        static string Hex(byte[] value) => BitConverter.ToString(value);
        static string Hex(string value) => Hex(Encoding.UTF8.GetBytes(value));

        private static string Me([CallerMemberName] string caller = "") => caller;

        #endregion RedisCacheSetAndRemoveTests from upstream
    }
}