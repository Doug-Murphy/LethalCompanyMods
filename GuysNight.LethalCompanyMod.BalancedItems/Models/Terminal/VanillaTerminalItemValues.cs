namespace GuysNight.LethalCompanyMod.BalancedItems.Models.Terminal {
	internal sealed class VanillaTerminalItemValues {
		internal VanillaTerminalItemValues(int purchasePrice) {
			PurchasePrice = purchasePrice;
		}

		/// <summary>
		/// The price of the item when buying via the terminal.
		/// </summary>
		internal int PurchasePrice { get; }

		public override string ToString() {
			return $"PurchasePrice: '{PurchasePrice}'";
		}
	}
}