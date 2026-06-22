using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Godot;

public partial class MainController : Node {
    private const string JSON_PROJECT_CONFIG_PATH = @".\project.json";
    private Logger logger;
    private ProjectConfig projectConfig;
    private Node screensNode;
    private Dictionary<string, Node> screensDict;
    public string currentScreenName;

    public MainController() {
        setupProjectConfig();
    }

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

    private void LoadScreen(string screenName) {
        logger.Log($"Trying to load screen {screenName}");
        if (!screensDict.ContainsKey(screenName)) {
            throw new ArgumentException("Undefined scene");
        }
        if (currentScreenName == screenName) {
            logger.Log("Trying to load current screen again");
        }
    }

    private void InitScreens() {
        screensNode = FindChild("screens");
        currentScreenName = "menu";
        screensNode.GetChildren();
    }

    public override void _Ready() {
        base._Ready();
        try {
            InitScreens();
            LoadScreen("menu");
        }
        catch (Exception e) {
            HandleException(e);
        }
    }

}
