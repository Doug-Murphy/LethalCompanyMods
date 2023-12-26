using GuysNight.LethalCompanyMod.ReasonableWeights.Models;

namespace GuysNight.LethalCompanyMod.ReasonableWeights {
	public static class ItemOverridesContainer {
		public static ItemPropertyOverride[] ItemOverrides { get; } = {
			//spawned loot items
			new ItemPropertyOverride("Key", 0),

			//purchased items
			new ItemPropertyOverride("Boombox", 5),
			new ItemPropertyOverride("ExtensionLadder", 5),
			new ItemPropertyOverride("Flashlight", 2),
			new ItemPropertyOverride("Jetpack", 25),
			new ItemPropertyOverride("LockPicker", 2),
			new ItemPropertyOverride("ProFlashlight", 2),
			new ItemPropertyOverride("RadarBooster", 5),
			new ItemPropertyOverride("Shovel", 5),
			new ItemPropertyOverride("SprayPaint", 1),
			new ItemPropertyOverride("StunGrenade", 2),
			new ItemPropertyOverride("TZPInhalant", 2),
			new ItemPropertyOverride("WalkieTalkie", 2),
			new ItemPropertyOverride("ZapGun", 4)
		};
	}
}