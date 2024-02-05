using GuysNight.LethalCompanyMod.BalancedItems.Models;
using System.Collections.Generic;

namespace GuysNight.LethalCompanyMod.BalancedItems {
	public static class ItemsContainer {
		public static Dictionary<string, ItemProperties> Items { get; } = new Dictionary<string, ItemProperties> {
			//spawned scrap items
			{ "7Ball", new ItemProperties(new OverrideProperties(0.5f)) },
			{ "Airhorn", new ItemProperties(new OverrideProperties(1.5f)) },
			{ "Bell", new ItemProperties(new OverrideProperties(5f)) },
			{ "BigBolt", new ItemProperties(new OverrideProperties(7f)) },
			{ "BottleBin", new ItemProperties() },
			{ "Brush", new ItemProperties(new OverrideProperties(1f)) },
			{ "Candy", new ItemProperties(new OverrideProperties(1f)) },
			{ "CashRegister", new ItemProperties(new OverrideProperties(20f)) },
			{ "ChemicalJug", new ItemProperties(new OverrideProperties(20f)) },
			{ "ClownHorn", new ItemProperties(new OverrideProperties(1f)) },
			{ "Cog1", new ItemProperties(new OverrideProperties(25f)) },
			{ "ComedyMask", new ItemProperties(new OverrideProperties(2f)) },
			{ "Dentures", new ItemProperties(new OverrideProperties(1.5f)) },
			{ "DiyFlashbang", new ItemProperties(new OverrideProperties(1.5f)) },
			{ "DustPan", new ItemProperties(new OverrideProperties(1f)) },
			{ "EggBeater", new ItemProperties(new OverrideProperties(0.5f)) },
			{ "EnginePart1", new ItemProperties(new OverrideProperties(30f)) },
			{ "FancyCup", new ItemProperties(new OverrideProperties(4f)) },
			{ "FancyLamp", new ItemProperties(new OverrideProperties(10f)) },
			{ "FancyPainting", new ItemProperties() },
			{ "FishTestProp", new ItemProperties(new OverrideProperties(0.5f)) },
			{ "FlashLaserPointer", new ItemProperties(new OverrideProperties(1f)) },
			{ "Flask", new ItemProperties(new OverrideProperties(2f)) },
			{ "GiftBox", new ItemProperties() },
			{ "GoldBar", new ItemProperties(new OverrideProperties(27.5f)) },
			{ "Hairdryer", new ItemProperties() },
			{ "LungApparatus", new ItemProperties(new OverrideProperties(50f)) },
			{ "MagnifyingGlass", new ItemProperties(new OverrideProperties(4f)) },
			{ "MetalSheet", new ItemProperties(new OverrideProperties(7f)) },
			{ "MoldPan", new ItemProperties(new OverrideProperties(3f)) },
			{ "Mug", new ItemProperties(new OverrideProperties(2f)) },
			{ "PerfumeBottle", new ItemProperties(new OverrideProperties(0.5f)) },
			{ "Phone", new ItemProperties() },
			{ "PickleJar", new ItemProperties(new OverrideProperties(3f)) },
			{ "PillBottle", new ItemProperties(new OverrideProperties(1f)) },
			{ "Remote", new ItemProperties(new OverrideProperties(1f)) },
			{ "Ring", new ItemProperties(new OverrideProperties(0.2f)) },
			{ "RobotToy", new ItemProperties(new OverrideProperties(5f)) },
			{ "RubberDuck", new ItemProperties(new OverrideProperties(0.5f)) },
			{ "SodaCanRed", new ItemProperties(new OverrideProperties(0.5f)) },
			{ "SteeringWheel", new ItemProperties(new OverrideProperties(5f)) },
			{ "StopSign", new ItemProperties(new OverrideProperties(6f)) },
			{ "TeaKettle", new ItemProperties(new OverrideProperties(5f)) },
			{ "Toothpaste", new ItemProperties(new OverrideProperties(1f)) },
			{ "ToyCube", new ItemProperties(new OverrideProperties(1f)) },
			{ "TragedyMask", new ItemProperties(new OverrideProperties(2f)) },
			{ "WhoopieCushion", new ItemProperties(new OverrideProperties(0.5f)) },
			{ "YieldSign", new ItemProperties(new OverrideProperties(6f)) },

			//spawned non-scrap items
			{ "Clipboard", new ItemProperties(new OverrideProperties(0.1f)) },
			{ "Key", new ItemProperties(new OverrideProperties(0.1f)) },
			{ "Ragdoll", new ItemProperties(new OverrideProperties(100f)) },
			{ "RedLocustHive", new ItemProperties(new OverrideProperties(0.5f)) },
			{ "Shotgun", new ItemProperties(new OverrideProperties(15f)) },
			{ "StickyNote", new ItemProperties(new OverrideProperties(0.1f)) },

			//purchased equipment
			{ "Boombox", new ItemProperties(new OverrideProperties(10f)) },
			{ "ExtensionLadder", new ItemProperties(new OverrideProperties(10f)) },
			{ "Flashlight", new ItemProperties(new OverrideProperties(2f)) },
			{ "Jetpack", new ItemProperties() },
			{ "LockPicker", new ItemProperties(new OverrideProperties(10f)) },
			{ "ProFlashlight", new ItemProperties(new OverrideProperties(2f)) },
			{ "RadarBooster", new ItemProperties() },
			{ "Shovel", new ItemProperties(new OverrideProperties(5f)) },
			{ "SprayPaint", new ItemProperties(new OverrideProperties(1f)) },
			{ "StunGrenade", new ItemProperties(new OverrideProperties(2f)) },
			{ "TZPInhalant", new ItemProperties(new OverrideProperties(1f)) },
			{ "WalkieTalkie", new ItemProperties(new OverrideProperties(2f)) },
			{ "ZapGun", new ItemProperties(new OverrideProperties(7f)) }
		};

		public static bool SetVanillaValuesForItem(string itemName, VanillaValues vanillaValues) {
			if (Items.TryGetValue(itemName, out var itemEntry)) {
				if (itemEntry.VanillaValues is null) {
					itemEntry.VanillaValues = vanillaValues;
					Items[itemName] = itemEntry;

					SharedComponents.Logger.LogDebug($"Vanilla values have been set for item '{itemName}' to be '{vanillaValues}'.");

					return true;
				}

				SharedComponents.Logger.LogDebug($"Vanilla values were already set for item '{itemName}' to be '{itemEntry.VanillaValues}'.");

				return false;
			}

			Items.Add(itemName, new ItemProperties(vanillaValues));
			SharedComponents.Logger.LogDebug($"Created new items container entry and set vanilla values for item '{itemName}' to be '{vanillaValues}'.");

			return true;
		}
	}
}