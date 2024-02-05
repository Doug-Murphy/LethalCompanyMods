#pragma warning disable S1118
using HarmonyLib;
using System;

namespace GuysNight.LethalCompanyMod.BalancedItems.Patches {
	[HarmonyPatch(typeof(LungProp))]
	public class LungPropPatches {
		private static readonly Random RandomGenerator = new Random();

		[HarmonyPatch("DisconnectFromMachinery")]
		[HarmonyPrefix]
		public static void SetApparatusValue(LungProp __instance) {
			if (!ItemsContainer.Items.TryGetValue(__instance.itemProperties.name, out var itemEntry)) {
				//should be impossible so long we initialize the collection in StartOfRoundPatches
				SharedComponents.Logger.LogWarning($"No item entry exists for item '{__instance.itemProperties.name}'. Making no changes to item value.");

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

			if (!isSellValueFeatureEnabled) {
				return;
			}

			var randomValue = RandomGenerator.Next(itemEntry.OverrideItemValues.MinValue, itemEntry.OverrideItemValues.MaxValue + 1);

			__instance.SetScrapValue(randomValue);
		}
	}
}