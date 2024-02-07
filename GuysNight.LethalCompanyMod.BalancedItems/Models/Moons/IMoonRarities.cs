using System.Collections.Generic;

namespace GuysNight.LethalCompanyMod.BalancedItems.Models.Moons {
	public interface IMoonRarities {
		/// <summary>
		/// The name of the moon that the rarities are for.
		/// </summary>
		public string MoonName { get; }

		/// <summary>
		/// The values for the moon rarities.
		/// </summary>
		public Dictionary<string, int> MoonRarityValues { get; }
	}
}