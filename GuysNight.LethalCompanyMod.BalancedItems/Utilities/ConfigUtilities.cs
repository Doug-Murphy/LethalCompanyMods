using GuysNight.LethalCompanyMod.BalancedItems.Models;
using System;
using System.Linq;

namespace GuysNight.LethalCompanyMod.BalancedItems.Utilities {
	public static class ConfigUtilities {
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
			itemOverrides.Weight = SharedComponents.ConfigFile.Bind(Constants.ConfigSectionHeaderWeight,
				item.name,
				NumericUtilities.DenormalizeWeight(Math.Abs(itemOverrides.Weight - default(float)) > 0 ? itemOverrides.Weight : itemWeight),
				string.Format(Constants.ConfigDescriptionWeight, item.itemName)
			).Value;

			//if sell value is not added in the config, add it for future
			//if sell value is added in the config, retrieve the value and set it in the overrides
			itemOverrides.AverageValue = SharedComponents.ConfigFile.Bind(Constants.ConfigSectionHeaderAverageSellValues,
				item.name,
				itemOverrides.AverageValue != default ? itemOverrides.AverageValue : itemAverageValue,
				string.Format(Constants.ConfigDescriptionAverageSellValues, item.itemName)).Value;

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
			itemOverrides.MoonRarities[level.name] = SharedComponents.ConfigFile.Bind(string.Format(Constants.ConfigSectionHeaderMoonRarity, level.PlanetName),
				item.name,
				itemOverrides.MoonRarities[level.name].HasValue ? itemOverrides.MoonRarities[level.name].Value : itemRarity,
				string.Format(Constants.ConfigDescriptionMoonRarity, item.itemName)
			).Value;

			itemOverride = itemOverrides;

			SharedComponents.Logger.LogInfo($"Finish adding config entry and setting override value for '{item.name}' to have rarity = '{itemOverrides.MoonRarities[level.name]}' on moon '{level.name}'");
		}
	}
}