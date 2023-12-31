using System;
using GuysNight.LethalCompanyMod.BalancedItems.Utilities;

namespace GuysNight.LethalCompanyMod.BalancedItems.Models {
	/// <summary>
	/// Contains the data required for overriding various aspects of an item.
	/// </summary>
	public sealed class ItemPropertyOverride {
		private ushort? _averageValue;
		private float? _weight;

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
			Name = name;
			Weight = weight;
		}

		/// <summary>
		/// The constructor to use when overriding only the item's value is desired.
		/// </summary>
		/// <param name="name">The item name.</param>
		/// <param name="weight">How much you want the item to weigh.</param>
		/// <param name="averageValue">The average price that you want the scrap item to be worth.</param>
		public ItemPropertyOverride(string name, float weight, ushort averageValue) {
			AverageValue = averageValue;
			Name = name;
			Weight = weight;
		}

		/// <summary>
		/// The constructor to use when overriding only the item's value is desired.
		/// </summary>
		/// <param name="name">The item name.</param>
		/// <param name="averageValue">The average price that you want the scrap item to be worth.</param>
		public ItemPropertyOverride(string name, ushort averageValue) {
			AverageValue = averageValue;
			Name = name;
		}

		/// <summary>
		/// The name of the item to override.
		/// </summary>
		public string Name { get; }

		private ushort? AverageValue {
			get => _averageValue;

			set {
				if (!value.HasValue) {
					return;
				}

				if (value < 0) {
					throw new ArgumentOutOfRangeException(nameof(value), "You cannot set a negative average value.");
				}

				_averageValue = value.Value;
			}
		}

		/// <summary>
		/// The minimum value that the item can sell for.
		/// </summary>
		public int? MinValue {
			get {
				if (!_averageValue.HasValue) {
					return null;
				}

				return (int)Math.Round(_averageValue.Value - _averageValue.Value * .2, MidpointRounding.AwayFromZero);
			}
		}

		/// <summary>
		/// The maximum value that the item can sell for.
		/// </summary>
		public int? MaxValue {
			get {
				if (!_averageValue.HasValue) {
					return null;
				}

				return (int)Math.Round(_averageValue.Value + _averageValue.Value * .2, MidpointRounding.AwayFromZero);
			}
		}

		/// <summary>
		/// The weight to set for the item. This value is normalized to the game's representation when setting.
		/// </summary>
		/// <remarks>If the desired weight is 5 pounds, the normalized representation is 1.05</remarks>
		public float? Weight {
			get => _weight;

			private set {
				if (!value.HasValue) {
					return;
				}

				if (value < 0) {
					throw new ArgumentOutOfRangeException(nameof(value), "You cannot set a negative weight.");
				}

				_weight = NumericUtilities.NormalizeWeight(value.Value);
			}
		}

		public override string ToString() {
			return $"Name: {Name}; MinValue: {MinValue}; AverageValue: {AverageValue}; MaxValue {MaxValue}; Weight: {(Weight.HasValue ? NumericUtilities.DenormalizeWeight(Weight.Value) : (float?)null)};";
		}
	}
}