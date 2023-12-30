#pragma warning disable	S1118

using System;
using GuysNight.LethalCompanyMod.BalancedItems.Utilities;
using HarmonyLib;

namespace GuysNight.LethalCompanyMod.BalancedItems.Patches {
	[HarmonyPatch(typeof(GrabbableObject))]
	public class GrabbableObjectPatches {
		[HarmonyPatch("Start")]
		[HarmonyPostfix]
		public static void ChangeItemWeights(GrabbableObject __instance) {
			if (__instance == null)
			{
				SharedComponents.Logger.LogInfo("__instance is null for some reason. Exiting override.");

				return;
			}

			SharedComponents.Logger.LogInfo($"item.itemProperties.name is '{__instance.itemProperties.name}'");
			SharedComponents.Logger.LogInfo($"item.itemProperties.weight is '{NumericUtilities.DenormalizeWeight(__instance.itemProperties.weight)}'");

			var itemOverride = Array.Find(ItemOverridesContainer.ItemOverrides, itemOverride => itemOverride.Name == __instance.itemProperties.name);
			if (itemOverride is null)
			{
				SharedComponents.Logger.LogInfo("No override exists for this item. Making no changes.");

				return;
			}

			if (!itemOverride.Weight.HasValue)
			{
				SharedComponents.Logger.LogInfo("An item override was found, but it did not have a weight specified.");

				return;
			}

			UpdateItemWeight(__instance, itemOverride.Weight.Value);
		}

		private static void UpdateItemWeight(GrabbableObject item, float weight) {
			item.itemProperties.weight = weight;
			SharedComponents.Logger.LogInfo($"Successfully overrode weight for '{item.itemProperties.name}' to be '{NumericUtilities.DenormalizeWeight(weight)}'");
		}
	}
}