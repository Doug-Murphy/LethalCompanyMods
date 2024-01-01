#pragma warning disable	S1118

using GuysNight.LethalCompanyMod.BalancedItems.Utilities;
using HarmonyLib;

namespace GuysNight.LethalCompanyMod.BalancedItems.Patches {
	[HarmonyPatch(typeof(GrabbableObject))]
	public class GrabbableObjectPatches {
		[HarmonyPatch("Start")]
		[HarmonyPostfix]
		public static void ChangeItemWeights(GrabbableObject __instance) {
			if (__instance == null) {
				SharedComponents.Logger.LogInfo("__instance is null for some reason. Exiting override.");

				return;
			}

			SharedComponents.Logger.LogInfo($"item.itemProperties.name is '{__instance.itemProperties.name}'");
			SharedComponents.Logger.LogInfo($"item.itemProperties.weight is '{NumericUtilities.DenormalizeWeight(__instance.itemProperties.weight)}'");

			if (!ItemOverridesContainer.ItemOverrides.TryGetValue(__instance.itemProperties.name, out var itemOverride)) {
				SharedComponents.Logger.LogInfo("No override exists for this item. Making no changes.");

				return;
			}

			UpdateItemWeight(__instance, itemOverride.Weight);
		}

		private static void UpdateItemWeight(GrabbableObject item, float weight) {
			item.itemProperties.weight = weight;
			SharedComponents.Logger.LogInfo($"Successfully overrode weight for '{item.itemProperties.name}' to be '{NumericUtilities.DenormalizeWeight(weight)}'");
		}
	}
}