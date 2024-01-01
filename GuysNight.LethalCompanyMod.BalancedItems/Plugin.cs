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
			SharedComponents.ConfigFile = new ConfigFile(Path.Combine(Paths.ConfigPath, $"{PluginInfo.PLUGIN_NAME}.cfg"), true);

			//weights
			foreach (var (itemName, itemOverrides) in ItemOverridesContainer.ItemOverrides) {
				//TODO: make this dynamic to where it fetches the displayed item name based on the internal item name
				itemOverrides.Weight = SharedComponents.ConfigFile.Bind(Constants.ConfigSectionHeaderWeight,
					itemName,
					NumericUtilities.DenormalizeWeight(itemOverrides.Weight),
					string.Format(Constants.ConfigDescriptionWeight, itemName)).Value;

				itemOverrides.AverageValue = SharedComponents.ConfigFile.Bind(Constants.ConfigSectionHeaderAverageSellValues,
					itemName,
					itemOverrides.AverageValue,
					string.Format(Constants.ConfigDescriptionAverageSellValues, itemName)).Value;
			}
		}
	}
}