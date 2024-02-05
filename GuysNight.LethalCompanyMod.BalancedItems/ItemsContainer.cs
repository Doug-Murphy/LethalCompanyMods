using GuysNight.LethalCompanyMod.BalancedItems.Models.Items;
using System.Collections.Generic;

namespace GuysNight.LethalCompanyMod.BalancedItems {
	internal static class ItemsContainer {
		internal static Dictionary<string, ItemProperties> Items { get; } = new Dictionary<string, ItemProperties> {
			//spawned scrap items
			{ "7Ball", new ItemProperties(new OverrideItemValues(0.5f)) },
			{ "Airhorn", new ItemProperties(new OverrideItemValues(1.5f)) },
			{ "Bell", new ItemProperties(new OverrideItemValues(5f)) },
			{ "BigBolt", new ItemProperties(new OverrideItemValues(7f)) },
			{ "BottleBin", new ItemProperties() },
			{ "Brush", new ItemProperties(new OverrideItemValues(1f)) },
			{ "Candy", new ItemProperties(new OverrideItemValues(1f)) },
			{ "CashRegister", new ItemProperties(new OverrideItemValues(20f)) },
			{ "ChemicalJug", new ItemProperties(new OverrideItemValues(20f)) },
			{ "ClownHorn", new ItemProperties(new OverrideItemValues(1f)) },
			{ "Cog1", new ItemProperties(new OverrideItemValues(25f)) },
			{ "ComedyMask", new ItemProperties(new OverrideItemValues(2f)) },
			{ "Dentures", new ItemProperties(new OverrideItemValues(1.5f)) },
			{ "DiyFlashbang", new ItemProperties(new OverrideItemValues(1.5f)) },
			{ "DustPan", new ItemProperties(new OverrideItemValues(1f)) },
			{ "EggBeater", new ItemProperties(new OverrideItemValues(0.5f)) },
			{ "EnginePart1", new ItemProperties(new OverrideItemValues(30f)) },
			{ "FancyCup", new ItemProperties(new OverrideItemValues(4f)) },
			{ "FancyLamp", new ItemProperties(new OverrideItemValues(10f)) },
			{ "FancyPainting", new ItemProperties() },
			{ "FishTestProp", new ItemProperties(new OverrideItemValues(0.5f)) },
			{ "FlashLaserPointer", new ItemProperties(new OverrideItemValues(1f)) },
			{ "Flask", new ItemProperties(new OverrideItemValues(2f)) },
			{ "GiftBox", new ItemProperties() },
			{ "GoldBar", new ItemProperties(new OverrideItemValues(27.5f)) },
			{ "Hairdryer", new ItemProperties() },
			{ "LungApparatus", new ItemProperties(new OverrideItemValues(50f)) },
			{ "MagnifyingGlass", new ItemProperties(new OverrideItemValues(4f)) },
			{ "MetalSheet", new ItemProperties(new OverrideItemValues(7f)) },
			{ "MoldPan", new ItemProperties(new OverrideItemValues(3f)) },
			{ "Mug", new ItemProperties(new OverrideItemValues(2f)) },
			{ "PerfumeBottle", new ItemProperties(new OverrideItemValues(0.5f)) },
			{ "Phone", new ItemProperties() },
			{ "PickleJar", new ItemProperties(new OverrideItemValues(3f)) },
			{ "PillBottle", new ItemProperties(new OverrideItemValues(1f)) },
			{ "Remote", new ItemProperties(new OverrideItemValues(1f)) },
			{ "Ring", new ItemProperties(new OverrideItemValues(0.2f)) },
			{ "RobotToy", new ItemProperties(new OverrideItemValues(5f)) },
			{ "RubberDuck", new ItemProperties(new OverrideItemValues(0.5f)) },
			{ "SodaCanRed", new ItemProperties(new OverrideItemValues(0.5f)) },
			{ "SteeringWheel", new ItemProperties(new OverrideItemValues(5f)) },
			{ "StopSign", new ItemProperties(new OverrideItemValues(6f)) },
			{ "TeaKettle", new ItemProperties(new OverrideItemValues(5f)) },
			{ "Toothpaste", new ItemProperties(new OverrideItemValues(1f)) },
			{ "ToyCube", new ItemProperties(new OverrideItemValues(1f)) },
			{ "TragedyMask", new ItemProperties(new OverrideItemValues(2f)) },
			{ "WhoopieCushion", new ItemProperties(new OverrideItemValues(0.5f)) },
			{ "YieldSign", new ItemProperties(new OverrideItemValues(6f)) },

			//spawned non-scrap items
			{ "Clipboard", new ItemProperties(new OverrideItemValues(0.1f)) },
			{ "Key", new ItemProperties(new OverrideItemValues(0.1f)) },
			{ "Ragdoll", new ItemProperties(new OverrideItemValues(100f)) },
			{ "RedLocustHive", new ItemProperties(new OverrideItemValues(0.5f)) },
			{ "Shotgun", new ItemProperties(new OverrideItemValues(15f)) },
			{ "StickyNote", new ItemProperties(new OverrideItemValues(0.1f)) },

			//purchased equipment
			{ "Boombox", new ItemProperties(new OverrideItemValues(10f)) },
			{ "ExtensionLadder", new ItemProperties(new OverrideItemValues(10f)) },
			{ "Flashlight", new ItemProperties(new OverrideItemValues(2f)) },
			{ "Jetpack", new ItemProperties() },
			{ "LockPicker", new ItemProperties(new OverrideItemValues(10f)) },
			{ "ProFlashlight", new ItemProperties(new OverrideItemValues(2f)) },
			{ "RadarBooster", new ItemProperties() },
			{ "Shovel", new ItemProperties(new OverrideItemValues(5f)) },
			{ "SprayPaint", new ItemProperties(new OverrideItemValues(1f)) },
			{ "StunGrenade", new ItemProperties(new OverrideItemValues(2f)) },
			{ "TZPInhalant", new ItemProperties(new OverrideItemValues(1f)) },
			{ "WalkieTalkie", new ItemProperties(new OverrideItemValues(2f)) },
			{ "ZapGun", new ItemProperties(new OverrideItemValues(7f)) }
		};

		internal static bool SetVanillaValuesForItem(string itemName, VanillaItemValues vanillaItemValues) {
			if (Items.TryGetValue(itemName, out var itemEntry)) {
				if (itemEntry.VanillaItemValues is null) {
					itemEntry.VanillaItemValues = vanillaItemValues;
					Items[itemName] = itemEntry;

					SharedComponents.Logger.LogDebug($"Vanilla values have been set for item '{itemName}' to be '{vanillaItemValues}'.");

					return true;
				}

				SharedComponents.Logger.LogDebug($"Vanilla values were already set for item '{itemName}' to be '{itemEntry.VanillaItemValues}'.");

				return false;
			}

			Items.Add(itemName, new ItemProperties(vanillaItemValues));
			SharedComponents.Logger.LogDebug($"Created new items container entry and set vanilla values for item '{itemName}' to be '{vanillaItemValues}'.");

			return true;
		}
	}
}