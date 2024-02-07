using System.Collections.Generic;

namespace GuysNight.LethalCompanyMod.BalancedItems.Models.Moons {
	/// <summary>
	/// Used for when we want to represent override moon rarities.
	/// </summary>
	internal sealed class OverrideMoonRarities : IMoonRarities {
		internal OverrideMoonRarities(string moonName) {
			MoonName = moonName;
		}

		/// <inheritdoc />
		public string MoonName { get; }

		public Dictionary<string, int> MoonRarityValues { get; } = new Dictionary<string, int>();
	}
}