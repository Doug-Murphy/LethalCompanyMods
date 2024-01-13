# Version 1.4.0

## Added

1. You can now enable or disable entire sections of the config to better tweak how you want the game to be. Some
   settings will require exiting to menu and restarting your session. View the config description for more information.
2. Add feature toggle for allowing you to sell equipment items for their average value instead of for 0 credits.

## Changed

1. Config description entries now list the default vanilla values for that override.

## Removed

Nothing.

# Version 1.3.1

1. Fix issue when another mod is loaded that adds moons that have invalid characters for BepInEx configuration in their
   names.

# Version 1.3.0

## Added

1. Allow specifying the item rarity values per item per moon.
2. Change total scrap value to instantly display instead of counting up.
3. Change total scrap value to display values > 10,000 and shrink font size as needed so that it doesn't wrap.

## Changed

1. Set allowable ranges for config entries to help with setting valid values.
2. The mod now only saves to the config file when needed instead of _everytime_ we bind a value to the config. This
   increases performance by reducing the number of save requests to your disk.

## Removed

Nothing.

# Version 1.2.0

## Added

1. Config file support. Huzzah! All items in the game (including custom items from other mods) will be registered into
   your config when you start a round, and reflected in-game at the start of each day.
2. Fixed bug in the game where the carry weight was being displayed as 5% higher than actual.

## Changed

Nothing.

## Removed

Nothing.

# Version 1.1.0

## Added

1. Print debug output in the game's log when starting a session to display all spawnable scrap on all levels with the
   name and vanilla min/max values from the game.
2. Server-side overrides of item sell values. The values are currently the vanilla values, but the min/max range in the
   value is set to +/- 20% of the average value instead of an arbitrary min/max range.

## Changed

1. Fixed cash register overrides not being applied.

## Removed

Nothing.

# Version 1.0.1

## Added

Nothing.

## Changed

1. Fixed some namespaces just to avoid any confusion.

## Removed

Nothing.

# Version 1.0.0

## Added

1. Client-side overrides of item weights. These values do not sync to other players in your lobby, regardless of who is
   hosting the session.

## Changed

Nothing.

## Removed

Nothing.