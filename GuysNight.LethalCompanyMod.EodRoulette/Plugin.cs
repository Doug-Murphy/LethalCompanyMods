using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System.Reflection;

namespace GuysNight.LethalCompanyMod.EodRoulette {
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	public class Plugin : BaseUnityPlugin {
		private void Awake() {
			Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

			SharedComponents.ConfigFile.Bind("Settings",
				"ChanceToDisable",
				Constants.DefaultChanceToDisable,
				new ConfigDescription("The percentage chance that the mine will be disabled when stepping off of it. This value can be set anytime and the updated value will be respected upon stepping off of the mine.", new AcceptableValueRange<byte>(0, 100)));
		}
	}
}