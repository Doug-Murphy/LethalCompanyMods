#pragma warning disable	S1118

using GuysNight.LethalCompanyMod.BalancedItems.Containers;
using GuysNight.LethalCompanyMod.BalancedItems.Models.Items;
using GuysNight.LethalCompanyMod.BalancedItems.Utilities;
using HarmonyLib;

namespace GuysNight.LethalCompanyMod.BalancedItems.Patches {
	[HarmonyPatch(typeof(GrabbableObject))]
	public class GrabbableObjectPatches {
		[HarmonyPatch("Start")]
		[HarmonyPostfix]
		public static void ChangeItemWeight(GrabbableObject __instance) {
			if (__instance is null) {
				SharedComponents.Logger.LogWarning($"__instance is null in '{nameof(ChangeItemWeight)}'. Aborting.");

				return;
			}

			SharedComponents.Logger.LogDebug($"item.itemProperties.name is '{__instance.itemProperties.name}'");
			SharedComponents.Logger.LogDebug($"item.itemProperties.weight is '{NumericUtilities.DenormalizeWeight(__instance.itemProperties.weight)}'");
			SharedComponents.Logger.LogDebug($"item.itemProperties.minValue is '{__instance.itemProperties.minValue}'");
			SharedComponents.Logger.LogDebug($"item.itemProperties.maxValue is '{__instance.itemProperties.maxValue}'");

			if (!ItemsContainer.Items.TryGetValue(__instance.itemProperties.name, out var itemEntry)) {
				//should be impossible so long we initialize the collection in StartOfRoundPatches
				SharedComponents.Logger.LogWarning($"No item entry exists for item '{__instance.itemProperties.name}'. Making no changes to item weight.");

				return;
			}

			var isWeightFeatureEnabled = true;

			if (SharedComponents.ConfigFile.TryGetEntry<bool>(Constants.ConfigSectionHeaderToggles, Constants.ConfigKeyToggleWeights, out var featureToggleConfigEntry)) {
				isWeightFeatureEnabled = featureToggleConfigEntry.Value;
				SharedComponents.Logger.LogDebug($"Successfully retrieved weight override feature toggle. Value is '{isWeightFeatureEnabled}'");
			}
			else {
				SharedComponents.Logger.LogWarning("Could not retrieve weight override feature toggle from config. Assuming it was set to true.");
			}

			if (isWeightFeatureEnabled) {
				//retrieve latest value from config
				SharedComponents.ConfigFile.Reload();
				SharedComponents.Logger.LogDebug($"Begin adding config entries and setting override values for '{__instance.itemProperties.name}'");
				itemEntry = ConfigUtilities.SyncConfigForItemOverrides(__instance.itemProperties);
				UpdateItemWeight(__instance, itemEntry.OverrideItemValues.Weight);
			}
			else {
				if (itemEntry.VanillaItemValues is null) {
					SharedComponents.Logger.LogWarning($"Vanilla values for item '{__instance.itemProperties.name}' is null. Assuming current values are vanilla.");
					ItemsContainer.SetVanillaValuesForItem(__instance.itemProperties.name, new VanillaItemValues(__instance.itemProperties.minValue, __instance.itemProperties.maxValue, __instance.itemProperties.weight));
					itemEntry = ItemsContainer.Items[__instance.itemProperties.name];
				}

				UpdateItemWeight(__instance, itemEntry.VanillaItemValues!.Weight);
			}

			SharedComponents.ConfigFile.Save();
		}

		private static void UpdateItemWeight(GrabbableObject item, float weight) {
			item.itemProperties.weight = weight;
			SharedComponents.Logger.LogInfo($"Successfully set weight for '{item.itemProperties.name}' to be '{NumericUtilities.DenormalizeWeight(weight)}'");
		}
	}
}