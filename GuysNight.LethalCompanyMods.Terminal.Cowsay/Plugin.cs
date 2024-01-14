using BepInEx;
using HarmonyLib;
using System.Reflection;
using TerminalApi.Classes;
using static TerminalApi.TerminalApi;

namespace GuysNight.LethalCompanyMods.Terminal.Cowsay {
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	[BepInDependency("atomic.terminalapi", "1.5.0")]
	public class Plugin : BaseUnityPlugin {
		private void Awake() {
			Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
			// Adds time command, 'check' is the verb. Verbs are optional
			AddCommand("cowsay-chuck", new CommandInfo {
					Category = "other",
					Description = "Displays the current time.",
					DisplayTextSupplier = OnTimeCommand
				},
				"check");
			AddCommand("cowsay", new CommandInfo {
					Category = "other",
					Description = "Displays the current time.",
					DisplayTextSupplier = OnTimeCommand
				},
				"check");

			//cowsay -n hello, world. I am a cow!
			//cowsay hello, world. I am a cow!
		}
	}
}