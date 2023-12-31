﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using GuysNight.LethalCompanyMod.BalancedItems.Models;

namespace GuysNight.LethalCompanyMod.BalancedItems.Extensions {
	public static class DictionaryExtensions {
		public static string ToDebugString(this Dictionary<string, ItemPropertyOverride> sourceDictionary) {
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("Item overrides specified:");
			
			foreach (var (itemName, itemOverride) in sourceDictionary.OrderBy(itemOverride => itemOverride.Key)) {
				stringBuilder.AppendLine($"Name: {itemName} -> {itemOverride}");
			}

			return stringBuilder.ToString();
		}
	}
}