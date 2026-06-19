using Godot;

public partial class main_controller : Node
{
    private const string LOG_PATH = "D:/godot_projects/new-game-project/logs"; //TODO clear hardcode
    private const string LOG_NAME = "log.txt";
    private const bool DEBUG_MODE = true;
    private Logger logger;

    public override void _Ready()
    {
        GD.Print("Hello");
    }

    public void setupLogger()
    {
        logger = new Logger(Logger.Config.GetInstance(LOG_NAME, LOG_PATH));
    }
}
