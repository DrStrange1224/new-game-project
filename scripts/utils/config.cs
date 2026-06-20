using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace utils;

public class Config
{
    private string filePath;
    private string fileName;
    private bool isRollback;
    private int rollbackCount;
    private static Config instance;

    static Config()
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var configPath = @$"{baseDirectory}..\..\appsettings.json";
        var configContent = File.ReadAllText(configPath);
        var forecastNode = JsonNode.Parse(configContent);

        instance = new Config
        (
            filePath: forecastNode["LOG_PATH"].ToString(),
            logName: forecastNode["LOG_NAME"].ToString(),

        );
    }

    public string FilePath { get; }
    public string LogName { get; }
    public bool DebugMode { get; }

    private Config(string filePath, string logName, bool debugMode)
    {
        FilePath = filePath;
        LogName = logName;
        DebugMode = debugMode;
    }
}
