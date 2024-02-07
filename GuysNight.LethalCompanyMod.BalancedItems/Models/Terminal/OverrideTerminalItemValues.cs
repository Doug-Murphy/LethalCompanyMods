namespace GuysNight.LethalCompanyMod.BalancedItems.Models.Terminal {
	/// <summary>
	/// Contains the data required for overriding various aspects of an item in the terminal.
	/// </summary>
	internal sealed class OverrideTerminalItemValues {
		/// <summary>
		/// Default constructor for programmatically adding new overrides to our collection.
		/// </summary>
		internal OverrideTerminalItemValues() {
		}

		/// <summary>
		/// The constructor to use when overriding only the item's purchase price is desired.
		/// </summary>
		/// <param name="purchasePrice">How much you want the item to cost in the terminal.</param>
		internal OverrideTerminalItemValues(int purchasePrice) {
			PurchasePrice = purchasePrice;
		}

		/// <summary>
		/// The price of the item when buying via the terminal.
		/// </summary>
		internal int PurchasePrice { get; set; }

		public override string ToString() {
			return $"PurchasePrice: '{PurchasePrice}'";
		}
	}
}