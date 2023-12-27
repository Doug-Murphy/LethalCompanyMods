using System;

namespace GuysNight.LethalCompanyMod.BalancedItems.Utilities {
	public static class NumericUtilities {
		public static float NormalizeWeight(float denormalizedWeight) {
			var normalizedWeight = (float)Math.Round(denormalizedWeight / 100f + 1, 3, MidpointRounding.AwayFromZero);

			return normalizedWeight;
		}

		public static float DenormalizeWeight(float normalizedWeight) {
			var denormalizedWeight = (float)Math.Round((normalizedWeight - 1) * 100, 1, MidpointRounding.AwayFromZero);

			return denormalizedWeight;
		}
	}
}