using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using GuysNight.LethalCompanyMod.BalancedItems.Extensions;
using GuysNight.LethalCompanyMod.BalancedItems.Utilities;
using HarmonyLib;

namespace GuysNight.LethalCompanyMod.BalancedItems {
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	public class Plugin : BaseUnityPlugin {
		private void Awake() {
			SharedComponents.Logger = Logger;
			Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
			SharedComponents.Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

			LoadConfigValues();

			SharedComponents.Logger.LogInfo(ItemOverridesContainer.ItemOverrides.ToDebugString());
		}

		private void LoadConfigValues() {
			var configFile = new ConfigFile(Path.Combine(Paths.ConfigPath, $"{PluginInfo.PLUGIN_NAME}.cfg"), true);
			//TODO: How to handle items with no override

			//weights
			foreach (var (itemName, itemOverrides) in ItemOverridesContainer.ItemOverrides) {
				//TODO: Remove this skip logic
				if (!itemOverrides.Weight.HasValue) {
					continue;
				}
				
				//TODO: make this dynamic to where it fetches the displayed item name based on the internal item name
				itemOverrides.Weight = configFile.Bind("Weights",
					itemName,
					NumericUtilities.DenormalizeWeight(itemOverrides.Weight ?? -1f),
					$"The weight for the {itemName} item.").Value;
			}

			//sell values
		}
	}
}