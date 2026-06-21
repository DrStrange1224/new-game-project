using System.IO;
using Godot;
using Godot.Collections;

public class Logger {
    public readonly record struct Config(string FilePath, string FileName);
    public readonly Config config;
    private readonly StreamWriter _streamWriter;

    public Logger(Dictionary configDict) {
        GD.Print(configDict.Keys);
        config = new Config(
            configDict["path"].ToString(),
            $"{configDict["fileName"]}.{configDict["fileExtension"]}"
        );
        _streamWriter = new StreamWriter($"{config.FilePath}\\{config.FileName}");
    }

    ~Logger() {
        _streamWriter.Close();
    }

    public void Log(string message) {
        _streamWriter.WriteLine(message);
        _streamWriter.Flush();
    }
}
