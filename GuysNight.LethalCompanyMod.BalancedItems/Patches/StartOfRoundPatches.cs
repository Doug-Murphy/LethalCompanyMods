#pragma warning disable S1118
using GuysNight.LethalCompanyMod.BalancedItems.Models;
using GuysNight.LethalCompanyMod.BalancedItems.Models.Moons;
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

			SharedComponents.ConfigFile.Reload();

			InitializeAllItems(__instance);
			InitializeAllLevels(__instance);

			SharedComponents.ConfigFile.Save();
		}

		private static void InitializeAllItems(StartOfRound startOfRoundInstance) {
			SharedComponents.Logger.LogDebug($"Found {startOfRoundInstance.allItemsList.itemsList.Count} items in allItemsList.");
			foreach (var item in startOfRoundInstance.allItemsList.itemsList.OrderBy(i => i.itemName)) {
				SharedComponents.Logger.LogDebug("allItemsListEntry: " +
				                                 $"name = '{item.name}', " +
				                                 $"itemName = '{item.itemName}', " +
				                                 $"weight = '{item.weight}', " +
				                                 $"minValue = '{item.minValue}', " +
				                                 $"maxValue = '{item.maxValue}', " +
				                                 $"isScrap = '{item.isScrap}'");

				ItemsContainer.SetVanillaValuesForItem(item.name, new VanillaValues(item.minValue, item.maxValue, item.weight));

				ConfigUtilities.SyncConfigForItemOverrides(item);
				if (item.name == "Key") {
					//make the key show up on the map
					item.isScrap = true;
				}
			}
		}

		private static void InitializeAllLevels(StartOfRound startOfRoundInstance) {
			var isMoonRarityFeatureEnabled = true;

			if (SharedComponents.ConfigFile.TryGetEntry<bool>(Constants.ConfigSectionHeaderToggles, Constants.ConfigKeyToggleMoonRarity, out var featureToggleConfigEntry)) {
				isMoonRarityFeatureEnabled = featureToggleConfigEntry.Value;
				SharedComponents.Logger.LogDebug($"Successfully retrieved moon rarity override feature toggle. Value is '{isMoonRarityFeatureEnabled}'");
			}
			else {
				SharedComponents.Logger.LogWarning("Could not retrieve moon rarity override feature toggle from config. Assuming it was set to true.");
			}

			foreach (var level in startOfRoundInstance.levels) {
				var vanillaRaritiesForMoon = new VanillaMoonRarities(level.name);
				var overriddenRaritiesForMoon = new OverrideMoonRarities(level.name);

				foreach (var spawnableScrap in level.spawnableScrap.OrderBy(s => s.spawnableItem.itemName)) {
					var configItemRarityOnMoon = ConfigUtilities.SyncConfigForMoonItemRarity(level, spawnableScrap);
					overriddenRaritiesForMoon.MoonRarityValues.TryAdd(spawnableScrap.spawnableItem.name, configItemRarityOnMoon);
					vanillaRaritiesForMoon.MoonRarityValues.TryAdd(spawnableScrap.spawnableItem.name, spawnableScrap.rarity);

					SharedComponents.Logger.LogDebug($"On level '{level.name}' we found a spawnable scrap item with name '{spawnableScrap.spawnableItem.name}', itemName '{spawnableScrap.spawnableItem.itemName}', weight '{NumericUtilities.DenormalizeWeight(spawnableScrap.spawnableItem.weight)}' pounds, rarity '{spawnableScrap.rarity}', min value '{spawnableScrap.spawnableItem.minValue}', and max value '{spawnableScrap.spawnableItem.maxValue}'");

					if (isMoonRarityFeatureEnabled) {
						UpdateItemRarityOnMoon(level.name, spawnableScrap, overriddenRaritiesForMoon.MoonRarityValues[spawnableScrap.spawnableItem.name]);
					}
					else {
						UpdateItemRarityOnMoon(level.name, spawnableScrap, vanillaRaritiesForMoon.MoonRarityValues[spawnableScrap.spawnableItem.name]);
					}
				}

				MoonsContainer.SetVanillaMoonRarityValuesForMoon(level.name, vanillaRaritiesForMoon);
			}
		}

		private static void UpdateItemRarityOnMoon(string moonName, SpawnableItemWithRarity item, int rarity) {
			item.rarity = rarity;
			SharedComponents.Logger.LogInfo($"Successfully set rarity to be '{rarity}' for item '{item.spawnableItem.name}' on '{moonName}'.");
		}
	}
}