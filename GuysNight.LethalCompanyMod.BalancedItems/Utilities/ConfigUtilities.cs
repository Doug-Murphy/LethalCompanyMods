using BepInEx.Configuration;
using GuysNight.LethalCompanyMod.BalancedItems.Models.Items;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace GuysNight.LethalCompanyMod.BalancedItems.Utilities {
	public static class ConfigUtilities {
		/// <summary>
		/// The list of invalid characters for BepInEx configs. Taken from their decompiled DLL.
		/// </summary>
		private static readonly char[] InvalidConfigChars = {
			'=',
			'\n',
			'\t',
			'\\',
			'"',
			'\'',
			'[',
			']'
		};

		//create a regex pattern from the invalid characters. Manually escape ] since it apparently isn't considered special in .NET Standard 2.1.
		private static readonly string Pattern = "[" + Regex.Escape(new string(InvalidConfigChars)).Replace("]", "\\]") + "]";

		//use that pattern to compile a Regex replacement.
		private static readonly Regex InvalidConfigCharsRegex = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

		/// <summary>
		/// Sanitize the config entry so that it is allowed by BepInEx
		/// </summary>
		/// <param name="rawEntry">The entry to sanitize.</param>
		/// <returns></returns>
		public static string SanitizeConfigEntry(string rawEntry) {
			//replace invalid characters with '_'
			return InvalidConfigCharsRegex.Replace(rawEntry, "_");
		}

		internal static ItemProperties SyncConfigForItemOverrides(Item gameItem) {
			var itemEntry = ItemsContainer.Items[gameItem.name];

			//in case the current item was not in the allItemsList or otherwise not set during initialization, assume the current values from the game are vanilla and set them
			itemEntry.VanillaItemValues ??= new VanillaItemValues(gameItem.minValue, gameItem.maxValue, gameItem.weight);

			//if weight is not added in the config, add it for future
			//if weight is added in the config, retrieve the value and set it in the overrides
			itemEntry.OverrideItemValues.Weight = SharedComponents.ConfigFile.Bind(SanitizeConfigEntry(Constants.ConfigSectionHeaderWeight),
				SanitizeConfigEntry(gameItem.name),
				NumericUtilities.DenormalizeWeight(Math.Abs(itemEntry.OverrideItemValues.Weight - default(float)) > 0 ? itemEntry.OverrideItemValues.Weight : itemEntry.VanillaItemValues.Weight),
				new ConfigDescription(string.Format(Constants.ConfigDescriptionWeight, gameItem.itemName, NumericUtilities.DenormalizeWeight(itemEntry.VanillaItemValues.Weight)), new AcceptableValueRange<float>(0, 1_000))
			).Value;

			var gameItemCalculatedAverageValue = (ushort)Math.Round(new[] { gameItem.minValue, gameItem.maxValue }.Average(), MidpointRounding.AwayFromZero);
			//if sell value is not added in the config, add it for future
			//if sell value is added in the config, retrieve the value and set it in the overrides
			itemEntry.OverrideItemValues.AverageValue = SharedComponents.ConfigFile.Bind(SanitizeConfigEntry(Constants.ConfigSectionHeaderAverageSellValues),
				SanitizeConfigEntry(gameItem.name),
				itemEntry.OverrideItemValues.AverageValue != default ? itemEntry.OverrideItemValues.AverageValue : gameItemCalculatedAverageValue,
				new ConfigDescription(string.Format(Constants.ConfigDescriptionAverageSellValues, gameItem.itemName, gameItemCalculatedAverageValue), new AcceptableValueRange<ushort>(ushort.MinValue, ushort.MaxValue))
			).Value;

			SharedComponents.Logger.LogDebug($"Finish adding config entries and setting override values for '{gameItem.name}' to have " +
			                                 $"average sell value = '{itemEntry.OverrideItemValues.AverageValue}', " +
			                                 $"weight = '{NumericUtilities.DenormalizeWeight(itemEntry.OverrideItemValues.Weight)}'");

			ItemsContainer.Items[gameItem.name] = itemEntry;

			return new ItemProperties(itemEntry.VanillaItemValues, itemEntry.OverrideItemValues);
		}

		internal static int SyncConfigForMoonItemRarity(SelectableLevel moon, SpawnableItemWithRarity gameItemWithRarity) {
			var configSectionHeader = SanitizeConfigEntry(string.Format(Constants.ConfigSectionHeaderMoonRarity, moon.PlanetName));
			var configKeyName = SanitizeConfigEntry(gameItemWithRarity.spawnableItem.name);

			if (SharedComponents.ConfigFile.TryGetEntry<int>(configSectionHeader, configKeyName, out var moonRarityFromConfig)) {
				//config contains an entry for the moon rarity. just return that
				return moonRarityFromConfig.Value;
			}

			var moonRarityValue = gameItemWithRarity.rarity;

			//config did not contain an entry, so let's set one. This also means it is safe to assume that what is in gameItemWithRarity is the vanilla values.
			//since an entry did not exist, and this mod provides no preset overrides for moon rarity, we can just set this to be the default vanilla value.
			SharedComponents.ConfigFile.Bind(configSectionHeader,
				configKeyName,
				moonRarityValue,
				new ConfigDescription(string.Format(Constants.ConfigDescriptionMoonRarity, gameItemWithRarity.spawnableItem.itemName, moonRarityValue), new AcceptableValueRange<int>(0, 100))
			);

			SharedComponents.Logger.LogDebug($"Finish adding config entry for '{gameItemWithRarity.spawnableItem.name}' to have rarity '{moonRarityValue}' on moon '{moon.PlanetName}'");

			return moonRarityValue;
		}

		internal static int? GetItemRarityForMoon(SelectableLevel moon, SpawnableItemWithRarity gameItemWithRarity) {
			var configSectionHeader = SanitizeConfigEntry(string.Format(Constants.ConfigSectionHeaderMoonRarity, moon.PlanetName));
			var configKeyName = SanitizeConfigEntry(gameItemWithRarity.spawnableItem.name);

			if (SharedComponents.ConfigFile.TryGetEntry<int>(configSectionHeader, configKeyName, out var moonRarityFromConfig)) {
				//config contains an entry for the moon rarity. just return that
				return moonRarityFromConfig.Value;
			}

			return null;
		}
	}
}