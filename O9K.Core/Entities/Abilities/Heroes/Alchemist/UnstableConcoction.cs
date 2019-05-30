using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Alchemist
{
	// Token: 0x020003D4 RID: 980
	[AbilityId(AbilityId.alchemist_unstable_concoction)]
	public class UnstableConcoction : ActiveAbility
	{
		// Token: 0x0600104C RID: 4172 RVA: 0x0000E5D5 File Offset: 0x0000C7D5
		public UnstableConcoction(Ability baseAbility) : base(baseAbility)
		{
			this.brewExplosionTimeData = new SpecialData(baseAbility, "brew_explosion");
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x0000E5EF File Offset: 0x0000C7EF
		public float BrewExplosion
		{
			get
			{
				return this.brewExplosionTimeData.GetValue(this.Level);
			}
		}

		// Token: 0x0400087F RID: 2175
		private readonly SpecialData brewExplosionTimeData;
	}
}
