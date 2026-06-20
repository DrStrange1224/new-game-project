using System;
using System.IO;

namespace utils;

public class Logger
{
    private Config _config;

    public Logger(Config config)
    {
        ThisConfig = config;
        loadConfig();
    }

    public Config ThisConfig
    {
        get { return _config; }
        set
        {
            //TODO checks
            _config = value;
        }
    }

    private void loadConfig() { }

    public void Log(string message) { }
}
