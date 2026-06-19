using System;
using System.IO;

public class Logger
{
    public struct Config
    {
        private string _filePath;
        private string _fileName;
        private bool _isRollback;
        private int _rollbackCount;

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
