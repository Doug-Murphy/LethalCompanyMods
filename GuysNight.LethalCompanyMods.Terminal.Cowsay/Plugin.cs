using BepInEx;
using HarmonyLib;
using System.Reflection;

namespace GuysNight.LethalCompanyMods.Terminal.Cowsay {
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	public class Plugin : BaseUnityPlugin {
		private void Awake() {
			Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
		}
	}
}