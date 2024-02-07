namespace GuysNight.LethalCompanyMod.BalancedItems.Models.Terminal {
	/// <summary>
	/// The various properties that we want to work with regarding terminal items.
	/// </summary>
	internal sealed class TerminalItemProperties {
		internal TerminalItemProperties() {
		}

		internal TerminalItemProperties(OverrideTerminalItemValues overrideTerminalItemValues) {
			OverrideTerminalItemValues = overrideTerminalItemValues;
		}

		internal TerminalItemProperties(VanillaTerminalItemValues vanillaTerminalItemValues) {
			VanillaTerminalItemValues = vanillaTerminalItemValues;
		}

		internal TerminalItemProperties(VanillaTerminalItemValues vanillaTerminalItemValues, OverrideTerminalItemValues overrideTerminalItemValues) {
			VanillaTerminalItemValues = vanillaTerminalItemValues;
			OverrideTerminalItemValues = overrideTerminalItemValues;
		}

		/// <summary>
		/// The overrides for this item.
		/// </summary>
		internal OverrideTerminalItemValues OverrideTerminalItemValues { get; } = new OverrideTerminalItemValues();

		/// <summary>
		/// The vanilla values for this item.
		/// </summary>
		internal VanillaTerminalItemValues VanillaTerminalItemValues { get; set; }
	}
}