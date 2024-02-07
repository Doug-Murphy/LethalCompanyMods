#pragma warning disable S1118
using GuysNight.LethalCompanyMod.BalancedItems.Containers;
using GuysNight.LethalCompanyMod.BalancedItems.Models.Terminal;
using GuysNight.LethalCompanyMod.BalancedItems.Utilities;
using HarmonyLib;

namespace GuysNight.LethalCompanyMod.BalancedItems.Patches {
	[HarmonyPatch(typeof(Terminal))]
	public class TerminalPatches {
		[HarmonyPatch("Start")]
		[HarmonyPrefix]
		public static void InitializeTerminalItems(Terminal __instance) {
			foreach (var item in __instance.buyableItemsList) {
				TerminalItemsContainer.SetVanillaValuesForTerminalItem(item.name, new VanillaTerminalItemValues(item.creditsWorth));
			}
		}

		[HarmonyPatch("BeginUsingTerminal")]
		[HarmonyPrefix]
		public static void SetPricesInTerminal(Terminal __instance) {
			SharedComponents.ConfigFile.Reload();

			var isTerminalPriceOverrideFeatureEnabled = true;

			if (SharedComponents.ConfigFile.TryGetEntry<bool>(Constants.ConfigSectionHeaderToggles, Constants.ConfigKeyToggleTerminalPurchasePrice, out var featureToggleConfigEntry)) {
				isTerminalPriceOverrideFeatureEnabled = featureToggleConfigEntry.Value;
				SharedComponents.Logger.LogDebug($"Successfully retrieved terminal purchase price override feature toggle. Value is '{isTerminalPriceOverrideFeatureEnabled}'");
			}
			else {
				SharedComponents.Logger.LogWarning("Could not retrieve terminal purchase price override feature toggle from config. Assuming it was set to true.");
			}

			foreach (var item in __instance.buyableItemsList) {
				TerminalItemsContainer.TerminalItems[item.name] = ConfigUtilities.SyncConfigForTerminalItemOverrides(item);
				var overridePurchasePrice = TerminalItemsContainer.TerminalItems[item.name].OverrideTerminalItemValues.PurchasePrice;
				var vanillaPurchasePrice = TerminalItemsContainer.TerminalItems[item.name].VanillaTerminalItemValues.PurchasePrice;
				SharedComponents.Logger.LogDebug($"For terminal item '{item.name}' the override sell price is '{overridePurchasePrice}' and the vanilla sell price is '{vanillaPurchasePrice}'");
				if (isTerminalPriceOverrideFeatureEnabled) {
					UpdateItemPriceInShop(item, overridePurchasePrice);
				}
				else {
					UpdateItemPriceInShop(item, vanillaPurchasePrice);
				}
			}

			SharedComponents.ConfigFile.Save();
		}

		private static void UpdateItemPriceInShop(Item item, int price) {
			item.creditsWorth = price;
			SharedComponents.Logger.LogInfo($"Successfully set purchase price for '{item.itemName}' to be '{price}'");
		}
	}
}