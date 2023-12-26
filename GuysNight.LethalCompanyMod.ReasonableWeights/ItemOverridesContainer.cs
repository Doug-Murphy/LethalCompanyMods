using GuysNight.LethalCompanyMod.ReasonableWeights.Models;

namespace GuysNight.LethalCompanyMod.ReasonableWeights {
	public static class ItemOverridesContainer {
		public static ItemPropertyOverride[] ItemOverrides { get; } = {
			//spawned loot items
			new ItemPropertyOverride("Key", 0),

			//purchased items
			new ItemPropertyOverride("BoomboxWeight", 5),
			new ItemPropertyOverride("ExtensionLadderWeight", 5),
			new ItemPropertyOverride("FlashlightWeight", 2),
			new ItemPropertyOverride("JetpackWeight", 25),
			new ItemPropertyOverride("LockPickerWeight", 2),
			new ItemPropertyOverride("ProFlashlightWeight", 2),
			new ItemPropertyOverride("RadarBoosterWeight", 5),
			new ItemPropertyOverride("ShovelWeight", 5),
			new ItemPropertyOverride("SprayPaintWeight", 1),
			new ItemPropertyOverride("StunGrenadeWeight", 2),
			new ItemPropertyOverride("TZPInhalantWeight", 2),
			new ItemPropertyOverride("WalkieTalkieWeight", 2),
			new ItemPropertyOverride("ZapGunWeight", 4)
		};
	}
}