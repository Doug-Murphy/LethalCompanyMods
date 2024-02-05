using GuysNight.LethalCompanyMod.BalancedItems.Models.Moons;
using System.Collections.Generic;

namespace GuysNight.LethalCompanyMod.BalancedItems {
	public static class MoonsContainer {
		public static Dictionary<string, MoonProperties> Moons { get; } = new Dictionary<string, MoonProperties>();
	}
}