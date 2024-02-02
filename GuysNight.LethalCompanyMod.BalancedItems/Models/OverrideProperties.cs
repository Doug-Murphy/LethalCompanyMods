using GuysNight.LethalCompanyMod.BalancedItems.Utilities;
using System;
using System.Collections.Generic;

namespace GuysNight.LethalCompanyMod.BalancedItems.Models {
	/// <summary>
	/// Contains the data required for overriding various aspects of an item.
	/// </summary>
	public sealed class OverrideProperties {
		private float _weight;

		/// <summary>
		/// Default constructor for programmatically adding new overrides to our collection.
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
		/// The average value of the item to override the default.
		/// </summary>
		public ushort AverageValue { get; set; }

		/// <summary>
		/// The minimum value that the item can sell for.
		/// </summary>
		public int MinValue => (int)Math.Round(AverageValue - AverageValue * Constants.SellValueVariance, MidpointRounding.AwayFromZero);

		/// <summary>
		/// The maximum value that the item can sell for.
		/// </summary>
		public int MaxValue => (int)Math.Round(AverageValue + AverageValue * Constants.SellValueVariance, MidpointRounding.AwayFromZero);

		/// <summary>
		/// Contains the rarity attributes for this item on the various moons.
		/// </summary>
		public Dictionary<string, byte?> MoonRarities { get; } = new Dictionary<string, byte?>();

		/// <summary>
		/// The weight to set for the item. This value is normalized to the game's representation when setting.
		/// </summary>
		/// <remarks>If the desired weight is 5 pounds, the normalized representation is 1.05</remarks>
		public float Weight {
			get => _weight;

			internal set {
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