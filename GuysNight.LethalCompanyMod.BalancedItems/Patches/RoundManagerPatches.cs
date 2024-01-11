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
				SharedComponents.Logger.LogInfo($"__instance is null in '{nameof(ChangeScrapValues)}'. Aborting.");

				return;
			}

			foreach (var spawnableScrap in __instance.currentLevel.spawnableScrap.Select(x => x.spawnableItem)) {
				SharedComponents.Logger.LogInfo($"spawnableScrap.name is '{spawnableScrap.name}'");
				SharedComponents.Logger.LogInfo($"spawnableScrap.itemName is '{spawnableScrap.itemName}'");
				SharedComponents.Logger.LogInfo($"spawnableScrap.minValue is '{spawnableScrap.minValue}'");
				SharedComponents.Logger.LogInfo($"spawnableScrap.maxValue is '{spawnableScrap.maxValue}'");

				var itemEntry = ConfigUtilities.SyncConfigForItemOverrides(spawnableScrap);

				if (!ItemsContainer.Items.ContainsKey(spawnableScrap.name)) {
					//should be impossible so long as we sync with config before this check
					SharedComponents.Logger.LogWarning($"No item entry exists for item '{spawnableScrap.name}'. Making no changes to item value.");

					continue;
				}

				UpdateItemValue(spawnableScrap, itemEntry.Overrides.MinValue, itemEntry.Overrides.MaxValue);
			}

			SharedComponents.ConfigFile.Save();
		}

		private static void UpdateItemValue(Item item, int minValue, int maxValue) {
			item.minValue = minValue;
			item.maxValue = maxValue;
			SharedComponents.Logger.LogInfo($"Successfully override sell value range for '{item.name}' to be '{minValue}' - '{maxValue}'");
		}
	}
}