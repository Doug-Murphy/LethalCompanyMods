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
	}
}