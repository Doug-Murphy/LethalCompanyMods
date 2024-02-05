#pragma warning disable S1118
using HarmonyLib;

namespace GuysNight.LethalCompanyMod.BalancedItems.Patches {
	[HarmonyPatch(typeof(DepositItemsDesk))]
	public class DepositItemsDeskPatches {
		[HarmonyPatch("SellItemsOnServer")]
		[HarmonyPrefix]
		public static void MakeEquipmentOnDeskSellable(DepositItemsDesk __instance) {
			SharedComponents.ConfigFile.Reload();

			var isEquipmentSellFeatureEnabled = true;

			if (SharedComponents.ConfigFile.TryGetEntry<bool>(Constants.ConfigSectionHeaderToggles, Constants.ConfigKeyToggleSellableEquipment, out var featureToggleConfigEntry)) {
				isEquipmentSellFeatureEnabled = featureToggleConfigEntry.Value;
				SharedComponents.Logger.LogDebug($"Successfully retrieved sellable equipment feature toggle. Value is '{isEquipmentSellFeatureEnabled}'");
			}
			else {
				SharedComponents.Logger.LogWarning("Could not retrieve sellable equipment feature toggle from config. Assuming it was set to true.");
			}

			if (!isEquipmentSellFeatureEnabled) {
				return;
			}

			foreach (var itemOnCounter in __instance.itemsOnCounter) {
				if (!ItemsContainer.Items.TryGetValue(itemOnCounter.itemProperties.name, out var itemEntry)) {
					SharedComponents.Logger.LogWarning($"Could not retrieve any item entries for '{itemOnCounter.itemProperties.name}' Unable to make that equipment item sellable.");

					continue;
				}

				//check if it's already listed as scrap. If it is, then we don't need to do anything about it.
				if (itemOnCounter.itemProperties.isScrap) {
					continue;
				}

				SharedComponents.Logger.LogInfo($"Setting '{itemOnCounter.itemProperties.name}' to be sellable equipment for '{itemEntry.OverrideItemValues.AverageValue}' credits.");
				itemOnCounter.itemProperties.isScrap = true;
				itemOnCounter.scrapValue = itemEntry.OverrideItemValues.AverageValue;
			}
		}
	}
}