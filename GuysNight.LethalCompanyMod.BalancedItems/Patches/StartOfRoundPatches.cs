#pragma warning disable S1118
using GuysNight.LethalCompanyMod.BalancedItems.Models;
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
				SharedComponents.Logger.LogWarning($"__instance is null in '{nameof(InitializeAllLevelsAndSpawnableScrap)}'. Aborting.");

				return;
			}

			InitializeAllItemsInItemsContainer(__instance);
			InitializeAllLevelsInItemsContainer(__instance);

			SharedComponents.ConfigFile.Save();
		}

		private static void InitializeAllItemsInItemsContainer(StartOfRound __instance) {
			SharedComponents.Logger.LogDebug($"Found {__instance.allItemsList.itemsList.Count} items in __instance.allItemsList.itemsList.");
			SharedComponents.ConfigFile.Reload();
			foreach (var item in __instance.allItemsList.itemsList.OrderBy(i => i.itemName)) {
				SharedComponents.Logger.LogDebug("allItemsListEntry: " +
				                                 $"name = '{item.name}', " +
				                                 $"itemName = '{item.itemName}', " +
				                                 $"weight = '{item.weight}', " +
				                                 $"minValue = '{item.minValue}', " +
				                                 $"maxValue = '{item.maxValue}', " +
				                                 $"isScrap = '{item.isScrap}'");

				ItemsContainer.SetVanillaValues(item.name, new VanillaValues(item.minValue, item.maxValue, item.weight));

				ConfigUtilities.SyncConfigForItemOverrides(item);
				if (item.name == "Key") {
					//make the key show up on the map
					item.isScrap = true;
				}
			}
		}

		private static void InitializeAllLevelsInItemsContainer(StartOfRound __instance) {
			SharedComponents.Logger.LogDebug($"Found {__instance.levels.Length} levels.");

			foreach (var level in __instance.levels) {
				foreach (var spawnableScrap in level.spawnableScrap.OrderBy(s => s.spawnableItem.itemName)) {
					SharedComponents.Logger.LogDebug($"On level '{level.name}' we found a spawnable scrap item with name '{spawnableScrap.spawnableItem.name}', itemName '{spawnableScrap.spawnableItem.itemName}', weight '{NumericUtilities.DenormalizeWeight(spawnableScrap.spawnableItem.weight)}' pounds, rarity '{spawnableScrap.rarity}', min value '{spawnableScrap.spawnableItem.minValue}', and max value '{spawnableScrap.spawnableItem.maxValue}'");
					ItemsContainer.SetVanillaValues(spawnableScrap.spawnableItem.name, new VanillaValues(spawnableScrap.spawnableItem.minValue, spawnableScrap.spawnableItem.maxValue, spawnableScrap.spawnableItem.weight));

					if (bool.TryParse(SharedComponents.ConfigFile[Constants.ConfigSectionHeaderToggles, Constants.ConfigKeyToggleMoonRarity].GetSerializedValue(), out var isMoonRarityFeatureEnabled)) {
						SharedComponents.Logger.LogDebug($"Successfully retrieved moon rarity override feature toggle. Value is '{isMoonRarityFeatureEnabled}'");
					}
					else {
						SharedComponents.Logger.LogWarning("Could not retrieve moon rarity override feature toggle from config. Assuming it was set to true.");
						isMoonRarityFeatureEnabled = true;
					}

					if (!ItemsContainer.Items.TryGetValue(spawnableScrap.spawnableItem.name, out var itemEntry)) {
						//should be impossible so long we initialize the collection in StartOfRoundPatches
						SharedComponents.Logger.LogWarning($"No item entry exists for item '{spawnableScrap.spawnableItem.name}'. Making no changes to item moon rarity.");

						continue;
					}

					if (isMoonRarityFeatureEnabled) {
						//retrieve latest value from config
						itemEntry = ConfigUtilities.SyncConfigForItemRarityOverride(level, spawnableScrap);
						UpdateItemRarity(level.name, spawnableScrap, itemEntry.Overrides.MoonRarities[level.name].GetValueOrDefault());
					}
					else {
						UpdateItemRarity(level.name, spawnableScrap, itemEntry.VanillaValues.MoonRarities[level.name]);
					}
				}
			}
		}

		private static void UpdateItemRarity(string levelName, SpawnableItemWithRarity item, byte rarity) {
			item.rarity = rarity;
			SharedComponents.Logger.LogInfo($"Successfully set rarity to be '{rarity}' for item '{item.spawnableItem.name}' on '{levelName}'.");
		}
	}
}