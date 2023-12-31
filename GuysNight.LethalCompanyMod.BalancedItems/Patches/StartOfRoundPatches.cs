#pragma warning disable S1118
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
					SharedComponents.Logger.LogInfo($"On level '{level.name}' we found a spawnable scrap item named '{spawnableScrap.spawnableItem.name}' with weight '{NumericUtilities.DenormalizeWeight(spawnableScrap.spawnableItem.weight)}' pounds, rarity '{spawnableScrap.rarity}', min value '{spawnableScrap.spawnableItem.minValue}', and max value '{spawnableScrap.spawnableItem.maxValue}'");
				}
			}
		}
	}
}