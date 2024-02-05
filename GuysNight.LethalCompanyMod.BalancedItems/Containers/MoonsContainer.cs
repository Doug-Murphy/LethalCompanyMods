using GuysNight.LethalCompanyMod.BalancedItems.Models.Moons;
using System.Collections.Generic;

namespace GuysNight.LethalCompanyMod.BalancedItems.Containers {
	internal static class MoonsContainer {
		internal static Dictionary<string, MoonProperties> Moons { get; } = new Dictionary<string, MoonProperties>();
	}
}