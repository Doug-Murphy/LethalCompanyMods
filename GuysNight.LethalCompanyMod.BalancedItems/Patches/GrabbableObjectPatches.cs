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
			SharedComponents.Logger.LogInfo($"item.itemProperties.minValue is '{__instance.itemProperties.minValue}'");
			SharedComponents.Logger.LogInfo($"item.itemProperties.maxValue is '{__instance.itemProperties.maxValue}'");

			var itemOverride = Array.Find(ItemOverridesContainer.ItemOverrides, itemOverride => itemOverride.Name == __instance.itemProperties.name);
			if (itemOverride is null)
			{
				SharedComponents.Logger.LogInfo("Unable to find item to override. Making no changes.");

				return;
			}

			if (itemOverride.MinValue.HasValue && itemOverride.MaxValue.HasValue)
			{
				UpdateItemValue(__instance, itemOverride.MinValue.Value, itemOverride.MaxValue.Value);
			}

			if (itemOverride.Weight.HasValue)
			{
				UpdateItemWeight(__instance, itemOverride.Weight.Value);
			}
		}

		private static void UpdateItemValue(GrabbableObject item, int minValue, int maxValue) {
			SharedComponents.Logger.LogInfo($"Existing sell value range for '{item.itemProperties.name}' is '{item.itemProperties.minValue}' - '{item.itemProperties.maxValue}'");
			item.itemProperties.minValue = minValue;
			item.itemProperties.maxValue = maxValue;
			SharedComponents.Logger.LogInfo($"Successfully override sell value range for '{item.itemProperties.name}' to be '{minValue}' - '{maxValue}'");
		}

		private static void UpdateItemWeight(GrabbableObject item, float weight) {
			SharedComponents.Logger.LogInfo($"Existing weight for '{item.itemProperties.name}' is '{NumericUtilities.DenormalizeWeight(item.itemProperties.weight)}'");
			item.itemProperties.weight = weight;
			SharedComponents.Logger.LogInfo($"Successfully overrode weight for '{item.itemProperties.name}' to be '{NumericUtilities.DenormalizeWeight(weight)}'");
		}
	}
}