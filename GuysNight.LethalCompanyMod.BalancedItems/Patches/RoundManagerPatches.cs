#pragma warning disable	S1118

using GuysNight.LethalCompanyMod.BalancedItems.Utilities;
using HarmonyLib;

namespace GuysNight.LethalCompanyMod.BalancedItems.Patches {
	[HarmonyPatch(typeof(RoundManager))]
	public class RoundManagerPatches {
		[HarmonyPrefix]
		[HarmonyPatch("SpawnScrapInLevel")]
		public static void SpawnScrapInLevelPrefix(RoundManager __instance) {
			if (__instance is null) {
				SharedComponents.Logger.LogWarning($"__instance is null in '{nameof(SpawnScrapInLevelPrefix)}'. Aborting.");

				return;
			}

			SharedComponents.ConfigFile.Reload();
			foreach (var spawnableScrap in __instance.currentLevel.spawnableScrap) {
				SharedComponents.Logger.LogDebug($"spawnableScrap.name is '{spawnableScrap.spawnableItem.name}'");
				SharedComponents.Logger.LogDebug($"spawnableScrap.itemName is '{spawnableScrap.spawnableItem.itemName}'");
				SharedComponents.Logger.LogDebug($"spawnableScrap.minValue is '{spawnableScrap.spawnableItem.minValue}'");
				SharedComponents.Logger.LogDebug($"spawnableScrap.maxValue is '{spawnableScrap.spawnableItem.maxValue}'");

				ChangeScrapRarity(__instance.currentLevel, spawnableScrap);
				ChangeScrapValue(spawnableScrap.spawnableItem);
			}

			SharedComponents.ConfigFile.Save();
		}

		private static void ChangeScrapRarity(SelectableLevel level, SpawnableItemWithRarity spawnableScrap) {
			if (!MoonsContainer.Moons.TryGetValue(level.name, out var moonEntry)) {
				//should be impossible so long we initialize the collection in StartOfRoundPatches
				SharedComponents.Logger.LogWarning($"No moon entry exists for moon '{level.name}'. Making no changes to item rarity.");

				return;
			}

			var isMoonRarityFeatureEnabled = true;

			if (SharedComponents.ConfigFile.TryGetEntry<bool>(Constants.ConfigSectionHeaderToggles, Constants.ConfigKeyToggleMoonRarity, out var featureToggleConfigEntry)) {
				isMoonRarityFeatureEnabled = featureToggleConfigEntry.Value;
				SharedComponents.Logger.LogDebug($"Successfully retrieved moon rarity override feature toggle. Value is '{isMoonRarityFeatureEnabled}'");
			}
			else {
				SharedComponents.Logger.LogWarning("Could not retrieve moon rarity override feature toggle from config. Assuming it was set to true.");
			}

			if (isMoonRarityFeatureEnabled) {
				var configMoonRarityForItem = ConfigUtilities.GetItemRarityForMoon(level, spawnableScrap);
				if (configMoonRarityForItem.HasValue) {
					moonEntry.OverrideMoonRarities.MoonRarityValues[spawnableScrap.spawnableItem.name] = configMoonRarityForItem.Value;
					MoonsContainer.Moons[level.name] = moonEntry;
					UpdateItemRarityOnMoon(level.name, spawnableScrap, moonEntry.OverrideMoonRarities.MoonRarityValues[spawnableScrap.spawnableItem.name]);
				}
			}
			else {
				UpdateItemRarityOnMoon(level.name, spawnableScrap, moonEntry.VanillaMoonRarities.MoonRarityValues[spawnableScrap.spawnableItem.name]);
			}
		}

		private static void UpdateItemRarityOnMoon(string moonName, SpawnableItemWithRarity item, int rarity) {
			item.rarity = rarity;
			SharedComponents.Logger.LogInfo($"Successfully set rarity to be '{rarity}' for item '{item.spawnableItem.name}' on '{moonName}'.");
		}

		private static void ChangeScrapValue(Item spawnableScrap) {
			if (!ItemsContainer.Items.TryGetValue(spawnableScrap.name, out var itemEntry)) {
				//should be impossible so long we initialize the collection in StartOfRoundPatches
				SharedComponents.Logger.LogWarning($"No item entry exists for item '{spawnableScrap.name}'. Making no changes to item value.");

				return;
			}

			var isSellValueFeatureEnabled = true;

			if (SharedComponents.ConfigFile.TryGetEntry<bool>(Constants.ConfigSectionHeaderToggles, Constants.ConfigKeyToggleAverageSellValues, out var featureToggleConfigEntry)) {
				isSellValueFeatureEnabled = featureToggleConfigEntry.Value;
				SharedComponents.Logger.LogDebug($"Successfully retrieved sell value override feature toggle. Value is '{isSellValueFeatureEnabled}'");
			}
			else {
				SharedComponents.Logger.LogWarning("Could not retrieve sell value override feature toggle from config. Assuming it was set to true.");
			}

			if (isSellValueFeatureEnabled) {
				//retrieve latest value from config
				itemEntry = ConfigUtilities.SyncConfigForItemOverrides(spawnableScrap);
				UpdateItemValue(spawnableScrap, itemEntry.OverrideValues.MinValue, itemEntry.OverrideValues.MaxValue);
			}
			else {
				UpdateItemValue(spawnableScrap, itemEntry.VanillaValues.MinValue, itemEntry.VanillaValues.MaxValue);
			}
		}

		private static void UpdateItemValue(Item item, int minValue, int maxValue) {
			item.minValue = minValue;
			item.maxValue = maxValue;
			SharedComponents.Logger.LogInfo($"Successfully set sell value range for '{item.name}' to be '{minValue}' - '{maxValue}'");
		}
	}
}