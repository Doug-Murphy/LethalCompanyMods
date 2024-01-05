﻿#pragma warning disable S101
#pragma warning disable S1118

using HarmonyLib;
using System;

namespace GuysNight.LethalCompanyMod.BalancedItems.Patches {
	[HarmonyPatch(typeof(HUDManager))]
	public class HUDManagerPatches {
		[HarmonyPatch("Update")]
		[HarmonyPostfix]
		public static void DisplayCorrectCarryWeight(HUDManager __instance) {
			if (__instance is null) {
				SharedComponents.Logger.LogInfo($"__instance is null in '{nameof(DisplayCorrectCarryWeight)}'. Aborting.");

				return;
			}

			var num = (float)Math.Round(Math.Clamp(GameNetworkManager.Instance.localPlayerController.carryWeight - 1f, 0.0f, 100f) * 100f);
			__instance.weightCounter.text = $"{num} lb";
		}
	}
}