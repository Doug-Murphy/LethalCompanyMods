using System.Linq;
using HarmonyLib;

namespace GuysNight.LethalCompanyMod.ReasonableWeights.Patches {
	[HarmonyPatch(typeof(GrabbableObject))]
	public class GrabbableObjectPatches {
		[HarmonyPatch("Start")]
		[HarmonyPostfix]
		public static void ChangeItemWeights(GrabbableObject __instance) {
			SharedComponents.Logger.LogInfo("Begin override for GrabbableObject.Start()");

			if (__instance == null)
			{
				SharedComponents.Logger.LogInfo("__instance is null for some reason. Exiting override.");

				return;
			}

			SharedComponents.Logger.LogInfo($"item.itemProperties.name is '{__instance.itemProperties.name}'");

			var itemOverride = ItemOverridesContainer.ItemOverrides.FirstOrDefault(itemOverride => itemOverride.Name == __instance.itemProperties.name);
			if (itemOverride is null)
			{
				SharedComponents.Logger.LogInfo("Unable to find item to override. Making no changes.");

				return;
			}

			__instance.itemProperties.weight = itemOverride.Weight;
			SharedComponents.Logger.LogInfo($"Overrode properties for '{__instance.itemProperties.name}' to be {itemOverride}");
		}
	}
}