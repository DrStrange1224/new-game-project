using System.Diagnostics.CodeAnalysis;
using Godot;
using Godot.Collections;

public partial class main_controller : Node {
    private const string JSON_PROJECT_CONFIG_PATH = ".\\project.json";
    private Logger logger;
    private Dictionary projectConfig;

    private void setupProjectConfig() {
        projectConfig = (Dictionary)GD.Load<Json>(JSON_PROJECT_CONFIG_PATH).Data;
        logger = new Logger((Dictionary)((Dictionary)projectConfig["loggers"])["default"]);
    }

    public override void _EnterTree() {
        base._EnterTree();
        setupProjectConfig();
    }

    public override void _Ready() {
        logger.Log("Init loggg");
        GD.Print("Hello");
    }
}
