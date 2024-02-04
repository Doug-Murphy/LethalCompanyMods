namespace GuysNight.LethalCompanyMod.BalancedItems.Models.Moons {
	/// <summary>
	/// The various properties that we want to work with regarding moons.
	/// </summary>
	public class MoonProperties {
		public MoonProperties() {
		}

		public MoonProperties(OverrideMoonRarities overrideMoonRarities) {
			OverrideMoonRarities = overrideMoonRarities;
		}

		public MoonProperties(VanillaMoonRarities vanillaMoonRarities) {
			VanillaMoonRarities = vanillaMoonRarities;
		}

		public MoonProperties(VanillaMoonRarities vanillaMoonRarities, OverrideMoonRarities overrideMoonRarities) {
			VanillaMoonRarities = vanillaMoonRarities;
			OverrideMoonRarities = overrideMoonRarities;
		}

		/// <summary>
		/// The overrides of the moon rarity values.
		/// </summary>
		public OverrideMoonRarities OverrideMoonRarities { get; set; }

		/// <summary>
		/// The vanilla values of the moon rarity values.
		/// </summary>
		public VanillaMoonRarities VanillaMoonRarities { get; set; }
	}
}