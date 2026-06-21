using System;
using System.Diagnostics.CodeAnalysis;
using Godot;

public partial class main_controller : Node {
    private const string JSON_PROJECT_CONFIG_PATH = @".\project.json";
    private Logger logger;
    private ProjectConfig projectConfig;
    private void HandleException(Exception e) {
        GD.Print(e.Message);
        GetTree().Quit(1);
    }

    private void setupProjectConfig() {
        try {
            projectConfig = ProjectConfig.LoadJson(JSON_PROJECT_CONFIG_PATH);
            logger = projectConfig.loggers["default"];
            logger.Log("Ready function run");
        }
        catch (Exception e) {
            HandleException(e);
        }
        GD.Print("Project config loaded. Now all logs will show at setted logs");
    }

    public override void _EnterTree() {
        base._EnterTree();
        setupProjectConfig();
    }
}
