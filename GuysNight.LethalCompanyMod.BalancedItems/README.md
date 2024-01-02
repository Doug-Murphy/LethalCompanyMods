# Introduction

Balanced Items is a simple mod designed to give more realistic weights and values to all of the items in Lethal Company.
This mod includes item adjustments for all scrap/loot items as well as those purchasable from the terminal.

# Configuration

While most mods create their configuration file when the mod loads upon game start, this mod does it a bit differently.
The config file is created dynamically as you play the game.
When you start a round, the mod retrieves all spawnable scrap items across all moons and creates config entries for
those. _This is done for all scrap on all moons before you land your ship._
When the mod creates config entries, it first checks if an override for the weight or average sell value is preset in
the mod.
If so, that value is created into the config. If not, the vanilla value is created into the config.
Config values that you specify will always be respected above all else. The vanilla values and the preset overrides in
this mod will be ignored.

## Weights

When specifying a weight, enter how many pounds you want the item to weigh.
> NOTE: The game has some oddities with weights to where sometimes items weigh 1-2 pounds more than you specify.

## Average Sell Value

The average sell value is just that - the average. The mod will calculate the min and max sell values to be +/- 20% of
this average sell value.
> NOTE: The game always sells equipment for 0 credits despite what you set in the config.

> NOTE: This adjustment causes items to have a narrower range between the minimum and maximum compared to vanilla
> gameplay. This adjustment
> ensures that there won't be any significant imbalances in the game, avoiding scenarios that are either too easy or
> too difficult. It's important to keep in mind that the game applies different scrap multipliers for various moons,
> which means the actual in-game values will likely be lower than the averages you specify.

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