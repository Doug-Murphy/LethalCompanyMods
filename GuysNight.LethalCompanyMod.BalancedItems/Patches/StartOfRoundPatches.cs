#pragma warning disable S1118
using GuysNight.LethalCompanyMod.BalancedItems.Containers;
using GuysNight.LethalCompanyMod.BalancedItems.Models.Items;
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

				ItemsContainer.SetVanillaValuesForItem(item.name, new VanillaItemValues(item.minValue, item.maxValue, item.weight));

				ConfigUtilities.SyncConfigForItemOverrides(item);
				if (item.name == "Key") {
					//make the key show up on the map
					item.isScrap = true;
				}
			}
		}

		private static void InitializeAllLevels(StartOfRound startOfRoundInstance) {
			foreach (var level in startOfRoundInstance.levels) {
				var vanillaRaritiesForMoon = new VanillaMoonRarities(level.name);
				var overriddenRaritiesForMoon = new OverrideMoonRarities(level.name);

				foreach (var spawnableScrap in level.spawnableScrap.OrderBy(s => s.spawnableItem.itemName)) {
					vanillaRaritiesForMoon.MoonRarityValues.TryAdd(spawnableScrap.spawnableItem.name, spawnableScrap.rarity);
					var configItemRarityOnMoon = ConfigUtilities.SyncConfigForMoonItemRarity(level, spawnableScrap);
					overriddenRaritiesForMoon.MoonRarityValues.TryAdd(spawnableScrap.spawnableItem.name, configItemRarityOnMoon);

					SharedComponents.Logger.LogDebug($"On level '{level.name}' we found a spawnable scrap item with name '{spawnableScrap.spawnableItem.name}', itemName '{spawnableScrap.spawnableItem.itemName}', weight '{NumericUtilities.DenormalizeWeight(spawnableScrap.spawnableItem.weight)}' pounds, rarity '{spawnableScrap.rarity}', min value '{spawnableScrap.spawnableItem.minValue}', and max value '{spawnableScrap.spawnableItem.maxValue}'");
				}

				MoonsContainer.Moons.TryAdd(level.name, new MoonProperties(vanillaRaritiesForMoon, overriddenRaritiesForMoon));
			}
		}
	}
}