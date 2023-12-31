using System.Reflection;
using BepInEx;
using GuysNight.LethalCompanyMod.BalancedItems.Extensions;
using HarmonyLib;

namespace GuysNight.LethalCompanyMod.BalancedItems {
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	public class Plugin : BaseUnityPlugin {
		private void Awake() {
			SharedComponents.Logger = Logger;
			Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
			SharedComponents.Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

			SharedComponents.Logger.LogInfo(ItemOverridesContainer.ItemOverrides.ToDebugString());
		}
	}
}