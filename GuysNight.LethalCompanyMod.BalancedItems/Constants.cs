namespace GuysNight.LethalCompanyMod.BalancedItems {
	public static class Constants {
		public const string ConfigSectionHeaderToggles = "Feature Toggles";

		public const string ConfigKeyToggleAverageSellValues = "EnableAverageSellValueOverrides";
		public const string ConfigKeyToggleMoonRarity = "EnableMoonRarityOverrides";
		public const string ConfigKeyToggleWeights = "EnableWeightOverrides";

		public const string ConfigSectionHeaderAverageSellValues = "Average Sell Values";
		public const string ConfigSectionHeaderMoonRarity = "Rarities for {0}";
		public const string ConfigSectionHeaderWeight = "Weight";

		public const string ConfigDescriptionAverageSellValues = "The average sell value for the '{0}' item. The default vanilla value is '{1}'.";
		public const string ConfigDescriptionMoonRarity = "The rarity for the '{0}' item. The default vanilla value is '{1}'.";
		public const string ConfigDescriptionWeight = "The weight for the '{0}' item. The default vanilla value is '{1}'.";

		public const double SellValueVariance = 0.2;
	}
}