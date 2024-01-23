﻿#pragma warning disable S1118

using HarmonyLib;
using System;

namespace GuysNight.LethalCompanyMod.EodRoulette.Patches {
	[HarmonyPatch(typeof(Landmine))]
	public class LandminePatches {
		private static readonly Random RandomGenerator = new Random();

		[HarmonyPatch("TriggerMineOnLocalClientByExiting")]
		[HarmonyPrefix]
		public static void PotentiallyAvoidExplosion(Landmine __instance) {
			if (byte.TryParse(SharedComponents.ConfigFile[Constants.ConfigSectionHeader, Constants.ConfigChanceToDisableEntryKey].GetSerializedValue(), out var chanceToDisable)) {
				SharedComponents.Logger.LogDebug($"Successfully retrieved chance to disable. Value is '{chanceToDisable}'");
			}
			else {
				SharedComponents.Logger.LogWarning($"Could not retrieve chance to disable from config. Assuming it was set to the default value of {Constants.DefaultChanceToDisable}.");
				chanceToDisable = Constants.DefaultChanceToDisable;
			}

			var randomNumber = RandomGenerator.Next(1, 101); //generate random int between 1 and 100, inclusive
			SharedComponents.Logger.LogDebug($"Random number generated {randomNumber}");
			if (randomNumber > chanceToDisable) {
				//the random number generation landed within the range of FAILURE. So let the normal event occur
				SharedComponents.Logger.LogDebug("The number generated resulted in a failed defusal. Letting game's code execute.");

				return;
			}

			// the random generation landed within the range of a success
			SharedComponents.Logger.LogDebug("The number generated resulted in a successful defusal. Doing overrides.");

			//stop the landmine red light from blinking and stop the sound it makes
			__instance.mineAnimator.enabled = false;
			//mark that the landmine has exploded so that the game's method does nothing.
			__instance.hasExploded = true;
		}
	}
}