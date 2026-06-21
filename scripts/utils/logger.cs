using System.IO;
using Godot;
using Godot.Collections;

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
    private StreamWriter _streamWriter;

    public Logger(Dictionary configDict) {
        GD.Print(configDict.Keys);
        config = new Config(
            configDict["path"].ToString(),
            $"{configDict["fileName"]}.{configDict["fileExtension"]}"
        );
        _streamWriter = new StreamWriter($"{config.filePath}\\{config.fileName}");
    }

    ~Logger() {
        _streamWriter.Close();
    }

    public void Log(string message) {
        _streamWriter.WriteLine(message);
        _streamWriter.Flush();
    }
}
