#if TOOLS
using Godot;

[Tool]
public partial class ScreenNodePlugin : EditorPlugin {
    public override void _EnablePlugin() {
        base._EnablePlugin();
        AddCustomType("ScreenNode", "Node", GD.Load<Script>(@".\ScreenNode.cs"), null);
    }

    public override void _DisablePlugin() {
        base._DisablePlugin();
        RemoveCustomType("ScreenNode");
    }
}
#endif