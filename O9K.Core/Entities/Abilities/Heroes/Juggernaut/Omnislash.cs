using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Juggernaut
{
	// Token: 0x02000359 RID: 857
	[AbilityId(AbilityId.juggernaut_omni_slash)]
	public class Omnislash : RangedAbility
	{
		// Token: 0x06000E87 RID: 3719 RVA: 0x0000CC7C File Offset: 0x0000AE7C
		public Omnislash(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "omni_slash_radius");
			this.DamageData = new SpecialData(baseAbility, "bonus_damage");
		}
	}
}
