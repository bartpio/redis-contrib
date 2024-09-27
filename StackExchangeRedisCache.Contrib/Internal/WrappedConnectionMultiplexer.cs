using System.Net;
using StackExchange.Redis;
using StackExchange.Redis.Maintenance;
using StackExchange.Redis.Profiling;

namespace StackExchangeRedisCache.Contrib.Internal;

internal sealed class WrappedConnectionMultiplexer(IConnectionMultiplexer multiplexer, ICommandFlagsTweaker tweaker) : IConnectionMultiplexer
{
    public IDatabase GetDatabase(int db = -1, object? asyncState = null)
    {
        IDatabase rawdb = multiplexer.GetDatabase(db, asyncState);
        return new WrappedDatabase(rawdb, tweaker);
    }

    #region Straight pass-through

#pragma warning disable CS0618 // Type or member is obsolete

    public string ClientName => multiplexer.ClientName;

    public string Configuration => multiplexer.Configuration;

    public int TimeoutMilliseconds => multiplexer.TimeoutMilliseconds;

    public long OperationCount => multiplexer.OperationCount;
    public bool PreserveAsyncOrder { get => multiplexer.PreserveAsyncOrder; set => multiplexer.PreserveAsyncOrder = value; }

    public bool IsConnected => multiplexer.IsConnected;

    public bool IsConnecting => multiplexer.IsConnecting;

    public bool IncludeDetailInExceptions { get => multiplexer.IncludeDetailInExceptions; set => multiplexer.IncludeDetailInExceptions = value; }
    public int StormLogThreshold { get => multiplexer.StormLogThreshold; set => multiplexer.StormLogThreshold = value; }

    public event EventHandler<RedisErrorEventArgs> ErrorMessage
    {
        add
        {
            multiplexer.ErrorMessage += value;
        }

        remove
        {
            multiplexer.ErrorMessage -= value;
        }
    }

    public event EventHandler<ConnectionFailedEventArgs> ConnectionFailed
    {
        add
        {
            multiplexer.ConnectionFailed += value;
        }

        remove
        {
            multiplexer.ConnectionFailed -= value;
        }
    }

    public event EventHandler<InternalErrorEventArgs> InternalError
    {
        add
        {
            multiplexer.InternalError += value;
        }

        remove
        {
            multiplexer.InternalError -= value;
        }
    }

    public event EventHandler<ConnectionFailedEventArgs> ConnectionRestored
    {
        add
        {
            multiplexer.ConnectionRestored += value;
        }

        remove
        {
            multiplexer.ConnectionRestored -= value;
        }
    }

    public event EventHandler<EndPointEventArgs> ConfigurationChanged
    {
        add
        {
            multiplexer.ConfigurationChanged += value;
        }

        remove
        {
            multiplexer.ConfigurationChanged -= value;
        }
    }

    public event EventHandler<EndPointEventArgs> ConfigurationChangedBroadcast
    {
        add
        {
            multiplexer.ConfigurationChangedBroadcast += value;
        }

        remove
        {
            multiplexer.ConfigurationChangedBroadcast -= value;
        }
    }

    public event EventHandler<ServerMaintenanceEvent> ServerMaintenanceEvent
    {
        add
        {
            multiplexer.ServerMaintenanceEvent += value;
        }

        remove
        {
            multiplexer.ServerMaintenanceEvent -= value;
        }
    }

    public event EventHandler<HashSlotMovedEventArgs> HashSlotMoved
    {
        add
        {
            multiplexer.HashSlotMoved += value;
        }

        remove
        {
            multiplexer.HashSlotMoved -= value;
        }
    }

    public void AddLibraryNameSuffix(string suffix)
    {
        multiplexer.AddLibraryNameSuffix(suffix);
    }

    public void Close(bool allowCommandsToComplete = true)
    {
        multiplexer.Close(allowCommandsToComplete);
    }

    public Task CloseAsync(bool allowCommandsToComplete = true)
    {
        return multiplexer.CloseAsync(allowCommandsToComplete);
    }

    public bool Configure(TextWriter? log = null)
    {
        return multiplexer.Configure(log);
    }

    public Task<bool> ConfigureAsync(TextWriter? log = null)
    {
        return multiplexer.ConfigureAsync(log);
    }

    public void Dispose()
    {
        multiplexer.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return multiplexer.DisposeAsync();
    }

    public void ExportConfiguration(Stream destination, ExportOptions options = (ExportOptions)(-1))
    {
        multiplexer.ExportConfiguration(destination, options);
    }

    public ServerCounters GetCounters()
    {
        return multiplexer.GetCounters();
    }

    public EndPoint[] GetEndPoints(bool configuredOnly = false)
    {
        return multiplexer.GetEndPoints(configuredOnly);
    }

    public int GetHashSlot(RedisKey key)
    {
        return multiplexer.GetHashSlot(key);
    }

    public IServer GetServer(string host, int port, object? asyncState = null)
    {
        return multiplexer.GetServer(host, port, asyncState);
    }

    public IServer GetServer(string hostAndPort, object? asyncState = null)
    {
        return multiplexer.GetServer(hostAndPort, asyncState);
    }

    public IServer GetServer(IPAddress host, int port)
    {
        return multiplexer.GetServer(host, port);
    }

    public IServer GetServer(EndPoint endpoint, object? asyncState = null)
    {
        return multiplexer.GetServer(endpoint, asyncState);
    }

    public IServer[] GetServers()
    {
        return multiplexer.GetServers();
    }

    public string GetStatus()
    {
        return multiplexer.GetStatus();
    }

    public void GetStatus(TextWriter log)
    {
        multiplexer.GetStatus(log);
    }

    public string? GetStormLog()
    {
        return multiplexer.GetStormLog();
    }

    public ISubscriber GetSubscriber(object? asyncState = null)
    {
        return multiplexer.GetSubscriber(asyncState);
    }

    public int HashSlot(RedisKey key)
    {
        return multiplexer.HashSlot(key);
    }

    public long PublishReconfigure(CommandFlags flags = CommandFlags.None)
    {
        return multiplexer.PublishReconfigure(flags);
    }

    public Task<long> PublishReconfigureAsync(CommandFlags flags = CommandFlags.None)
    {
        return multiplexer.PublishReconfigureAsync(flags);
    }

    public void RegisterProfiler(Func<ProfilingSession?> profilingSessionProvider)
    {
        multiplexer.RegisterProfiler(profilingSessionProvider);
    }

    public void ResetStormLog()
    {
        multiplexer.ResetStormLog();
    }

    public void Wait(Task task)
    {
        multiplexer.Wait(task);
    }

    public T Wait<T>(Task<T> task)
    {
        return multiplexer.Wait(task);
    }

    public void WaitAll(params Task[] tasks)
    {
        multiplexer.WaitAll(tasks);
    }

    string IConnectionMultiplexer.ToString()
    {
        return multiplexer.ToString();
    }

#pragma warning restore CS0618 // Type or member is obsolete

    #endregion Straight pass-through
}
