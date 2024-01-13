using BepInEx.Configuration;
using GuysNight.LethalCompanyMod.BalancedItems.Models;
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
		private static string SanitizeConfigEntry(string rawEntry) {
			//replace invalid characters with '_'
			return InvalidConfigCharsRegex.Replace(rawEntry, "_");
		}

		public static (VanillaValues VanillaValues, OverrideProperties Overrides) SyncConfigForItemOverrides(Item gameItem) {
			//if the items container does not contain an entry for the current item, add one
			ItemsContainer.Items.TryAdd(gameItem.name, (null, null));

			var itemEntry = ItemsContainer.Items[gameItem.name];
			itemEntry.VanillaValues ??= new VanillaValues(gameItem.minValue, gameItem.maxValue, gameItem.weight);
			itemEntry.Overrides ??= new OverrideProperties();

			SharedComponents.ConfigFile.Reload();
			//if weight is not added in the config, add it for future
			//if weight is added in the config, retrieve the value and set it in the overrides
			itemEntry.Overrides.Weight = SharedComponents.ConfigFile.Bind(SanitizeConfigEntry(Constants.ConfigSectionHeaderWeight),
				SanitizeConfigEntry(gameItem.name),
				NumericUtilities.DenormalizeWeight(Math.Abs(itemEntry.Overrides.Weight - default(float)) > 0 ? itemEntry.Overrides.Weight : itemEntry.VanillaValues.Weight),
				new ConfigDescription(string.Format(Constants.ConfigDescriptionWeight, gameItem.itemName, NumericUtilities.DenormalizeWeight(itemEntry.VanillaValues.Weight)), new AcceptableValueRange<float>(0, 1_000))
			).Value;

			var gameItemCalculatedAverageValue = (ushort)Math.Round(new[] { gameItem.minValue, gameItem.maxValue }.Average(), MidpointRounding.AwayFromZero);
			//if sell value is not added in the config, add it for future
			//if sell value is added in the config, retrieve the value and set it in the overrides
			itemEntry.Overrides.AverageValue = SharedComponents.ConfigFile.Bind(SanitizeConfigEntry(Constants.ConfigSectionHeaderAverageSellValues),
				SanitizeConfigEntry(gameItem.name),
				itemEntry.Overrides.AverageValue != default ? itemEntry.Overrides.AverageValue : gameItemCalculatedAverageValue,
				new ConfigDescription(string.Format(Constants.ConfigDescriptionAverageSellValues, gameItem.itemName, gameItemCalculatedAverageValue), new AcceptableValueRange<ushort>(ushort.MinValue, ushort.MaxValue))
			).Value;

			SharedComponents.Logger.LogInfo($"Finish adding config entries and setting override values for '{gameItem.name}' to have " +
			                                $"average sell value = '{itemEntry.Overrides.AverageValue}', " +
			                                $"weight = '{NumericUtilities.DenormalizeWeight(itemEntry.Overrides.Weight)}'");

			ItemsContainer.Items[gameItem.name] = itemEntry;

			return (itemEntry.VanillaValues, itemEntry.Overrides);
		}

		public static (VanillaValues VanillaValues, OverrideProperties Overrides) SyncConfigForItemRarityOverride(SelectableLevel level, SpawnableItemWithRarity spawnableItemWithRarity) {
			var gameItem = spawnableItemWithRarity.spawnableItem;
			var gameItemRarity = spawnableItemWithRarity.rarity;

			//if the items container does not contain an entry for the current item, add one
			ItemsContainer.Items.TryAdd(gameItem.name, (null, null));

			var itemEntry = ItemsContainer.Items[gameItem.name];
			itemEntry.VanillaValues ??= new VanillaValues(gameItem.minValue, gameItem.maxValue, gameItem.weight);
			itemEntry.Overrides ??= new OverrideProperties();
			itemEntry.VanillaValues.MoonRarities.TryAdd(level.name, (byte)gameItemRarity);
			SharedComponents.Logger.LogDebug($"Set itemEntry.VanillaValues.MoonRarities for item '{gameItem.name}' on level '{level.name}' to be '{gameItemRarity}'");
			itemEntry.Overrides.MoonRarities.TryAdd(level.name, null);

			SharedComponents.ConfigFile.Reload();
			//if rarity for the moon is not added in the config, add it for future
			//if it is added in the config, retrieve the value and set it in the overrides
			itemEntry.Overrides.MoonRarities[level.name] = SharedComponents.ConfigFile.Bind(SanitizeConfigEntry(string.Format(Constants.ConfigSectionHeaderMoonRarity, level.PlanetName)),
				SanitizeConfigEntry(gameItem.name),
				itemEntry.Overrides.MoonRarities[level.name].HasValue ? itemEntry.Overrides.MoonRarities[level.name].Value : itemEntry.VanillaValues.MoonRarities[level.name],
				new ConfigDescription(string.Format(Constants.ConfigDescriptionMoonRarity, gameItem.itemName, gameItemRarity), new AcceptableValueRange<byte>(0, 100)) //100 is the max in the game
			).Value;

			SharedComponents.Logger.LogInfo($"Finish adding config entry and setting override value for '{gameItem.name}' to have rarity = '{itemEntry.Overrides.MoonRarities[level.name]}' on moon '{level.name}'");

			ItemsContainer.Items[gameItem.name] = itemEntry;

			return (itemEntry.VanillaValues, itemEntry.Overrides);
		}
	}
}