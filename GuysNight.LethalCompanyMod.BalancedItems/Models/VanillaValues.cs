namespace GuysNight.LethalCompanyMod.BalancedItems.Models {
	public sealed class VanillaValues {
		public VanillaValues(int minValue, int maxValue, float weight) {
			MinValue = minValue;
			MaxValue = maxValue;
			Weight = weight;
		}

		public int MinValue { get; }

		public int MaxValue { get; }

		public float Weight { get; }

		public override string ToString() {
			return $"MinValue: '{MinValue}' MaxValue: '{MaxValue}' Weight:'{Weight}'";
		}
	}
}