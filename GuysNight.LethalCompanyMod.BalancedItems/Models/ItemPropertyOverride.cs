using System;
using GuysNight.LethalCompanyMod.BalancedItems.Utilities;

namespace GuysNight.LethalCompanyMod.BalancedItems.Models {
	/// <summary>
	/// Contains the data required for overriding various aspects of an item.
	/// </summary>
	public sealed class ItemPropertyOverride {
		/// <summary>
		/// The constructor to use when no overriding is desired.
		/// </summary>
		/// <param name="name">The item name.</param>
		public ItemPropertyOverride(string name) {
			Name = name;
		}
		
		/// <summary>
		/// The constructor to use when overriding only the item's weight is desired.
		/// </summary>
		/// <param name="name">The item name.</param>
		/// <param name="weight">How much you want the item to weigh.</param>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when weight is negative.</exception>
		public ItemPropertyOverride(string name, float weight) {
			if (weight < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(weight), "You cannot set a negative weight value.");
			}
			Name = name;
			Weight = NumericUtilities.NormalizeWeight(weight);
		}

		/// <summary>
		/// The constructor to use when overriding only the item's value is desired.
		/// </summary>
		/// <param name="name">The item name.</param>
		/// <param name="weight">How much you want the item to weigh.</param>
		/// <param name="minValue">How much you want the minimum sell value to be.</param>
		/// <param name="maxValue">How much you want the maximum sell value to be.</param>
		/// <exception cref="ArgumentException">Thrown when minValue is greater than maxValue.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when minValue is negative.</exception>
		public ItemPropertyOverride(string name, float weight, int minValue, int maxValue) {
			if (minValue < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(minValue), "You cannot set a negative minimum sell value.");
			}
			if (minValue > maxValue)
			{
				throw new ArgumentException("The minimum value cannot be larger than the maximum value.", nameof(minValue));
			}
			
			Name = name;
			MinValue = minValue;
			MaxValue = maxValue;
			Weight = weight;
		}
		
		/// <summary>
		/// The constructor to use when overriding only the item's value is desired.
		/// </summary>
		/// <param name="name">The item name.</param>
		/// <param name="minValue">How much you want the minimum sell value to be.</param>
		/// <param name="maxValue">How much you want the maximum sell value to be.</param>
		/// <exception cref="ArgumentException">Thrown when minValue is greater than maxValue.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when minValue is negative.</exception>
		public ItemPropertyOverride(string name, int minValue, int maxValue) {
			if (minValue < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(minValue), "You cannot set a negative minimum sell value.");
			}
			if (minValue > maxValue)
			{
				throw new ArgumentException("The minimum value cannot be larger than the maximum value.", nameof(minValue));
			}
			
			Name = name;
			MinValue = minValue;
			MaxValue = maxValue;
		}

		/// <summary>
		/// The name of the item to override.
		/// </summary>
		public string Name { get; }
		
		/// <summary>
		/// The minimum value that the item can sell for.
		/// </summary>
		public int? MinValue { get; }
		
		/// <summary>
		/// The maximum value that the item can sell for.
		/// </summary>
		public int? MaxValue { get; }

		/// <summary>
		/// The weight to set for the item. This value should be normalized to the game's representation.
		/// </summary>
		/// <remarks>If the desired weight is 5 pounds, the normalized representation is 1.05</remarks>
		public float? Weight { get; }

		public override string ToString() {
			return $"Name: {Name}; MinValue: {MinValue}; MaxValue {MaxValue}; Weight: {(Weight.HasValue ? NumericUtilities.DenormalizeWeight(Weight.Value) : (float?)null)};";
		}
	}
}