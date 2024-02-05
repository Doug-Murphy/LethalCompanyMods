namespace GuysNight.LethalCompanyMod.BalancedItems.Models.Moons {
	/// <summary>
	/// The various properties that we want to work with regarding moons.
	/// </summary>
	internal class MoonProperties {
		internal MoonProperties() {
		}

		internal MoonProperties(OverrideMoonRarities overrideMoonRarities) {
			OverrideMoonRarities = overrideMoonRarities;
		}

		internal MoonProperties(VanillaMoonRarities vanillaMoonRarities) {
			VanillaMoonRarities = vanillaMoonRarities;
		}

		internal MoonProperties(VanillaMoonRarities vanillaMoonRarities, OverrideMoonRarities overrideMoonRarities) {
			VanillaMoonRarities = vanillaMoonRarities;
			OverrideMoonRarities = overrideMoonRarities;
		}

		/// <summary>
		/// The overrides of the moon rarity values.
		/// </summary>
		internal OverrideMoonRarities OverrideMoonRarities { get; set; }

		/// <summary>
		/// The vanilla values of the moon rarity values.
		/// </summary>
		internal VanillaMoonRarities VanillaMoonRarities { get; set; }
	}
}