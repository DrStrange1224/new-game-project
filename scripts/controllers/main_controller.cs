using Godot;

public partial class main_controller : Node {
    private const string LOG_PATH = "D:/godot_projects/new-game-project/logs"; //TODO clear hardcode
    private const string LOG_NAME = "log.log";
    private Logger logger;

    public override void _Ready() {
        setupLogger();
        logger.Log("Init log");
        GD.Print("Hello");
    }

    public void setupLogger() {
        Logger.Config config = new(
            LOG_PATH,
            LOG_NAME
        );
        logger = new Logger(config);
    }
}
