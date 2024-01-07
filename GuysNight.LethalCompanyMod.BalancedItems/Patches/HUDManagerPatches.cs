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
			if (_originalFontSize.HasValue) {
				return;
			}

			SharedComponents.Logger.LogInfo($"Previous fontSize was '{__instance.totalValueText.fontSize}'");
			_originalFontSize = __instance.totalValueText.fontSize;
		}

		/// <summary>
		/// When displaying total scrap value, display it as a formatted number and don't limit to 10,000
		/// </summary>
		/// <param name="__instance"></param>
		[HarmonyPatch("Update")]
		[HarmonyPostfix]
		public static void DisplayFormattedTotalScrapValue(HUDManager __instance) {
			var totalScrapScannedDisplayNumFieldInfo = AccessTools.Field(typeof(HUDManager), "totalScrapScannedDisplayNum");
			var fieldRef = AccessTools.FieldRefAccess<HUDManager, int>(totalScrapScannedDisplayNumFieldInfo);
			var totalScrapScannedDisplayNum = fieldRef(__instance);
			totalScrapScannedDisplayNum = Math.Clamp(totalScrapScannedDisplayNum, 0, 999_999);
			if (totalScrapScannedDisplayNum > 9_950) {
				SharedComponents.Logger.LogInfo($"Setting __instance.totalValueText.text to {totalScrapScannedDisplayNum}");
			}

			if (totalScrapScannedDisplayNum >= 10_000) {
				__instance.totalValueText.fontSize = 16;
			}
			else if (totalScrapScannedDisplayNum >= 1_000) {
				__instance.totalValueText.fontSize = 18.5f;
			}
			else {
				__instance.totalValueText.fontSize = _originalFontSize.Value;
			}

			//helpful, but doesn't seem to size itself small enough to stay fitting on one line
			// __instance.totalValueText.enableAutoSizing = true;

			__instance.totalValueText.text = $"${totalScrapScannedDisplayNum:N0}";
		}
	}
}