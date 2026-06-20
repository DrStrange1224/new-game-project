using System;
using System.IO;

public class Logger
{
    public class Config
    {
        private string filePath;
        private string fileName;
        private bool isRollback;
        private int rollbackCount;

        public string FilePath
        {
            get { }
            set { }
        }
    }

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
