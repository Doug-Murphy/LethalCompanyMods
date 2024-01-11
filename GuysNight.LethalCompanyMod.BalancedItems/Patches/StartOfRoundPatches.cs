#pragma warning disable S1118
using GuysNight.LethalCompanyMod.BalancedItems.Utilities;
using HarmonyLib;
using System.Linq;

namespace GuysNight.LethalCompanyMod.BalancedItems.Patches {
	[HarmonyPatch(typeof(StartOfRound))]
	public class StartOfRoundPatches {
		[HarmonyPrefix]
		[HarmonyPatch("Start")]
		public static void InitializeAllLevelsAndSpawnableScrap(StartOfRound __instance) {
			if (__instance is null) {
				SharedComponents.Logger.LogInfo($"__instance is null in '{nameof(InitializeAllLevelsAndSpawnableScrap)}'. Aborting.");

				return;
			}

			SharedComponents.Logger.LogInfo($"Found {__instance.allItemsList.itemsList.Count} items in __instance.allItemsList.itemsList.");
			foreach (var item in __instance.allItemsList.itemsList.OrderBy(i => i.itemName)) {
				SharedComponents.Logger.LogInfo("allItemsListEntry: " +
				                                $"name = '{item.name}', " +
				                                $"itemName = '{item.itemName}', " +
				                                $"weight = '{item.weight}', " +
				                                $"minValue = '{item.minValue}', " +
				                                $"maxValue = '{item.maxValue}', " +
				                                $"isScrap = '{item.isScrap}'");

				ConfigUtilities.SyncConfigForItemOverrides(item);
			}

			SharedComponents.Logger.LogInfo($"Found {__instance.levels.Length} levels.");

			foreach (var level in __instance.levels) {
				foreach (var spawnableScrap in level.spawnableScrap.OrderBy(s => s.spawnableItem.itemName)) {
					SharedComponents.Logger.LogInfo($"On level '{level.name}' we found a spawnable scrap item with name '{spawnableScrap.spawnableItem.name}', itemName '{spawnableScrap.spawnableItem.itemName}', weight '{NumericUtilities.DenormalizeWeight(spawnableScrap.spawnableItem.weight)}' pounds, rarity '{spawnableScrap.rarity}', min value '{spawnableScrap.spawnableItem.minValue}', and max value '{spawnableScrap.spawnableItem.maxValue}'");
					var itemEntry = ConfigUtilities.SyncConfigForItemRarityOverride(level, spawnableScrap);
					if (!ItemsContainer.Items.ContainsKey(spawnableScrap.spawnableItem.name)) {
						//should be impossible so long as we sync with config before this check
						SharedComponents.Logger.LogWarning($"No item entry exists for item '{spawnableScrap.spawnableItem.name}'. Making no changes to item spawn rarity on moon '{level.name}'.");

						continue;
					}

					UpdateItemRarity(level.name, spawnableScrap, itemEntry.Overrides.MoonRarities[level.name].GetValueOrDefault());
				}
			}

			SharedComponents.ConfigFile.Save();
		}

		private static void UpdateItemRarity(string levelName, SpawnableItemWithRarity item, byte rarity) {
			item.rarity = rarity;
			SharedComponents.Logger.LogInfo($"Successfully overrode rarity to be '{rarity}' for item '{item.spawnableItem.name}' on '{levelName}'.");
		}
	}
}