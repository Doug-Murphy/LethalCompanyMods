using System.Collections.Generic;

namespace GuysNight.LethalCompanyMod.BalancedItems.Models {
	public sealed class VanillaValues {
		public VanillaValues(int minValue, int maxValue, float weight) {
			MinValue = minValue;
			MaxValue = maxValue;
			Weight = weight;
		}

		public int MinValue { get; }

		public int MaxValue { get; }

		public Dictionary<string, byte> MoonRarities { get; } = new Dictionary<string, byte>();

		public float Weight { get; }
	}
}