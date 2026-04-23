using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace StackExchangeRedisCache.Contrib.SourceGenerators;

internal static class MethodManifestLoader
{
    private static readonly Lazy<IReadOnlyDictionary<string, bool>> LazyGetOpByMethodName = new(Load);

    /// <summary>Method name -> <see cref="ManifestEntry.GetOp"/> (true = get/tweakable-get, false = set/tweakable-set).</summary>
    internal static IReadOnlyDictionary<string, bool> GetOpByMethodName => LazyGetOpByMethodName.Value;

    private static IReadOnlyDictionary<string, bool> Load()
    {
        Assembly asm = typeof(MethodManifestLoader).Assembly;
        string? resourceName = asm.GetManifestResourceNames()
            .FirstOrDefault(static n => n.EndsWith("methodManifest.json", StringComparison.Ordinal));
        if (resourceName is null)
        {
            return new Dictionary<string, bool>(StringComparer.Ordinal);
        }

        using Stream? stream = asm.GetManifestResourceStream(resourceName);
        if (stream is null)
        {
            return new Dictionary<string, bool>(StringComparer.Ordinal);
        }

        using var reader = new StreamReader(stream);
        string json = reader.ReadToEnd();
        ManifestEntry[]? entries = JsonSerializer.Deserialize<ManifestEntry[]>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });

        if (entries is null || entries.Length == 0)
        {
            return new Dictionary<string, bool>(StringComparer.Ordinal);
        }

        var dict = new Dictionary<string, bool>(StringComparer.Ordinal);
        foreach (ManifestEntry e in entries)
        {
            if (string.IsNullOrEmpty(e.Name))
            {
                continue;
            }

            dict[e.Name] = e.GetOp;
        }

        return dict;
    }

    private sealed class ManifestEntry
    {
        public string Name { get; set; } = string.Empty;
        public bool GetOp { get; set; }
    }
}
