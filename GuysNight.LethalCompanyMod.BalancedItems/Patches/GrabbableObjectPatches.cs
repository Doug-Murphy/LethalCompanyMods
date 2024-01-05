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
				SharedComponents.Logger.LogInfo($"__instance is null in '{nameof(ChangeItemWeight)}'. Aborting.");

				return;
			}

			SharedComponents.Logger.LogInfo($"item.itemProperties.name is '{__instance.itemProperties.name}'");
			SharedComponents.Logger.LogInfo($"item.itemProperties.weight is '{NumericUtilities.DenormalizeWeight(__instance.itemProperties.weight)}'");
			SharedComponents.Logger.LogInfo($"item.itemProperties.minValue is '{__instance.itemProperties.minValue}'");
			SharedComponents.Logger.LogInfo($"item.itemProperties.maxValue is '{__instance.itemProperties.maxValue}'");

			SharedComponents.Logger.LogInfo($"Begin adding config entries and setting override values for '{__instance.itemProperties.name}'");

			ConfigUtilities.SyncConfigForItemOverrides(__instance.itemProperties, out var itemOverrides);

			UpdateItemWeight(__instance, itemOverrides.Weight);
		}

		private static void UpdateItemWeight(GrabbableObject item, float weight) {
			item.itemProperties.weight = weight;
			SharedComponents.Logger.LogInfo($"Successfully overrode weight for '{item.itemProperties.name}' to be '{NumericUtilities.DenormalizeWeight(weight)}'");
		}
	}
}