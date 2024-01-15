#pragma warning disable	S1118

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

			SharedComponents.Logger.LogDebug($"Begin adding config entries and setting override values for '{__instance.itemProperties.name}'");

			SharedComponents.ConfigFile.Reload();
			var itemEntry = ConfigUtilities.SyncConfigForItemOverrides(__instance.itemProperties);

			if (bool.TryParse(SharedComponents.ConfigFile[Constants.ConfigSectionHeaderToggles, Constants.ConfigKeyToggleWeights].GetSerializedValue(), out var isWeightFeatureEnabled)) {
				SharedComponents.Logger.LogDebug($"Successfully retrieved weight override feature toggle. Value is '{isWeightFeatureEnabled}'");
			}
			else {
				SharedComponents.Logger.LogWarning("Could not retrieve weight override feature toggle from config. Assuming it was set to true.");
				isWeightFeatureEnabled = true;
			}

			if (isWeightFeatureEnabled) {
				UpdateItemWeight(__instance, itemEntry.Overrides.Weight);
			}
			else {
				UpdateItemWeight(__instance, itemEntry.VanillaValues.Weight);
			}

			SharedComponents.ConfigFile.Save();
		}

		private static void UpdateItemWeight(GrabbableObject item, float weight) {
			item.itemProperties.weight = weight;
			SharedComponents.Logger.LogInfo($"Successfully set weight for '{item.itemProperties.name}' to be '{NumericUtilities.DenormalizeWeight(weight)}'");
		}
	}
}