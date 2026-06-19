using System;
using System.IO;
using System.Text;
using Godot;

public class Logger {
    public readonly struct Config {
        public readonly string filePath;
        public readonly string fileName;

        public Config(string filePath, string fileName) {
            this.filePath = filePath;
            this.fileName = fileName;
        }
    }

    public readonly Config config;
    private DirectoryInfo _logsDirectory;
    private StreamWriter _streamWriter;
    private Encoding encoding = new UTF8Encoding(true);

    public Logger(Config config) {
        this.config = config;
        _logsDirectory = Directory.CreateDirectory(this.config.filePath);
        GD.Print($"{_logsDirectory.FullName}\\{config.fileName}");
        _streamWriter = new StreamWriter($"{_logsDirectory.FullName}\\{config.fileName}");
    }

    public void Log(string message) {
        _streamWriter.WriteLine(message);
        _streamWriter.Flush();
    }

    ~Logger() {
        _streamWriter.Close();
    }
}
