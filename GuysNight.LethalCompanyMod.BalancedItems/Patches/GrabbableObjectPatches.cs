#pragma warning disable	S1118

using System;
using System.Linq;
using GuysNight.LethalCompanyMod.BalancedItems.Models;
using GuysNight.LethalCompanyMod.BalancedItems.Utilities;
using HarmonyLib;

namespace GuysNight.LethalCompanyMod.BalancedItems.Patches {
	[HarmonyPatch(typeof(GrabbableObject))]
	public class GrabbableObjectPatches {
		[HarmonyPatch("Start")]
		[HarmonyPostfix]
		public static void ChangeItemWeight(GrabbableObject __instance) {
			if (__instance == null) {
				SharedComponents.Logger.LogInfo("__instance is null for some reason. Exiting override.");

				return;
			}

			SharedComponents.Logger.LogInfo($"item.itemProperties.name is '{__instance.itemProperties.name}'");
			SharedComponents.Logger.LogInfo($"item.itemProperties.weight is '{NumericUtilities.DenormalizeWeight(__instance.itemProperties.weight)}'");
			SharedComponents.Logger.LogInfo($"item.itemProperties.minValue is '{__instance.itemProperties.minValue}'");
			SharedComponents.Logger.LogInfo($"item.itemProperties.maxValue is '{__instance.itemProperties.maxValue}'");

			SharedComponents.Logger.LogInfo($"Begin adding config entries and setting override values for '{__instance.itemProperties.name}'");

			var itemWeight = __instance.itemProperties.weight;
			var itemMinValue = __instance.itemProperties.minValue;
			var itemMaxValue = __instance.itemProperties.maxValue;
			var itemAverageValue = (ushort)Math.Round(new[] { itemMinValue, itemMaxValue }.Average(), MidpointRounding.AwayFromZero);

			//if the overrides container does not contain an override for the current grabbable item, add an entry
			if (!ItemOverridesContainer.ItemOverrides.ContainsKey(__instance.itemProperties.name)) {
				ItemOverridesContainer.ItemOverrides[__instance.itemProperties.name] = new OverrideProperties();
			}

			var itemOverrides = ItemOverridesContainer.ItemOverrides[__instance.itemProperties.name];

			//if weight is not added in the config, add it for future
			//if weight is added in the config, retrieve the value and set it in the overrides
			itemOverrides.Weight = SharedComponents.ConfigFile.Bind(Constants.ConfigSectionHeaderWeight,
				__instance.itemProperties.name,
				Math.Abs(itemOverrides.Weight - default(float)) > 0 ? NumericUtilities.DenormalizeWeight(itemOverrides.Weight) : NumericUtilities.DenormalizeWeight(itemWeight),
				string.Format(Constants.ConfigDescriptionWeight, __instance.itemProperties.name)
			).Value;

			//if sell value is not added in the config, add it for future
			//if sell value is added in the config, retrieve the value and set it in the overrides
			itemOverrides.AverageValue = SharedComponents.ConfigFile.Bind(Constants.ConfigSectionHeaderAverageSellValues,
				__instance.itemProperties.name,
				itemOverrides.AverageValue != default ? itemOverrides.AverageValue : itemAverageValue,
				string.Format(Constants.ConfigDescriptionAverageSellValues, __instance.itemProperties.name)).Value;

			SharedComponents.Logger.LogInfo($"Finish adding config entries and setting override values for '{__instance.itemProperties.name}'");

			UpdateItemWeight(__instance, itemOverrides.Weight);
		}

		private static void UpdateItemWeight(GrabbableObject item, float weight) {
			item.itemProperties.weight = weight;
			SharedComponents.Logger.LogInfo($"Successfully overrode weight for '{item.itemProperties.name}' to be '{NumericUtilities.DenormalizeWeight(weight)}'");
		}
	}
}