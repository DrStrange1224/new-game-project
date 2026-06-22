using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;

public static class ProjectConfig {

    public static Dictionary<string, Logger> loggers = [];
    private const string DEFAULT_LOGGER_NAME = "default";

    /// <summary>
    /// Loads default logger to <tt>ProjectConfig.loggers</tt> with <em>"logger"</em> key
    /// </summary>
    private static void LoadDefaultLogger() {
        loggers = new Dictionary<string, Logger> {
            {"default", new Logger()}
        };
    }

    /// <summary>
    /// Loads loggers from json file. Requires <em>"loggers"</em> key in json, else does nothing. Also can override <em>"default"</em> logger.
    /// </summary>
    /// <param name="json">json object containing <em>"loggers"</em> key</param>
    private static void LoadLoggers(JsonObject json) {
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

    private static void LoadDefaultSettings() {
        LoadDefaultLogger();
    }

    private static void LoadSettings(JsonObject json) {
        if (json["loggers"] != null) LoadLoggers(json["loggers"]!.AsObject());
        else LoadDefaultLogger();
    }

    public static void LoadJson(string path) {
        if (!File.Exists(path)) {
            throw new IOException($"json file not found at path: {path}");
        }

        using FileStream fs = File.OpenRead(path);
        JsonObject? json = JsonSerializer.Deserialize<JsonObject>(fs);

        if (json == null) LoadDefaultSettings();
        else LoadSettings(json);
    }
}