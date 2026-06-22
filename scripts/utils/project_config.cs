using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;

public struct ProjectConfig {

    public Dictionary<string, Logger> loggers;
    private const string DEFAULT_LOGGER_NAME = "default";

    private ProjectConfig(JsonObject? json) {
        if (json == null) {
            LoadDefaultLogger();
        }
        else {
            LoadLoggers(json);
        }
    }

    /// <summary>
    /// Loads default logger to <tt>ProjectConfig.loggers</tt> with <em>"logger"</em> key
    /// </summary>
    private void LoadDefaultLogger() {
        loggers = new Dictionary<string, Logger> {
            {"default", new Logger()}
        };
    }

    /// <summary>
    /// Loads loggers from json file. Requires <em>"loggers"</em> key in json, else does nothing. Also can override <em>"default"</em> logger.
    /// </summary>
    /// <param name="json">json object containing <em>"loggers"</em> key</param>
    private void LoadLoggers(JsonObject json) {
        if (!json.ContainsKey("loggers")) {
            LoadDefaultLogger();
            return;
        }

        var loggersObject = json["loggers"]!.AsObject();
        if (!loggersObject.ContainsKey("default")) {
            LoadDefaultLogger();
            loggers.EnsureCapacity(loggersObject.Count + 1);
        }
        else {
            loggers = new Dictionary<string, Logger>(loggersObject.Count);
        }

        foreach (var logger in loggersObject) {
            loggers.Add(logger.Key, new Logger(logger.Value!.AsObject()));
        }
    }

    public static ProjectConfig LoadJson(string path) {
        if (!File.Exists(path)) {
            throw new IOException($"json file not found at path: {path}");
        }
        using FileStream fs = File.OpenRead(path);
        return new(JsonSerializer.Deserialize<JsonObject>(fs));
    }
}