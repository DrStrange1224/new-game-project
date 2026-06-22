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
        projectConfig = ProjectConfig.LoadJson(JSON_PROJECT_CONFIG_PATH);

        logger = projectConfig.loggers["default"];
    }

    private void HandleException(Exception e) {
        GD.Print(e.Message);
        GetTree().Quit(1);
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
        InitScreens();
        LoadScreen("menu");
    }

}
