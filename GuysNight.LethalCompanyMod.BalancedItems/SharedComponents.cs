using BepInEx.Configuration;
using BepInEx.Logging;

namespace GuysNight.LethalCompanyMod.BalancedItems {
	public static class SharedComponents {
		public static ManualLogSource Logger { get; set; }

		public static ConfigFile ConfigFile { get; set; }
	}
}