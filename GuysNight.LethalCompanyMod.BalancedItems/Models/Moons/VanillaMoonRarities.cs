using System.Collections.Generic;

namespace GuysNight.LethalCompanyMod.BalancedItems.Models.Moons {
	/// <summary>
	/// Used for when we want to represent vanilla moon rarities.
	/// </summary>
	public class VanillaMoonRarities : IMoonRarities {
		public VanillaMoonRarities(string moonName) {
			MoonName = moonName;
		}

		/// <inheritdoc />
		public string MoonName { get; }

		public Dictionary<string, int> MoonRarityValues { get; } = new Dictionary<string, int>();
	}
}