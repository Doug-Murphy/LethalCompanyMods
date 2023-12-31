#pragma warning disable	S1118

using System.Linq;
using GuysNight.LethalCompanyMod.BalancedItems.Models;
using HarmonyLib;

namespace GuysNight.LethalCompanyMod.BalancedItems.Patches {
	[HarmonyPatch(typeof(RoundManager))]
	public class RoundManagerPatches {
		[HarmonyPrefix]
		[HarmonyPatch("SpawnScrapInLevel")]
		public static void ChangeScrapValues(RoundManager __instance) {
			if (__instance == null) {
				SharedComponents.Logger.LogInfo("__instance is null for some reason. Exiting override.");

				return;
			}

			foreach (var spawnableScrap in __instance.currentLevel.spawnableScrap.Select(x => x.spawnableItem)) {
				SharedComponents.Logger.LogInfo($"spawnableScrap.name is '{spawnableScrap.name}'");
				SharedComponents.Logger.LogInfo($"spawnableScrap.minValue is '{spawnableScrap.minValue}'");
				SharedComponents.Logger.LogInfo($"spawnableScrap.maxValue is '{spawnableScrap.maxValue}'");

				if (!ItemOverridesContainer.ItemOverrides.TryGetValue(spawnableScrap.name, out var itemOverride)) {
					SharedComponents.Logger.LogInfo("No override exists for this item. Making no changes.");

					return;
				}

				if (!itemOverride.MinValue.HasValue || !itemOverride.MaxValue.HasValue) {
					SharedComponents.Logger.LogInfo(@$"An item override was found, but it did not have both a {nameof(ItemPropertyOverride.MinValue)} and {nameof(ItemPropertyOverride.MaxValue)} specified. {nameof(ItemPropertyOverride.MinValue)} = '{itemOverride.MinValue}' {nameof(ItemPropertyOverride.MaxValue)} = '{itemOverride.MaxValue}'");

					continue;
				}

				UpdateItemValue(spawnableScrap, itemOverride.MinValue.Value, itemOverride.MaxValue.Value);
			}
		}

		private static void UpdateItemValue(Item item, int minValue, int maxValue) {
			item.minValue = minValue;
			item.maxValue = maxValue;
			SharedComponents.Logger.LogInfo($"Successfully override sell value range for '{item.name}' to be '{minValue}' - '{maxValue}'");
		}
	}
}