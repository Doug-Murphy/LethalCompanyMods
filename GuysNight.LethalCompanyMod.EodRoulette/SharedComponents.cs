using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using System.IO;

namespace GuysNight.LethalCompanyMod.EodRoulette {
	public static class SharedComponents {
		public static ManualLogSource Logger { get; } = BepInEx.Logging.Logger.CreateLogSource(PluginInfo.PLUGIN_NAME);

		public static ConfigFile ConfigFile { get; } = new ConfigFile(Path.Combine(Paths.ConfigPath, $"{PluginInfo.PLUGIN_NAME}.cfg"), true) { SaveOnConfigSet = true };
	}
}