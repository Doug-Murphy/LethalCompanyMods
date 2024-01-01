using System;
using GuysNight.LethalCompanyMod.BalancedItems.Utilities;

namespace GuysNight.LethalCompanyMod.BalancedItems.Models {
	/// <summary>
	/// Contains the data required for overriding various aspects of an item.
	/// </summary>
	public sealed class OverrideProperties {
		private float? _weight;

		/// <summary>
		/// The constructor to use when you do not want to override anything about the item.
		/// </summary>
		public OverrideProperties() {
		}

		/// <summary>
		/// The constructor to use when overriding only the item's weight is desired.
		/// </summary>
		/// <param name="weight">How much you want the item to weigh.</param>
		public OverrideProperties(float weight) {
			Weight = weight;
		}

		/// <summary>
		/// The constructor to use when overriding only the item's value is desired.
		/// </summary>
		/// <param name="averageValue">The average price that you want the scrap item to be worth.</param>
		public OverrideProperties(ushort averageValue) {
			AverageValue = averageValue;
		}

		/// <summary>
		/// The constructor to use when overriding the item's weight and value is desired.
		/// </summary>
		/// <param name="weight">How much you want the item to weigh.</param>
		/// <param name="averageValue">The average price that you want the scrap item to be worth.</param>
		public OverrideProperties(float weight, ushort averageValue) {
			AverageValue = averageValue;
			Weight = weight;
		}

		/// <summary>
		/// The average value of the item to override the default.
		/// </summary>
		private ushort? AverageValue { get; }

		/// <summary>
		/// The minimum value that the item can sell for.
		/// </summary>
		public int? MinValue {
			get {
				if (!AverageValue.HasValue) {
					return null;
				}

				return (int)Math.Round(AverageValue.Value - AverageValue.Value * .2, MidpointRounding.AwayFromZero);
			}
		}

		/// <summary>
		/// The maximum value that the item can sell for.
		/// </summary>
		public int? MaxValue {
			get {
				if (!AverageValue.HasValue) {
					return null;
				}

				return (int)Math.Round(AverageValue.Value + AverageValue.Value * .2, MidpointRounding.AwayFromZero);
			}
		}

		/// <summary>
		/// The weight to set for the item. This value is normalized to the game's representation when setting.
		/// </summary>
		/// <remarks>If the desired weight is 5 pounds, the normalized representation is 1.05</remarks>
		public float? Weight {
			get => _weight;

			internal set {
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
			return $"MinValue: {MinValue.ToString().PadLeft(3)}; AverageValue: {AverageValue.ToString().PadLeft(3)}; MaxValue {MaxValue.ToString().PadLeft(3)}; Weight: {(Weight.HasValue ? NumericUtilities.DenormalizeWeight(Weight.Value).ToString().PadLeft(5) : null)};";
		}
	}
}