#pragma warning disable S1118
using System;
using System.Linq;
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
					//add weight to config if it not already present
					SharedComponents.ConfigFile.Bind(Constants.ConfigSectionHeaderWeight,
						spawnableScrap.spawnableItem.name,
						NumericUtilities.DenormalizeWeight(spawnableScrap.spawnableItem.weight),
						string.Format(Constants.ConfigDescriptionWeight, spawnableScrap.spawnableItem.name)
					);

					//add sell value to config if it is not already present
					SharedComponents.ConfigFile.Bind(Constants.ConfigSectionHeaderAverageSellValues,
						spawnableScrap.spawnableItem.name,
						(int)Math.Round(new[] { spawnableScrap.spawnableItem.minValue, spawnableScrap.spawnableItem.maxValue }.Average(), MidpointRounding.AwayFromZero),
						string.Format(Constants.ConfigDescriptionAverageSellValues, spawnableScrap.spawnableItem.name));

					SharedComponents.Logger.LogInfo($"On level '{level.name}' we found a spawnable scrap item named '{spawnableScrap.spawnableItem.name}' with weight '{NumericUtilities.DenormalizeWeight(spawnableScrap.spawnableItem.weight)}' pounds, rarity '{spawnableScrap.rarity}', min value '{spawnableScrap.spawnableItem.minValue}', and max value '{spawnableScrap.spawnableItem.maxValue}'");
				}
			}
		}
	}
}