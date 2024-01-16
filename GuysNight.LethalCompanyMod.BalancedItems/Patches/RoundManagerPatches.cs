#pragma warning disable	S1118

using GuysNight.LethalCompanyMod.BalancedItems.Utilities;
using HarmonyLib;
using System.Linq;

namespace GuysNight.LethalCompanyMod.BalancedItems.Patches {
	[HarmonyPatch(typeof(RoundManager))]
	public class RoundManagerPatches {
		[HarmonyPrefix]
		[HarmonyPatch("SpawnScrapInLevel")]
		public static void ChangeScrapValues(RoundManager __instance) {
			if (__instance is null) {
				SharedComponents.Logger.LogWarning($"__instance is null in '{nameof(ChangeScrapValues)}'. Aborting.");

				return;
			}

			SharedComponents.ConfigFile.Reload();
			foreach (var spawnableScrap in __instance.currentLevel.spawnableScrap.Select(x => x.spawnableItem)) {
				SharedComponents.Logger.LogDebug($"spawnableScrap.name is '{spawnableScrap.name}'");
				SharedComponents.Logger.LogDebug($"spawnableScrap.itemName is '{spawnableScrap.itemName}'");
				SharedComponents.Logger.LogDebug($"spawnableScrap.minValue is '{spawnableScrap.minValue}'");
				SharedComponents.Logger.LogDebug($"spawnableScrap.maxValue is '{spawnableScrap.maxValue}'");

				if (!ItemsContainer.Items.TryGetValue(spawnableScrap.name, out var itemEntry)) {
					//should be impossible so long we initialize the collection in StartOfRoundPatches
					SharedComponents.Logger.LogWarning($"No item entry exists for item '{spawnableScrap.name}'. Making no changes to item value.");

					continue;
				}

				if (bool.TryParse(SharedComponents.ConfigFile[Constants.ConfigSectionHeaderToggles, Constants.ConfigKeyToggleAverageSellValues].GetSerializedValue(), out var isSellValueFeatureEnabled)) {
					SharedComponents.Logger.LogDebug($"Successfully retrieved sell value override feature toggle. Value is '{isSellValueFeatureEnabled}'");
				}
				else {
					SharedComponents.Logger.LogWarning("Could not retrieve sell value override feature toggle from config. Assuming it was set to true.");
					isSellValueFeatureEnabled = true;
				}

				if (isSellValueFeatureEnabled) {
					//retrieve latest value from config
					itemEntry = ConfigUtilities.SyncConfigForItemOverrides(spawnableScrap);
					UpdateItemValue(spawnableScrap, itemEntry.Overrides.MinValue, itemEntry.Overrides.MaxValue);
				}
				else {
					UpdateItemValue(spawnableScrap, itemEntry.VanillaValues.MinValue, itemEntry.VanillaValues.MaxValue);
				}
			}

			SharedComponents.ConfigFile.Save();
		}

		private static void UpdateItemValue(Item item, int minValue, int maxValue) {
			item.minValue = minValue;
			item.maxValue = maxValue;
			SharedComponents.Logger.LogInfo($"Successfully set sell value range for '{item.name}' to be '{minValue}' - '{maxValue}'");
		}
	}
}