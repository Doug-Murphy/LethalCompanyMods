#pragma warning disable S101
#pragma warning disable S1118

using HarmonyLib;
using System;

namespace GuysNight.LethalCompanyMod.BalancedItems.Patches {
	[HarmonyPatch(typeof(HUDManager))]
	public class HUDManagerPatches {
		private static float? _originalFontSize;

		[HarmonyPatch("Update")]
		[HarmonyPostfix]
		public static void DisplayCorrectCarryWeight(HUDManager __instance) {
			if (__instance is null) {
				SharedComponents.Logger.LogInfo($"__instance is null in '{nameof(DisplayCorrectCarryWeight)}'. Aborting.");

				return;
			}

			if (GameNetworkManager.Instance?.localPlayerController is null) {
				SharedComponents.Logger.LogInfo($"Instance or localPlayerController is null in '{nameof(DisplayCorrectCarryWeight)}'. Aborting.");

				return;
			}

			var num = (float)Math.Round(Math.Clamp(GameNetworkManager.Instance.localPlayerController.carryWeight - 1f, 0.0f, 100f) * 100f);
			__instance.weightCounter.text = $"{num} lb";
		}

		/// <summary>
		/// Store original font size of the total value text box so we can change it back if needed.
		/// </summary>
		/// <param name="__instance"></param>
		[HarmonyPatch("Update")]
		[HarmonyPrefix]
		public static void StoreVariablesBeforeUpdateChanges(HUDManager __instance) {
			if (!_originalFontSize.HasValue) {
				SharedComponents.Logger.LogInfo($"Original totalValueText.fontSize was '{__instance.totalValueText.fontSize}'");
				_originalFontSize = __instance.totalValueText.fontSize; //default is 21.48
			}
		}

		/// <summary>
		/// When displaying total scrap value, display it as a formatted number and don't limit to 10,000
		/// </summary>
		/// <param name="__instance"></param>
		[HarmonyPatch("Update")]
		[HarmonyPostfix]
		public static void DisplayFormattedTotalScrapValue(HUDManager __instance) {
			if (__instance.totalScrapScanned >= 1_000_000) {
				__instance.totalValueText.fontSize = 12;
			}
			else if (__instance.totalScrapScanned >= 100_000) {
				__instance.totalValueText.fontSize = 14;
			}
			else if (__instance.totalScrapScanned >= 10_000) {
				__instance.totalValueText.fontSize = 16;
			}
			else if (__instance.totalScrapScanned >= 1_000) {
				__instance.totalValueText.fontSize = 18;
			}
			else {
				__instance.totalValueText.fontSize = _originalFontSize.Value;
			}

			__instance.totalValueText.text = $"${__instance.totalScrapScanned:N0}";
		}
	}
}