#if TOOLS
using Godot;
using Salaros.Configuration;

[Tool]
public partial class NewNodesPlugin : EditorPlugin {
    private readonly ConfigParser cp;
    NewNodesPlugin() {
        cp = new ConfigParser(@".\plugin.cfg");
    }

    public override void _EnablePlugin() {
        base._EnablePlugin();
        foreach (string plugin in cp.GetArrayValue("subplugins", "plugin_names")) {
            EditorInterface.Singleton.SetPluginEnabled(cp.GetValue("plugin", "name") + plugin, true);
        }
    }

    public override void _DisablePlugin() {
        base._DisablePlugin();
        foreach (string plugin in cp.GetArrayValue("subplugins", "plugin_names")) {
            EditorInterface.Singleton.SetPluginEnabled(cp.GetValue("plugin", "name") + plugin, false);
        }
    }
}
#endif
