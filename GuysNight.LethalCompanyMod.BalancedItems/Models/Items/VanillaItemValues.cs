namespace GuysNight.LethalCompanyMod.BalancedItems.Models.Items {
	internal sealed class VanillaItemValues {
		internal VanillaItemValues(int minValue, int maxValue, float weight) {
			MinValue = minValue;
			MaxValue = maxValue;
			Weight = weight;
		}

		internal int MinValue { get; }

		internal int MaxValue { get; }

		internal float Weight { get; }

		public override string ToString() {
			return $"MinValue: '{MinValue}' MaxValue: '{MaxValue}' Weight:'{Weight}'";
		}
	}
}