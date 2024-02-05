namespace GuysNight.LethalCompanyMod.BalancedItems {
	internal static class Constants {
		internal const string ConfigSectionHeaderToggles = "Feature Toggles";

		internal const string ConfigKeyToggleAverageSellValues = "EnableAverageSellValueOverrides";
		internal const string ConfigKeyToggleSellableEquipment = "EnableSellingEquipment";
		internal const string ConfigKeyToggleMoonRarity = "EnableMoonRarityOverrides";
		internal const string ConfigKeyToggleWeights = "EnableWeightOverrides";

		internal const string ConfigSectionHeaderAverageSellValues = "Average Sell Values";
		internal const string ConfigSectionHeaderMoonRarity = "Rarities for {0}";
		internal const string ConfigSectionHeaderWeight = "Weight";

		internal const string ConfigDescriptionAverageSellValues = "The average sell value for the '{0}' item. The default vanilla value is '{1}'.";
		internal const string ConfigDescriptionMoonRarity = "The rarity for the '{0}' item. The default vanilla value is '{1}'.";
		internal const string ConfigDescriptionWeight = "The weight for the '{0}' item. The default vanilla value is '{1}'.";

		internal const double SellValueVariance = 0.2;
	}
}