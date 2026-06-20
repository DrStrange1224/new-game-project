using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

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

    }

    public string FilePath { get; }

    private Config(string filePath)
    {
        FilePath = filePath;
    }
}
