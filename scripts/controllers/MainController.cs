using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Godot;
using Godot.Collections;

public partial class MainController : Node {
    private const string JSON_PROJECT_CONFIG_PATH = @".\project.json";
    private Logger logger;
    private Node? screensNode;
    private System.Collections.Generic.Dictionary<string, Node> _screensDict = [];

    [Export] public string currentScreenName = "loading";
    [Export] public Godot.Collections.Dictionary<string, PackedScene> screensDict = [];
    [Export] public PackedScene defaultLoadingScreen = new();

    public MainController() {
        ProjectConfig.LoadJson(JSON_PROJECT_CONFIG_PATH);
        logger = ProjectConfig.loggers["default"];
    }

    public override void _Ready() {
        base._Ready();

        screensNode = new Node() {
            Name = "screens"
        };
        this.AddChild(screensNode);

        if (screensDict.Count == 0) {
            _screensDict.Add("loading", defaultLoadingScreen.Instantiate());
            throw new Exception("No screens were loaded");
        }
        foreach (var i in screensDict) {
            _screensDict.Add(i.Key, i.Value.Instantiate());
            screensNode.AddChild(_screensDict[i.Key]);
        }
        if (!_screensDict.ContainsKey("loading")) {
            _screensDict.Add("loading", defaultLoadingScreen.Instantiate());
            screensNode.AddChild(_screensDict["loading"]);
        }
    }

    private void LoadScreen(string screenName) {
        logger.Log($"Trying to load screen {screenName}");

        if (!_screensDict.ContainsKey(screenName)) {
            throw new ArgumentException("Undefined scene");
        }
        if (currentScreenName == screenName) {
            logger.Log("Trying to load current screen again");
            return;
        }

        _screensDict[currentScreenName].SetProcess(false);
        currentScreenName = screenName;

        _screensDict["loading"].SetProcess(true);
        //LOAD NEW SCREEN RESOURCES
        _screensDict["loading"].SetProcess(false);

        _screensDict[currentScreenName].SetProcess(true);
    }
}
