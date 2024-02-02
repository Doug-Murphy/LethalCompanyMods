using GuysNight.LethalCompanyMod.BalancedItems.Models;
using System.Collections.Generic;

namespace GuysNight.LethalCompanyMod.BalancedItems {
	public static class ItemsContainer {
		public static Dictionary<string, (VanillaValues VanillaValues, OverrideProperties Overrides)> Items { get; } = new Dictionary<string, (VanillaValues, OverrideProperties)> {
			//spawned scrap items
			{ "7Ball", (null, new OverrideProperties(0.5f)) },
			{ "Airhorn", (null, new OverrideProperties(1.5f)) },
			{ "Bell", (null, new OverrideProperties(5f)) },
			{ "BigBolt", (null, new OverrideProperties(7f)) },
			{ "BottleBin", (null, null) },
			{ "Brush", (null, new OverrideProperties(1f)) },
			{ "Candy", (null, new OverrideProperties(1f)) },
			{ "CashRegister", (null, new OverrideProperties(20f)) },
			{ "ChemicalJug", (null, new OverrideProperties(20f)) },
			{ "ClownHorn", (null, new OverrideProperties(1f)) },
			{ "Cog1", (null, new OverrideProperties(25f)) },
			{ "ComedyMask", (null, new OverrideProperties(2f)) },
			{ "Dentures", (null, new OverrideProperties(1.5f)) },
			{ "DiyFlashbang", (null, new OverrideProperties(1.5f)) },
			{ "DustPan", (null, new OverrideProperties(1f)) },
			{ "EggBeater", (null, new OverrideProperties(0.5f)) },
			{ "EnginePart1", (null, new OverrideProperties(30f)) },
			{ "FancyCup", (null, new OverrideProperties(4f)) },
			{ "FancyLamp", (null, new OverrideProperties(10f)) },
			{ "FancyPainting", (null, null) },
			{ "FishTestProp", (null, new OverrideProperties(0.5f)) },
			{ "FlashLaserPointer", (null, new OverrideProperties(1f)) },
			{ "Flask", (null, new OverrideProperties(2f)) },
			{ "GiftBox", (null, null) },
			{ "GoldBar", (null, new OverrideProperties(27.5f)) },
			{ "Hairdryer", (null, null) },
			{ "LungApparatus", (null, new OverrideProperties(50f)) },
			{ "MagnifyingGlass", (null, new OverrideProperties(4f)) },
			{ "MetalSheet", (null, new OverrideProperties(7f)) },
			{ "MoldPan", (null, new OverrideProperties(3f)) },
			{ "Mug", (null, new OverrideProperties(2f)) },
			{ "PerfumeBottle", (null, new OverrideProperties(0.5f)) },
			{ "Phone", (null, null) },
			{ "PickleJar", (null, new OverrideProperties(3f)) },
			{ "PillBottle", (null, new OverrideProperties(1f)) },
			{ "Remote", (null, new OverrideProperties(1f)) },
			{ "Ring", (null, new OverrideProperties(0.2f)) },
			{ "RobotToy", (null, new OverrideProperties(5f)) },
			{ "RubberDuck", (null, new OverrideProperties(0.5f)) },
			{ "SodaCanRed", (null, new OverrideProperties(0.5f)) },
			{ "SteeringWheel", (null, new OverrideProperties(5f)) },
			{ "StopSign", (null, new OverrideProperties(6f)) },
			{ "TeaKettle", (null, new OverrideProperties(5f)) },
			{ "Toothpaste", (null, new OverrideProperties(1f)) },
			{ "ToyCube", (null, new OverrideProperties(1f)) },
			{ "TragedyMask", (null, new OverrideProperties(2f)) },
			{ "WhoopieCushion", (null, new OverrideProperties(0.5f)) },
			{ "YieldSign", (null, new OverrideProperties(6f)) },

			//spawned non-scrap items
			{ "Clipboard", (null, new OverrideProperties(0.1f)) },
			{ "Key", (null, new OverrideProperties(0.1f)) },
			{ "Ragdoll", (null, new OverrideProperties(100f)) },
			{ "RedLocustHive", (null, new OverrideProperties(0.5f)) },
			{ "Shotgun", (null, new OverrideProperties(15f)) },
			{ "StickyNote", (null, new OverrideProperties(0.1f)) },

			//purchased equipment
			{ "Boombox", (null, new OverrideProperties(10f)) },
			{ "ExtensionLadder", (null, new OverrideProperties(10f)) },
			{ "Flashlight", (null, new OverrideProperties(2f)) },
			{ "Jetpack", (null, null) },
			{ "LockPicker", (null, new OverrideProperties(10f)) },
			{ "ProFlashlight", (null, new OverrideProperties(2f)) },
			{ "RadarBooster", (null, null) },
			{ "Shovel", (null, new OverrideProperties(5f)) },
			{ "SprayPaint", (null, new OverrideProperties(1f)) },
			{ "StunGrenade", (null, new OverrideProperties(2f)) },
			{ "TZPInhalant", (null, new OverrideProperties(1f)) },
			{ "WalkieTalkie", (null, new OverrideProperties(2f)) },
			{ "ZapGun", (null, new OverrideProperties(7f)) }
		};

		public static bool SetVanillaValues(string itemName, VanillaValues vanillaValues) {
			if (Items.TryGetValue(itemName, out var itemEntry)) {
				if (itemEntry.VanillaValues is null) {
					itemEntry.VanillaValues = vanillaValues;
					Items[itemName] = itemEntry;

					SharedComponents.Logger.LogDebug($"Vanilla values have been set for item '{itemName}' to be '{itemEntry.VanillaValues}'.");

					return true;
				}

				SharedComponents.Logger.LogDebug($"Vanilla values were already set for item '{itemName}' to be '{itemEntry.VanillaValues}'.");

				return false;
			}

			Items.Add(itemName, (vanillaValues, null));

			return true;
		}

		public static bool SetVanillaMoonRarityValues(string moonName, string itemName, byte rarity) {
			if (!Items.TryGetValue(itemName, out var itemEntry)) {
				//shouldn't be possible so long as this is called after the first pass of allItemsList
				return false;
			}

			itemEntry.VanillaValues.MoonRarities.TryAdd(moonName, rarity);

			Items[itemName] = itemEntry;

			SharedComponents.Logger.LogDebug($"Vanilla values have been set for item '{itemName}' to have rarity '{rarity}' on moon '{moonName}'.");

			return true;
		}
	}
}