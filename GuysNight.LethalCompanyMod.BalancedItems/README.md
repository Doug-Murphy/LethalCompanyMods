# Introduction

Balanced Items is a simple mod designed to give configurable, more realistic weights and values to all of the items in
Lethal Company. This mod includes item adjustments for all scrap/loot items as well as equipment from the terminal. This
includes all custom items that come from other mods you have added.

This mod also fixes the bug in the game where the current carry weight is being displayed as 5% greater than actual.

# Configuration

This mod introduces a unique approach to configuration file creation. Unlike most mods that generate their config file
upon the game's initial load, this mod dynamically creates its configuration as you play. Specifically, at the start of
each round, a list of all equipment and spawnable scrap items is compiled and corresponding config entries are
generated. This process encompasses all scrap on all moons and is completed before you even pull the lever on your ship
to land. Furthermore, the config values for some properties can be changed mid-game and be reflected when you start a
new session or start the next day.

If there is an existing override for an item's property specified in the mod, those values are set into the config
during creation. Otherwise, default (vanilla) values are used. Please note that any values you manually set in the
config file will always take precedence over both the vanilla values and any preset overrides within this mod.

## Weights

The weight of an item can be changed mid-game and be reflected at the start of the next day without restarting your
session.

For weight specification, simply input the desired weight of the item in pounds.

## Average Sell Value

The average sell value of an item can be changed mid-game and be reflected at the start of the next day without
restarting your session.

The average sell value you set serves as the basis for calculating the item's minimum and
maximum sell values, which will be +/- 20% of this average. For example, if you set the average sell value of an item to
be 100, then the item's value when spawned will be between 80-120.
> NOTE: Be aware that different moons in the game have different scrap multipliers, potentially resulting in actual
> in-game values being lower than your specified averages.

> NOTE: Regardless of your configuration, equipment will always be sold for 0 credits.

> NOTE: There are no preset overrides for item values in this mod, thus this mod simply narrows the variance between the
> minimum and maximum values compared to standard gameplay by default, ensuring balanced gameplay without creating
> overly easy or difficult scenarios.

## Spawn Rarities

The spawn rarity of an item on a moon can be changed mid-game but will not be reflected at the start of the next day.
You must restart your session (but not the game) for these changes to take effect.

The way that Lethal Company handles spawn rarities is by a relative weighting system. This makes it rather difficult to
understand the likelihood that your item will spawn. If you don't want an item to spawn on a specific moon at all, set
the item's rarity for that moon to be 0. The examples below should help understand this system.

> Example 1: You want only clown horns to spawn on Experimentation. In the config file for Experimentation, set all
> items except the clown horn to have a 0 rarity. The clown horn item can have any value except for 0. Since it is the
> only value set, it accounts for 100% of the spawn weighting and will account for 100% of the scrap spawned.

> Example 2: You want 1/3 of the generated scrap to be clown horns, 1/3 to be bottles, and the last 1/3
> to be candy on Assurance. Set the rarity for those 3 items to be the same value (you can just use 1 for simplicity)
> and all other items to 0.

> Example 3: You want 1/2 of the generated scrap to be clown horns, 1/4 to be bottles, and the last 1/4
> to be candy on Titan. Set the clown horn rarity to be 2, bottles to be 1, and candy to be 1. The sum of these values
> is 4. Since clown horns account for 2 of the 4, they account for half of the generated scrap. The other two account
> for only 1 of the 4, so they account for roughly a fourth of the generated scrap.

> NOTE: Given that scrap spawns are random, there is no guarantee that the values you set will be exactly what you have
> spawned in the game. The values you specify are merely the relative weighting of that item for that moon.

> NOTE: The Apparatus is an odd scrap item because it is not spawned in the same way that other scrap items are.
> "Regular" scrap items spawn randomly across the ground, but the Apparatus spawns as part of a room that is generated
> for the inside of the moon's building. Therefore, the apparatus spawn rate cannot be controlled.

# Adjustments

Below are the preset overrides set in this mod by default. Other properties are available to be overridden in your
config.

## Scrap

| Item Name            | Weight (Pounds) |
|----------------------|-----------------|
| Airhorn              | 1.5             |
| Apparatus            | 50              |
| Bee Hive             | 0.5             |
| Big Bolt             | 7               |
| Bottles              | Unchanged       |
| Brass Bell           | 5               |
| Candy                | 1               |
| Cash Register        | 20              |
| Chemical Jug         | 20              |
| Clown Horn           | 1               |
| Coffee Mug           | 2               |
| Comedy Mask          | 2               |
| Cookie Mold Pan      | 3               |
| DIY Flashbang        | 1.5             |
| Dust Pan             | 1               |
| Egg Beater           | 0.5             |
| Fancy Lamp           | 10              |
| Flask                | 2               |
| Gift Box             | Unchanged       |
| Gold Bar             | 27.5            |
| Golden Cup           | 4               |
| Hair Brush           | 1               |
| Hairdryer            | Unchanged       |
| Jar of Pickles       | 3               |
| Large axle           | 25              |
| Laser Pointer        | 1               |
| Magic 7 Ball         | 0.5             |
| Magnifying Glass     | 4               |
| Old Phone            | Unchanged       |
| Painting             | Unchanged       |
| Perfume Bottle       | 0.5             |
| Pill Bottle          | 1               |
| Plastic Fish         | 0.5             |
| Red Soda             | 0.5             |
| Remote               | 1               |
| Ring                 | 0.2             |
| Robot Toy            | 5               |
| Rubber Ducky         | 0.5             |
| Steering Wheel       | 5               |
| Stop Sign            | 6               |
| Tattered Metal Sheet | 7               |
| Tea Kettle           | 5               |
| Teeth                | 1.5             |
| Toothpaste           | 1               |
| Toy Cube             | 1               |
| Tragedy Mask         | 2               |
| V-Type Engine        | 30              |
| Whoopie Cushion      | 0.5             |
| Yield Sign           | 6               |

## Equipment

| Item Name        | Weight (Pounds) |
|------------------|-----------------|
| Boombox          | 10              |
| Extension Ladder | 10              |
| Flashlight       | 2               |
| Jetpack          | Unchanged       |
| Lockpicker       | 10              |
| Pro-Flashlight   | 2               |
| Radar-Booster    | Unchanged       |
| Shovel           | 5               |
| Spray Paint      | 1               |
| Stun Grenade     | 2               |
| TZP-Inhalant     | 1               |
| Walkie-talkie    | 2               |
| Zap Gun          | 7               |

## Miscellaneous

| Item Name     | Weight (Pounds) |
|---------------|-----------------|
| Clipboard     | 0.1             |
| Double-barrel | 15              |
| Key           | 0.1             |
| Player Body   | 100             |
| Sticky Note   | 0.1             |

# Issues / Requests

If you have any feature requests, questions, or encounter any issues like item weights not being changed correctly you
can [open an issue on GitHub](https://github.com/Doug-Murphy/LethalCompanyMods/issues).
Before opening an issue, please search to see if your request, question, or issue already has an open issue.
Alternatively, feel free to open up a PR to address your inquiry yourself. PRs are welcome!

## Known Issues / Roadmap / Oddities

1. Given how the game handles item weights and item values, the item weights _are not_ synced across players while the
   item values _are_ synced across players. Syncing the weights across players is a planned feature.