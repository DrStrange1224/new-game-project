using System;
using System.IO;

public class Logger
{
    public class Config
    {
        private string _filePath;
        private string _fileName;
        private bool _isRollback;
        private int _rollbackCount;

        private Config(){}

        public static Config GetInstance(string fileName, string filePath, int rollbackCount)
        {
            var instance = new Config
            {
                FileName = fileName,
                FilePath = filePath,
                IsRollback = rollbackCount > 0,
                RollbackCount = rollbackCount
            };
            return instance;
        }

        public static Config GetInstance(string fileName, string filePath)
        {
            return GetInstance(fileName, filePath, 0);
        }

        public static Config GetInstance(string fileName)
        {
            return GetInstance(fileName, "./");
        }

        private string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                //TODO check if file name is valid (must contain extension, mustn't contain any wrong symbols)
                _fileName = value;
            }
        }

        public string FilePath
        {
            get
            {
                //TODO check for other ways to create string
                return _filePath + _fileName;
            }

            init
            {
                if (!File.Exists(value))
                {
                    File.Create(value);
                }
                _filePath = value;
            }
        }

        public bool IsRollback
        {
            get
            {
                return _isRollback;
            }
            init
            {
                _isRollback = value;
            }
        }

        public int RollbackCount
        {
            get
            {
                return _rollbackCount;
            }
            init
            {
                if (value >= 0)
                {
                    _rollbackCount = value;
                }
                else
                {
                    throw new ArgumentException("rollbackCount property");
                }
            }
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
        get
        {
            return _config;
        }
        set
        {
            //TODO checks
            _config = value;
        }
    }

    private void loadConfig()
    {
        
    }

    public void Log(string message)
    {
        
    }
}