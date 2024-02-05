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
			SharedComponents.ConfigFile = new ConfigFile(Path.Combine(Paths.ConfigPath, $"{PluginInfo.PLUGIN_NAME}.cfg"), true) { SaveOnConfigSet = false };

			SharedComponents.ConfigFile.Bind(Constants.ConfigSectionHeaderToggles,
				Constants.ConfigKeyToggleAverageSellValues,
				true,
				"Whether or not your specified average sell value overrides should be applied. If set to false, vanilla values will be used. Changes to this value are reflected only when new items are spawned."
			);

			SharedComponents.ConfigFile.Bind(Constants.ConfigSectionHeaderToggles,
				Constants.ConfigKeyToggleSellableEquipment,
				true,
				"Whether or not you want your equipment to sell for the average value of the item. If set to false, equipment will sell for 0 credits. Changes to this value are reflected every time you sell items to the company."
			);

			SharedComponents.ConfigFile.Bind(Constants.ConfigSectionHeaderToggles,
				Constants.ConfigKeyToggleMoonRarity,
				true,
				"Whether or not your specified moon rarity overrides should be applied. If set to false, vanilla values will be used. Changes to this value are reflected at the start of each day."
			);

			SharedComponents.ConfigFile.Bind(Constants.ConfigSectionHeaderToggles,
				Constants.ConfigKeyToggleTerminalPurchasePrice,
				true,
				"Whether or not your specified terminal item price overrides should be applied. If set to false, vanilla values will be used. Changes to this value are reflected every time you begin using the terminal."
			);

			SharedComponents.ConfigFile.Bind(Constants.ConfigSectionHeaderToggles,
				Constants.ConfigKeyToggleWeights,
				true,
				"Whether or not your specified weight overrides should be applied. If set to false, vanilla values will be used. Changes to this value are reflected only when new items are spawned, or when restarting your session."
			);

			SharedComponents.ConfigFile.Save();
		}
	}
}