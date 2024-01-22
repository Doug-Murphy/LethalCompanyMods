#pragma warning disable S1118

using BepInEx.Logging;
using HarmonyLib;
using System;

namespace GuysNight.LethalCompanyMod.EodRoulette.Patches {
	[HarmonyPatch(typeof(Landmine))]
	public class LandminePatches {
		private static readonly Random RandomGenerator = new Random();
		private static readonly ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(PluginInfo.PLUGIN_NAME);

		[HarmonyPatch("TriggerMineOnLocalClientByExiting")]
		[HarmonyPrefix]
		public static void PotentiallyAvoidExplosion(Landmine __instance) {
			var randomNumber = RandomGenerator.Next(1, 101); //generate random int between 1 and 100, inclusive

			Logger.LogDebug($"Random number generated {randomNumber}");
			if (randomNumber < 90) {
				//the random number generation landed within the range of FAILURE. So let the normal event occur
				Logger.LogDebug("The number generated resulted in a failed defusal. Letting game's code execute.");

				return;
			}

			// the random generation landed within the range of a success
			Logger.LogDebug("The number generated resulted in a successful defusal. Doing overrides.");

			//stop the landmine red light from blinking and stop the sound it makes
			__instance.mineAnimator.enabled = false;
			//mark that the landmine has exploded so that the game's method does nothing.
			__instance.hasExploded = true;
		}
	}
}