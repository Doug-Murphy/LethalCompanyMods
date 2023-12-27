using GuysNight.LethalCompanyMod.ReasonableWeights.Utilities;

namespace GuysNight.LethalCompanyMod.ReasonableWeights.Models {
	/// <summary>
	/// Contains the data required for overriding various aspects of an item.
	/// </summary>
	public sealed class ItemPropertyOverride {
		public ItemPropertyOverride(string name) {
			Name = name;
		}
		
		public ItemPropertyOverride(string name, float weight) {
			Name = name;
			Weight = NumericUtilities.NormalizeWeight(weight);
		}

		/// <summary>
		/// The name of the item to override.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// The weight to set for the item. This value should be normalized to the game's representation.
		/// </summary>
		/// <remarks>If the desired weight is 5 pounds, the normalized representation is 1.05</remarks>
		public float? Weight { get; }

		public override string ToString() {
			return $"Name: {Name}; Weight: {(Weight.HasValue ? NumericUtilities.DenormalizeWeight(Weight.Value) : (float?)null)};";
		}
	}
}