using System.Reflection;
using BepInEx;
using HarmonyLib;

namespace GuysNight.LethalCompanyMod.ReasonableWeights {
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	public class Plugin : BaseUnityPlugin {
		private void Awake() {
			SharedComponents.Logger = Logger;
			Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
			SharedComponents.Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

			SharedComponents.Logger.LogInfo("Item overrides specified:");
			foreach (var itemOverrideLog in ItemOverridesContainer.ItemOverrides)
			{
				SharedComponents.Logger.LogInfo($"{itemOverrideLog}");
			}
		}
	}
}