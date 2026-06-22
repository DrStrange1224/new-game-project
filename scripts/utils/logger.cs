using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Text.Json.Nodes;

public class Logger {
    public readonly struct Config {

        private readonly string _filePath = ".\\logs";
        private readonly string _fileName = ".log";

        public Config() {
            FullPath = $"{FilePath}/{FileName}";
        }

        [NotNull]
        public readonly string? FilePath {
            get => _filePath;
            init => _filePath = (string.IsNullOrWhiteSpace(value) ? null : value) ?? _filePath;
        }

        [NotNull]
        public readonly string? FileName {
            get => _fileName;
            init => _fileName = (string.IsNullOrWhiteSpace(value) ? null : value) ?? _fileName;
        }

        public readonly string FullPath {
            get;
            init;
        }
    }

    public readonly Config config;
    private FileStream fileWriter;

    public Logger() {
        config = new();

        if (!Directory.Exists(config.FilePath)) {
            Directory.CreateDirectory(config.FilePath!);
        }
        if (!File.Exists(config.FullPath)) {
            File.Create(config.FullPath);
        }
        fileWriter = File.OpenWrite(config.FullPath);
    }

    public Logger(JsonObject? settings) {
        if (settings == null) {
            config = new();
        }
        else {
            config = new() {
                FilePath = settings.ContainsKey("path") ? settings["path"]!.ToString() : null,
                FileName = settings.ContainsKey("fileName") ? settings["fileName"]!.ToString() : null
            };
        }

        if (!Directory.Exists(config.FilePath)) {
            Directory.CreateDirectory(config.FilePath!);
        }
        if (!File.Exists(config.FullPath)) {
            File.Create(config.FullPath);
        }
        fileWriter = File.OpenWrite(config.FullPath);
    }

    ~Logger() {
        fileWriter.Close();
    }

    public void Log(string message) {
        fileWriter.Write(Encoding.UTF8.GetBytes(message));
        fileWriter.Flush();
    }
}
