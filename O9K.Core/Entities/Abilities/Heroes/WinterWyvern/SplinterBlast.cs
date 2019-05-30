using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.WinterWyvern
{
	// Token: 0x020001C2 RID: 450
	[AbilityId(AbilityId.winter_wyvern_splinter_blast)]
	public class SplinterBlast : RangedAreaOfEffectAbility
	{
		// Token: 0x0600090B RID: 2315 RVA: 0x00008327 File Offset: 0x00006527
		public SplinterBlast(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "split_radius");
			this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
		}
	}
}
