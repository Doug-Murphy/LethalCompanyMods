using GuysNight.LethalCompanyMod.BalancedItems.Utilities;
using System;

namespace GuysNight.LethalCompanyMod.BalancedItems.Models.Items {
	/// <summary>
	/// Contains the data required for overriding various aspects of an item.
	/// </summary>
	internal sealed class OverrideItemValues {
		private float _weight;

		/// <summary>
		/// Default constructor for programmatically adding new overrides to our collection.
		/// </summary>
		internal OverrideItemValues() {
		}

		/// <summary>
		/// The constructor to use when overriding only the item's weight is desired.
		/// </summary>
		/// <param name="weight">How much you want the item to weigh.</param>
		internal OverrideItemValues(float weight) {
			Weight = weight;
		}

		/// <summary>
		/// The average value of the item to override the default.
		/// </summary>
		internal ushort AverageValue { get; set; }

		/// <summary>
		/// The minimum value that the item can sell for.
		/// </summary>
		internal int MinValue => (int)Math.Round(AverageValue - AverageValue * Constants.SellValueVariance, MidpointRounding.AwayFromZero);

		/// <summary>
		/// The maximum value that the item can sell for.
		/// </summary>
		internal int MaxValue => (int)Math.Round(AverageValue + AverageValue * Constants.SellValueVariance, MidpointRounding.AwayFromZero);

		/// <summary>
		/// The weight to set for the item. This value is normalized to the game's representation when setting.
		/// </summary>
		/// <remarks>If the desired weight is 5 pounds, the normalized representation is 1.05</remarks>
		internal float Weight {
			get => _weight;

			set {
				if (value < 0) {
					throw new ArgumentOutOfRangeException(nameof(value), "You cannot set a negative weight.");
				}

				_weight = NumericUtilities.NormalizeWeight(value);
			}
		}

		public override string ToString() {
			return $"MinValue: '{MinValue}' AvgValue: '{AverageValue}' MaxValue: '{MaxValue}' Weight:'{Weight}'";
		}
	}
}