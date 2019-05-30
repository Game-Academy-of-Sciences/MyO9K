using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.SandKing
{
	// Token: 0x020002DB RID: 731
	[AbilityId(AbilityId.sandking_sand_storm)]
	public class SandStorm : AreaOfEffectAbility
	{
		// Token: 0x06000CC5 RID: 3269 RVA: 0x0000B797 File Offset: 0x00009997
		public SandStorm(Ability baseAbility) : base(baseAbility)
		{
			base.IsInvisibility = true;
			this.RadiusData = new SpecialData(baseAbility, "sand_storm_radius");
			this.DamageData = new SpecialData(baseAbility, "sand_storm_damage");
		}
	}
}
