﻿using GuysNight.LethalCompanyMod.BalancedItems.Models;
using System.Collections.Generic;

namespace GuysNight.LethalCompanyMod.BalancedItems {
	public static class ItemOverridesContainer {
		public static Dictionary<string, OverrideProperties> ItemOverrides { get; } = new Dictionary<string, OverrideProperties> {
			//spawned scrap items
			{ "7Ball", new OverrideProperties(0.5f) },
			{ "Airhorn", new OverrideProperties(1.5f) },
			{ "Bell", new OverrideProperties(5f) },
			{ "BigBolt", new OverrideProperties(7f) },
			{ "BottleBin", new OverrideProperties() },
			{ "Brush", new OverrideProperties(1f) },
			{ "Candy", new OverrideProperties(1f) },
			{ "CashRegister", new OverrideProperties(20f) },
			{ "ChemicalJug", new OverrideProperties(20f) },
			{ "ClownHorn", new OverrideProperties(1f) },
			{ "Cog1", new OverrideProperties(25f) },
			{ "ComedyMask", new OverrideProperties(2f) },
			{ "Dentures", new OverrideProperties(1.5f) },
			{ "DiyFlashbang", new OverrideProperties(1.5f) },
			{ "DustPan", new OverrideProperties(1f) },
			{ "EggBeater", new OverrideProperties(0.5f) },
			{ "EnginePart1", new OverrideProperties(30f) },
			{ "FancyCup", new OverrideProperties(4f) },
			{ "FancyLamp", new OverrideProperties(10f) },
			{ "FancyPainting", new OverrideProperties() },
			{ "FishTestProp", new OverrideProperties(0.5f) },
			{ "FlashLaserPointer", new OverrideProperties(1f) },
			{ "Flask", new OverrideProperties(2f) },
			{ "GiftBox", new OverrideProperties() },
			{ "GoldBar", new OverrideProperties(27.5f) },
			{ "Hairdryer", new OverrideProperties() },
			{ "LungApparatus", new OverrideProperties(50f) },
			{ "MagnifyingGlass", new OverrideProperties(4f) },
			{ "MetalSheet", new OverrideProperties(7f) },
			{ "MoldPan", new OverrideProperties(3f) },
			{ "Mug", new OverrideProperties(2f) },
			{ "PerfumeBottle", new OverrideProperties(0.5f) },
			{ "Phone", new OverrideProperties() },
			{ "PickleJar", new OverrideProperties(3f) },
			{ "PillBottle", new OverrideProperties(1f) },
			{ "Remote", new OverrideProperties(1f) },
			{ "Ring", new OverrideProperties(0.2f) },
			{ "RobotToy", new OverrideProperties(5f) },
			{ "RubberDuck", new OverrideProperties(0.5f) },
			{ "SodaCanRed", new OverrideProperties(0.5f) },
			{ "SteeringWheel", new OverrideProperties(5f) },
			{ "StopSign", new OverrideProperties(6f) },
			{ "TeaKettle", new OverrideProperties(5f) },
			{ "Toothpaste", new OverrideProperties(1f) },
			{ "ToyCube", new OverrideProperties(1f) },
			{ "TragedyMask", new OverrideProperties(2f) },
			{ "WhoopieCushion", new OverrideProperties(0.5f) },
			{ "YieldSign", new OverrideProperties(6f) },

			//spawned non-scrap items
			{ "Clipboard", new OverrideProperties(0.1f) },
			{ "Key", new OverrideProperties(0.1f) },
			{ "Ragdoll", new OverrideProperties(100f) },
			{ "RedLocustHive", new OverrideProperties(0.5f) },
			{ "Shotgun", new OverrideProperties(15f) },
			{ "StickyNote", new OverrideProperties(0.1f) },

			//purchased equipment
			{ "Boombox", new OverrideProperties(10f) },
			{ "ExtensionLadder", new OverrideProperties(10f) },
			{ "Flashlight", new OverrideProperties(2f) },
			{ "Jetpack", new OverrideProperties() },
			{ "LockPicker", new OverrideProperties(10f) },
			{ "ProFlashlight", new OverrideProperties(2f) },
			{ "RadarBooster", new OverrideProperties() },
			{ "Shovel", new OverrideProperties(5f) },
			{ "SprayPaint", new OverrideProperties(1f) },
			{ "StunGrenade", new OverrideProperties(2f) },
			{ "TZPInhalant", new OverrideProperties(1f) },
			{ "WalkieTalkie", new OverrideProperties(2f) },
			{ "ZapGun", new OverrideProperties(7f) }
		};
	}
}