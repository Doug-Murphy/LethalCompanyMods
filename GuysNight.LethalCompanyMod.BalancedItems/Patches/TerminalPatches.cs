using HarmonyLib;

namespace GuysNight.LethalCompanyMod.BalancedItems.Patches {
	[HarmonyPatch(typeof(Terminal))]
	public class TerminalPatches {
		[HarmonyPatch("BeginUsingTerminal")]
		[HarmonyPrefix]
		public static void SetPricesInTerminal(Terminal __instance) {
			SharedComponents.ConfigFile.Reload();

			SetItemPricesInShop(__instance);
		}

		private static void SetItemPricesInShop(Terminal __instance) {
			foreach (var buyableItem in __instance.buyableItemsList) {
				SharedComponents.Logger.LogDebug($"Found buyable item '{buyableItem.itemName}' with creditsWorth '{buyableItem.creditsWorth}' and itemName '{buyableItem.itemName}'");
				buyableItem.creditsWorth = 10; //set price to 10
				SharedComponents.Logger.LogDebug($"Set buyable item '{buyableItem.itemName}' creditsWorth to '{buyableItem.creditsWorth}'");
			}
		}
	}
}