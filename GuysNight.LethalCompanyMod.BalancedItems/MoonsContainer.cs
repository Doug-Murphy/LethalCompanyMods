using GuysNight.LethalCompanyMod.BalancedItems.Models.Moons;
using System.Collections.Generic;

namespace GuysNight.LethalCompanyMod.BalancedItems {
	public static class MoonsContainer {
		public static Dictionary<string, MoonProperties> Moons { get; } = new Dictionary<string, MoonProperties>();

		public static bool SetVanillaMoonRarityValuesForMoon(string moonName, VanillaMoonRarities moonRarityValues) {
			if (Moons.TryGetValue(moonName, out var moonEntry)) {
				if (moonEntry.VanillaMoonRarities is null || moonEntry.VanillaMoonRarities.MoonRarityValues.Count == 0) {
					moonEntry.VanillaMoonRarities = moonRarityValues;
					Moons[moonName] = moonEntry;

					SharedComponents.Logger.LogDebug($"Vanilla rarities have been set for moon '{moonName}'");

					return true;
				}

				SharedComponents.Logger.LogDebug($"Vanilla rarities for moon '{moonName}' were already set.");

				return false;
			}

			Moons.Add(moonName, new MoonProperties(moonRarityValues));

			return true;
		}
	}
}