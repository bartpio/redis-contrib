# Developer Setup

This document covers what you need to build the library and run the full test suite locally.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8) (8.0.400 or later)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (includes Docker Compose)

Docker is only required for integration tests that connect to a live Redis cluster. Pure unit tests run without it.

## Test infrastructure

The integration tests in `RedisCacheWithCommandTweakerTests` expect:

| Role | Default address |
|------|----------------|
| Primary (read/write) | `localhost:6379` |
| Replica (read-only) | `localhost:6380` |

A `compose.yml` file in the repository root starts both instances with replication already configured.

### Start

```bash
docker compose up -d
```

Docker will pull the `redis:latest` image on first run. The replica container waits for the primary to pass its health check before starting, so replication is active by the time the command returns.

You can verify replication is working:

```bash
docker exec redis-contrib-redis-primary-1 redis-cli info replication
# role:master
# connected_slaves:1
```

### Stop and clean up

```bash
docker compose down
```

## Building

```bash
dotnet build
```

## Running the tests

```bash
dotnet test
```

Expected outcome with Docker running:

```
Passed: 21  Skipped: 10  Failed: 0
```

The 10 skipped tests are legacy upstream tests (`RedisCacheSetAndRemoveTests`) that carry a hard-coded skip reason unrelated to this project.

### Overriding the Redis endpoints

If you want to point the tests at a different Redis instance, set environment variables before running:

```bash
export StackExchangeRedisCache_Contrib_Tests_PRIMARY=myredis.example.com:6379
export StackExchangeRedisCache_Contrib_Tests_REPLICA=myredis-replica.example.com:6379
dotnet test
```

To run without a replica (primary only):

```bash
export StackExchangeRedisCache_Contrib_Tests_REPLICA=NONE
dotnet test
```

Tests that exercise `DemandReplica` behaviour will still pass when a replica is present; they are skipped-equivalent when the replica endpoint is unavailable because the connection will fail gracefully via `AbortOnConnectFail = false`.

## Test organisation

| File | Redis required | Purpose |
|------|---------------|---------|
| `CommandFlagsExtensionsTests.cs` | No | Unit tests for `CommandFlags` extension methods |
| `NullCommandFlagsTweakerTests.cs` | No | Unit tests for `NullCommandFlagsTweaker` pass-through contract and singleton guarantee |
| `ServiceCollectionExtensionsTests.cs` | No | DI registration tests, including double-wrap prevention |
| `RedisCacheWithCommandTweakerTests.cs` | Yes | Integration tests verifying which tweaker method is called (and how many times) for each `IDistributedCache` operation |
