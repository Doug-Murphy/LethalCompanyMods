#pragma warning disable S1075
using BepInEx;
using GuysNight.LethalCompanyMod.Terminal.Cowsay.Models;
using HarmonyLib;
using System.Net.Http;
using System.Reflection;
using System.Web;
using TerminalApi.Classes;
using UnityEngine;
using static TerminalApi.TerminalApi;

namespace GuysNight.LethalCompanyMod.Terminal.Cowsay {
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	[BepInDependency("atomic.terminalapi", "1.5.0")]
	public class Plugin : BaseUnityPlugin {
		private const string ChuckNorrisApiUrl = "https://api.chucknorris.io/jokes/random";
		private const string CowsayApiUrl = "https://cowsay.morecode.org/say";
		private static readonly HttpClient HttpClient = new HttpClient();

		private void Awake() {
			Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
			AddCommand("cowsay", new CommandInfo {
				Category = "other",
				Description = "Displays a cow telling you a random Chuck Norris joke.",
				DisplayTextSupplier = GetCowsayChuckNorris,
				Title = "Cowsay"
			});
		}

		private string GetCowsayChuckNorris() {
			var chuckNorrisResponse = HttpClient.GetAsync(ChuckNorrisApiUrl).GetAwaiter().GetResult();

			if (!chuckNorrisResponse.IsSuccessStatusCode) {
				Logger.LogWarning($"Chuck Norris API did not return a successful status code. Returned {chuckNorrisResponse.StatusCode} with content {chuckNorrisResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult()}");

				return "An error occurred. Please try again later.";
			}

			var serializedChuckNorrisResponse = chuckNorrisResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
			var deserializedChuckNorrisResponse = JsonUtility.FromJson<ChuckNorrisResponse>(serializedChuckNorrisResponse);
			Logger.LogDebug($"Chuck Norris API response is '{serializedChuckNorrisResponse}'");
			if (deserializedChuckNorrisResponse is null) {
				Logger.LogWarning($"Chuck Norris response could not be deserialized. The response was {serializedChuckNorrisResponse}");

				return "An error occurred. Please try again later.";
			}

			Logger.LogDebug($"Chuck Norris API deserialized response's value property is '{deserializedChuckNorrisResponse.value}'");

			var cowsayRequest = HttpClient.GetAsync($"{CowsayApiUrl}?format=text&message={HttpUtility.UrlEncode(deserializedChuckNorrisResponse.value)}").GetAwaiter().GetResult();

			if (!cowsayRequest.IsSuccessStatusCode) {
				Logger.LogWarning($"Cowsay API did not return a successful status code. Returned {cowsayRequest.StatusCode} with content {cowsayRequest.Content.ReadAsStringAsync().GetAwaiter().GetResult()}");

				return "An error occurred. Please try again later.";
			}

			return cowsayRequest.Content.ReadAsStringAsync().GetAwaiter().GetResult();
		}
	}
}