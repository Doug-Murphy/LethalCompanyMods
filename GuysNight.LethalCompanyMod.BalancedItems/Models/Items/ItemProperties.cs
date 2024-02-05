namespace GuysNight.LethalCompanyMod.BalancedItems.Models.Items {
	/// <summary>
	/// The various properties that we want to work with regarding items.
	/// </summary>
	internal sealed class ItemProperties {
		internal ItemProperties() {
		}

		internal ItemProperties(OverrideProperties overrideValues) {
			OverrideValues = overrideValues;
		}

		internal ItemProperties(VanillaValues vanillaValues) {
			VanillaValues = vanillaValues;
		}

		internal ItemProperties(VanillaValues vanillaValues, OverrideProperties overrideValues) {
			VanillaValues = vanillaValues;
			OverrideValues = overrideValues;
		}

		/// <summary>
		/// The overrides for this item.
		/// </summary>
		internal OverrideProperties OverrideValues { get; } = new OverrideProperties();

		/// <summary>
		/// The vanilla values for this item.
		/// </summary>
		internal VanillaValues VanillaValues { get; set; }
	}
}