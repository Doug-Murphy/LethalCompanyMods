using BepInEx.Configuration;
using BepInEx.Logging;

namespace GuysNight.LethalCompanyMod.BalancedItems {
	internal static class SharedComponents {
		internal static ManualLogSource Logger { get; set; }

		internal static ConfigFile ConfigFile { get; set; }
	}
}