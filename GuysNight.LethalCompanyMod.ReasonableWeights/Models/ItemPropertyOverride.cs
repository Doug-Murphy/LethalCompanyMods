namespace GuysNight.LethalCompanyMod.ReasonableWeights.Models {
	/// <summary>
	/// Contains the data required for overriding various aspects of an item.
	/// </summary>
	public sealed class ItemPropertyOverride {
		public ItemPropertyOverride(string name, float weight) {
			Name = name;
			Weight = weight;
		}

		/// <summary>
		/// The name of the item to override.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// The weight to set for the item.
		/// </summary>
		public float Weight { get; }

		public override string ToString() {
			return $"Name: {Name}; Weight: {Weight};";
		}
	}
}