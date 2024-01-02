#pragma warning disable S1118
using System;
using System.Linq;
using GuysNight.LethalCompanyMod.BalancedItems.Models;
using GuysNight.LethalCompanyMod.BalancedItems.Utilities;
using HarmonyLib;

namespace GuysNight.LethalCompanyMod.BalancedItems.Patches {
	[HarmonyPatch(typeof(StartOfRound))]
	public class StartOfRoundPatches {
		[HarmonyPrefix]
		[HarmonyPatch("Start")]
		public static void PrintAllLevelsAndSpawnableScrap(StartOfRound __instance) {
			if (__instance is null) {
				SharedComponents.Logger.LogInfo($"__instance is null in '{nameof(PrintAllLevelsAndSpawnableScrap)}'. Aborting.");

				return;
			}

			SharedComponents.Logger.LogInfo($"Found {__instance.levels.Length} levels.");
			foreach (var level in __instance.levels) {
				foreach (var spawnableScrap in level.spawnableScrap.OrderBy(s => s.spawnableItem.name)) {
					SharedComponents.Logger.LogInfo($"On level '{level.name}' we found a spawnable scrap item named '{spawnableScrap.spawnableItem.name}' with weight '{NumericUtilities.DenormalizeWeight(spawnableScrap.spawnableItem.weight)}' pounds, rarity '{spawnableScrap.rarity}', min value '{spawnableScrap.spawnableItem.minValue}', and max value '{spawnableScrap.spawnableItem.maxValue}'");
					SharedComponents.Logger.LogInfo($"Begin adding config entries and setting override values for '{spawnableScrap.spawnableItem.name}'");
					
					var itemWeight = spawnableScrap.spawnableItem.weight;
					var itemMinValue = spawnableScrap.spawnableItem.minValue;
					var itemMaxValue = spawnableScrap.spawnableItem.maxValue;
					var itemAverageValue = (ushort)Math.Round(new[] { itemMinValue, itemMaxValue }.Average(), MidpointRounding.AwayFromZero);

					//if the overrides container does not contain an override for the current scrap item, add an entry
					if (!ItemOverridesContainer.ItemOverrides.ContainsKey(spawnableScrap.spawnableItem.name)) {
						ItemOverridesContainer.ItemOverrides[spawnableScrap.spawnableItem.name] = new OverrideProperties();
					}

					var itemOverrides = ItemOverridesContainer.ItemOverrides[spawnableScrap.spawnableItem.name];

					//if weight is not added in the config, add it for future
					//if weight is added in the config, retrieve the value and set it in the overrides
					itemOverrides.Weight = NumericUtilities.NormalizeWeight(SharedComponents.ConfigFile.Bind(Constants.ConfigSectionHeaderWeight,
						spawnableScrap.spawnableItem.name,
						NumericUtilities.DenormalizeWeight(itemWeight),
						string.Format(Constants.ConfigDescriptionWeight, spawnableScrap.spawnableItem.name)
					).Value);

					//if sell value is not added in the config, add it for future
					//if sell value is added in the config, retrieve the value and set it in the overrides
					itemOverrides.AverageValue = SharedComponents.ConfigFile.Bind(Constants.ConfigSectionHeaderAverageSellValues,
						spawnableScrap.spawnableItem.name,
						itemAverageValue,
						string.Format(Constants.ConfigDescriptionAverageSellValues, spawnableScrap.spawnableItem.name)).Value;
					
					SharedComponents.Logger.LogInfo($"Finish adding config entries and setting override values for '{spawnableScrap.spawnableItem.name}'");
				}
			}
		}
	}
}