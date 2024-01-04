using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System.IO;
using System.Reflection;

namespace GuysNight.LethalCompanyMod.BalancedItems {
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	public class Plugin : BaseUnityPlugin {
		private void Awake() {
			SharedComponents.Logger = Logger;
			Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
			SharedComponents.Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
			SharedComponents.ConfigFile = new ConfigFile(Path.Combine(Paths.ConfigPath, $"{PluginInfo.PLUGIN_NAME}.cfg"), true);
		}
	}
}