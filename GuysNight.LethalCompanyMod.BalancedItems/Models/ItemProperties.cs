namespace GuysNight.LethalCompanyMod.BalancedItems.Models {
	/// <summary>
	/// The various properties that we want to work with regarding items.
	/// </summary>
	public class ItemProperties {
		public ItemProperties() {
		}

		public ItemProperties(OverrideProperties overrideValues) {
			OverrideValues = overrideValues;
		}

		public ItemProperties(VanillaValues vanillaValues) {
			VanillaValues = vanillaValues;
		}

		public ItemProperties(VanillaValues vanillaValues, OverrideProperties overrideValues) {
			VanillaValues = vanillaValues;
			OverrideValues = overrideValues;
		}

		/// <summary>
		/// The overrides for this item.
		/// </summary>
		public OverrideProperties OverrideValues { get; } = new OverrideProperties();

		/// <summary>
		/// The vanilla values for this item.
		/// </summary>
		public VanillaValues VanillaValues { get; set; }
	}
}