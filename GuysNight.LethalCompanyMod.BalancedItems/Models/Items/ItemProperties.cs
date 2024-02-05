namespace GuysNight.LethalCompanyMod.BalancedItems.Models.Items {
	/// <summary>
	/// The various properties that we want to work with regarding items.
	/// </summary>
	internal sealed class ItemProperties {
		internal ItemProperties() {
		}

		internal ItemProperties(OverrideItemValues overrideItemValues) {
			OverrideItemValues = overrideItemValues;
		}

		internal ItemProperties(VanillaItemValues vanillaItemValues) {
			VanillaItemValues = vanillaItemValues;
		}

		internal ItemProperties(VanillaItemValues vanillaItemValues, OverrideItemValues overrideItemValues) {
			VanillaItemValues = vanillaItemValues;
			OverrideItemValues = overrideItemValues;
		}

		/// <summary>
		/// The overrides for this item.
		/// </summary>
		internal OverrideItemValues OverrideItemValues { get; } = new OverrideItemValues();

		/// <summary>
		/// The vanilla values for this item.
		/// </summary>
		internal VanillaItemValues VanillaItemValues { get; set; }
	}
}