using HarmonyLib;

namespace GuysNight.LethalCompanyMod.BalancedItems.Patches {
	[HarmonyPatch(typeof(Terminal))]
	public class TerminalPatches {
		[HarmonyPatch("BeginUsingTerminal")]
		[HarmonyPrefix]
		public static void SetPricesInTerminal(Terminal __instance) {
			SharedComponents.ConfigFile.Reload();

			SetItemPricesInShop(__instance);
			// SetShipUpgradePricesInShop();
		}

		// [HarmonyPatch("LoadNewNodeIfAffordable")]
		// [HarmonyPrefix]
		// public static void LoadNewNodePrefix(TerminalNode node) {
		// 	switch (node.name) {
		// 		case "LoudHornBuy1":
		// 		case "LoudHornBuyConfirm":
		// 			node.itemCost = 5;
		//
		// 			break;
		// 	}
		//
		// 	SharedComponents.Logger.LogDebug($"node.name = '{node.name}' node.itemCost = '{node.itemCost}' node.displayText = '{node.displayText}");
		// }

		private static void SetItemPricesInShop(Terminal __instance) {
			foreach (var buyableItem in __instance.buyableItemsList) {
				SharedComponents.Logger.LogDebug($"Found buyable item '{buyableItem.itemName}' with creditsWorth '{buyableItem.creditsWorth}' and itemName '{buyableItem.itemName}'");
				buyableItem.creditsWorth = 10; //set price to 10
				SharedComponents.Logger.LogDebug($"Set buyable item '{buyableItem.itemName}' creditsWorth to '{buyableItem.creditsWorth}'");
			}
		}

		// private static void SetShipUpgradePricesInShop() {
		// 	foreach (var shipUpgrade in StartOfRound.Instance.unlockablesList.unlockables) {
		// 		SharedComponents.Logger.LogDebug($"shipUpgrade stuff -> '{shipUpgrade.unlockableName}', {shipUpgrade.unlockableType}");
		// 		Unlockables.RegisterUnlockable(shipUpgrade, 0, StoreType.ShipUpgrade);
		// 		Unlockables.UpdateUnlockablePrice(shipUpgrade, 5);
		// 		SharedComponents.Logger.LogDebug($"Found ship upgrade unlockableName '{shipUpgrade.unlockableName}'");
		// 	}
		// }
	}
}