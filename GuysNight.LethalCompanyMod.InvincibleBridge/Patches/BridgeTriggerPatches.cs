using HarmonyLib;

namespace GuysNight.LethalCompanyMod.InvincibleBridge.Patches {
	[HarmonyPatch(typeof(BridgeTrigger))]
	public class BridgeTriggerPatches {
		[HarmonyPatch("Update")]
		[HarmonyPrefix]
		public static void SetBridgeDurabilityToOne(BridgeTrigger __instance) {
			__instance.bridgeDurability = 1.0f;
		}
	}
}