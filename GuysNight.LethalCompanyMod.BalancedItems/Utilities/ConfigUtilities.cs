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

		public static void SyncConfigForItemOverrides(Item item, out OverrideProperties overrideProperties) {
			var itemWeight = item.weight;
			var itemMinValue = item.minValue;
			var itemMaxValue = item.maxValue;
			var itemAverageValue = (ushort)Math.Round(new[] { itemMinValue, itemMaxValue }.Average(), MidpointRounding.AwayFromZero);

			//if the overrides container does not contain an override for the current item, add an entry
			if (!ItemOverridesContainer.ItemOverrides.ContainsKey(item.name)) {
				ItemOverridesContainer.ItemOverrides[item.name] = new OverrideProperties();
			}

			var itemOverrides = ItemOverridesContainer.ItemOverrides[item.name];

			SharedComponents.ConfigFile.Reload();
			//if weight is not added in the config, add it for future
			//if weight is added in the config, retrieve the value and set it in the overrides
			itemOverrides.Weight = SharedComponents.ConfigFile.Bind(SanitizeConfigEntry(Constants.ConfigSectionHeaderWeight),
				SanitizeConfigEntry(item.name),
				NumericUtilities.DenormalizeWeight(Math.Abs(itemOverrides.Weight - default(float)) > 0 ? itemOverrides.Weight : itemWeight),
				new ConfigDescription(string.Format(Constants.ConfigDescriptionWeight, item.itemName), new AcceptableValueRange<float>(0, 1_000))
			).Value;

			//if sell value is not added in the config, add it for future
			//if sell value is added in the config, retrieve the value and set it in the overrides
			itemOverrides.AverageValue = SharedComponents.ConfigFile.Bind(SanitizeConfigEntry(Constants.ConfigSectionHeaderAverageSellValues),
				SanitizeConfigEntry(item.name),
				itemOverrides.AverageValue != default ? itemOverrides.AverageValue : itemAverageValue,
				new ConfigDescription(string.Format(Constants.ConfigDescriptionAverageSellValues, item.itemName), new AcceptableValueRange<ushort>(ushort.MinValue, ushort.MaxValue))
			).Value;

			overrideProperties = itemOverrides;

			SharedComponents.Logger.LogInfo($"Finish adding config entries and setting override values for '{item.name}' to have " +
			                                $"average sell value = '{itemOverrides.AverageValue}', " +
			                                $"weight = '{NumericUtilities.DenormalizeWeight(itemOverrides.Weight)}'");
		}

		public static void SyncConfigForItemRarityOverride(SelectableLevel level, SpawnableItemWithRarity spawnableItemWithRarity, out OverrideProperties itemOverride) {
			var item = spawnableItemWithRarity.spawnableItem;
			var itemRarity = spawnableItemWithRarity.rarity;

			//if the overrides container does not contain an override for the current item, add an entry
			ItemOverridesContainer.ItemOverrides.TryAdd(item.name, new OverrideProperties());

			var itemOverrides = ItemOverridesContainer.ItemOverrides[item.name];
			itemOverrides.MoonRarities.TryAdd(level.name, null);

			SharedComponents.ConfigFile.Reload();
			//if rarity for the moon is not added in the config, add it for future
			//if it is added in the config, retrieve the value and set it in the overrides
			itemOverrides.MoonRarities[level.name] = SharedComponents.ConfigFile.Bind(SanitizeConfigEntry(string.Format(Constants.ConfigSectionHeaderMoonRarity, level.PlanetName)),
				SanitizeConfigEntry(item.name),
				itemOverrides.MoonRarities[level.name].HasValue ? itemOverrides.MoonRarities[level.name].Value : (byte)itemRarity,
				new ConfigDescription(string.Format(Constants.ConfigDescriptionMoonRarity, item.itemName), new AcceptableValueRange<byte>(0, 100)) //100 is the max in the game
			).Value;

			itemOverride = itemOverrides;

			SharedComponents.Logger.LogInfo($"Finish adding config entry and setting override value for '{item.name}' to have rarity = '{itemOverrides.MoonRarities[level.name]}' on moon '{level.name}'");
		}
	}
}