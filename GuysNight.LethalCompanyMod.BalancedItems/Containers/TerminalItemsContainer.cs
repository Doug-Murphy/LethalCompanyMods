using GuysNight.LethalCompanyMod.BalancedItems.Models.Terminal;
using System.Collections.Generic;

namespace GuysNight.LethalCompanyMod.BalancedItems.Containers {
	internal static class TerminalItemsContainer {
		internal static Dictionary<string, TerminalItemProperties> TerminalItems { get; } = new Dictionary<string, TerminalItemProperties>();

		internal static bool SetVanillaValuesForTerminalItem(string itemName, VanillaTerminalItemValues vanillaTerminalItemValues) {
			if (TerminalItems.TryGetValue(itemName, out var itemEntry)) {
				if (itemEntry.VanillaTerminalItemValues is null) {
					itemEntry.VanillaTerminalItemValues = vanillaTerminalItemValues;
					TerminalItems[itemName] = itemEntry;

					SharedComponents.Logger.LogDebug($"Vanilla values have been set for terminal item '{itemName}' to be '{vanillaTerminalItemValues}'.");

					return true;
				}

				SharedComponents.Logger.LogDebug($"Vanilla values were already set for terminal item '{itemName}' to be '{itemEntry.VanillaTerminalItemValues}'.");

				return false;
			}

			TerminalItems.Add(itemName, new TerminalItemProperties(vanillaTerminalItemValues));
			SharedComponents.Logger.LogDebug($"Created new terminal items container entry and set vanilla values for terminal item '{itemName}' to be '{vanillaTerminalItemValues}'.");

			return true;
		}
	}
}