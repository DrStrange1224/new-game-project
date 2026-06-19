using System;
using System.IO;

public class Logger {
    public struct Config {
        public string filePath = "";
        public string fileName = "";
        public bool isRollback = false;
        public int rollbackCount = 0;

        public Config(string filePath, string fileName, int rollbackCount) {
            this.filePath = filePath;
            this.fileName = fileName;
            if (rollbackCount <= 0) {
                rollbackCount = 0;
                isRollback = false;
            }
            else {
                isRollback = true;
            }
        }
    }

    private Config _config;

    public Logger(Config config) {
        ThisConfig = config;
        loadConfig();
    }

    public Config ThisConfig {
        get { return _config; }
        set {
            //TODO checks
            _config = value;
        }
    }

    private void loadConfig() { }

    public void Log(string message) { }
}
