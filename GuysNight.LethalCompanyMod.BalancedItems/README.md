# Introduction

Balanced Items is a simple mod designed to give configurable, more realistic weights and values to all of the items in
Lethal Company. This mod includes item adjustments for all scrap/loot items as well as equipment from the terminal.

# Configuration

This mod introduces a unique approach to configuration file creation. Unlike most mods that generate their config file
upon the game's initial load, this mod dynamically creates its configuration as you play. Specifically, at the start of
each round, the mod compiles a list of all equipment and spawnable scrap items and generates corresponding config
entries. This process encompasses all scrap on all moons and is completed before your ship lands. Furthermore, the
config values can be changed mid-game and be reflected when you start a new session or start the next day.

If there's an existing override for the item's weight or average sell value specified in the mod, these values are set
into the config. Otherwise, default (vanilla) values are used. Please note that any values you manually set in the
config file will always take precedence over both the vanilla values and any preset overrides within this mod.

## Weights

For weight specification, simply input the desired weight of the item in pounds.
> NOTE: Due to some peculiarities in the game's mechanics, item weights may sometimes be 1-2 pounds heavier than the
> specified value.

## Average Sell Value

The average sell value you set serves as the basis for calculating the item's minimum and maximum sell values, which are
determined to be +/- 20% of this average.
> NOTE: Regardless of your configuration, the game will always assign a sale value of 0 credits to equipment.

> NOTE: This approach narrows the variance between the minimum and maximum values compared to standard gameplay,
> ensuring balanced gameplay without overly easy or difficult scenarios. Be aware that different moons in the game have
> varying scrap multipliers, potentially resulting in actual in-game values being lower than your specified averages.

# Adjustments

## Scrap

| Item Name            | Weight (Pounds) | Value     |
|----------------------|-----------------|-----------|
| Airhorn              | 1.5             | Unchanged |
| Apparatus            | 50              | Unchanged |
| Bee Hive             | 0.5             | Unchanged |
| Big Bolt             | 7               | Unchanged |
| Bottles              | Unchanged       | Unchanged |
| Brass Bell           | 5               | Unchanged |
| Candy                | 1               | Unchanged |
| Cash Register        | 20              | Unchanged |
| Chemical Jug         | 20              | Unchanged |
| Clown Horn           | 1               | Unchanged |
| Coffee Mug           | 2               | Unchanged |
| Comedy Mask          | 2               | Unchanged |
| Cookie Mold Pan      | 3               | Unchanged |
| DIY Flashbang        | 1.5             | Unchanged |
| Dust Pan             | 1               | Unchanged |
| Egg Beater           | 0.5             | Unchanged |
| Fancy Lamp           | 10              | Unchanged |
| Flask                | 2               | Unchanged |
| Gift Box             | Unchanged       | Unchanged |
| Gold Bar             | 27.5            | Unchanged |
| Golden Cup           | 4               | Unchanged |
| Hair Brush           | 1               | Unchanged |
| Hairdryer            | Unchanged       | Unchanged |
| Jar of Pickles       | 3               | Unchanged |
| Large axle           | 25              | Unchanged |
| Laser Pointer        | 1               | Unchanged |
| Magic 7 Ball         | 0.5             | Unchanged |
| Magnifying Glass     | 4               | Unchanged |
| Old Phone            | Unchanged       | Unchanged |
| Painting             | Unchanged       | Unchanged |
| Perfume Bottle       | 0.5             | Unchanged |
| Pill Bottle          | 1               | Unchanged |
| Plastic Fish         | 0.5             | Unchanged |
| Red Soda             | 0.5             | Unchanged |
| Remote               | 1               | Unchanged |
| Ring                 | 0.2             | Unchanged |
| Robot Toy            | 5               | Unchanged |
| Rubber Ducky         | 0.5             | Unchanged |
| Steering Wheel       | 5               | Unchanged |
| Stop Sign            | 6               | Unchanged |
| Tattered Metal Sheet | 7               | Unchanged |
| Tea Kettle           | 5               | Unchanged |
| Teeth                | 1.5             | Unchanged |
| Toothpaste           | 1               | Unchanged |
| Toy Cube             | 1               | Unchanged |
| Tragedy Mask         | 2               | Unchanged |
| V-Type Engine        | 30              | Unchanged |
| Whoopie Cushion      | 0.5             | Unchanged |
| Yield Sign           | 6               | Unchanged |

## Equipment

| Item Name        | Weight (Pounds) | Value     |
|------------------|-----------------|-----------|
| Boombox          | 10              | Unchanged |
| Extension Ladder | 10              | Unchanged |
| Flashlight       | 2               | Unchanged |
| Jetpack          | Unchanged       | Unchanged |
| Lockpicker       | 10              | Unchanged |
| Pro-Flashlight   | 2               | Unchanged |
| Radar-Booster    | Unchanged       | Unchanged |
| Shovel           | 5               | Unchanged |
| Spray Paint      | 1               | Unchanged |
| Stun Grenade     | 2               | Unchanged |
| TZP-Inhalant     | 1               | Unchanged |
| Walkie-talkie    | 2               | Unchanged |
| Zap Gun          | 7               | Unchanged |

## Miscellaneous

| Item Name     | Weight (Pounds) | Value     |
|---------------|-----------------|-----------|
| Clipboard     | 0.1             | Unchanged |
| Double-barrel | 15              | Unchanged |
| Key           | 0.1             | Unchanged |
| Player Body   | 100             | Unchanged |
| Sticky Note   | 0.1             | Unchanged |

# Issues / Requests

If you have any feature requests, questions, or encounter any issues like item weights not being changed correctly you
can [open an issue on GitHub](https://github.com/Doug-Murphy/LethalCompanyMods/issues).
Before opening an issue, please search to see if your request, question, or issue already has an open issue.
Alternatively, feel free to open up a PR to address your inquiry yourself. PRs are welcome!

## Known Issues / Roadmap / Oddities

1. Given how the game handles item weights and item values, the item weights _are not_ synced across players while the
   item values _are_ synced.