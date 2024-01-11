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

			var itemEntry = ConfigUtilities.SyncConfigForItemOverrides(__instance.itemProperties);

			if (!ItemsContainer.Items.ContainsKey(__instance.itemProperties.name)) {
				//should be impossible so long as we sync with config before this check
				SharedComponents.Logger.LogWarning($"No item entry exists for item '{__instance.itemProperties.name}'. Making no changes to item weight.");

				return;
			}

			UpdateItemWeight(__instance, itemEntry.Overrides.Weight);

			SharedComponents.ConfigFile.Save();
		}

		private static void UpdateItemWeight(GrabbableObject item, float weight) {
			item.itemProperties.weight = weight;
			SharedComponents.Logger.LogInfo($"Successfully overrode weight for '{item.itemProperties.name}' to be '{NumericUtilities.DenormalizeWeight(weight)}'");
		}
	}
}