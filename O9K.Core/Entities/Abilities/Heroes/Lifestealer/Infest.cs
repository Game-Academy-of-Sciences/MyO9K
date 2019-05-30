using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Lifestealer
{
	// Token: 0x02000350 RID: 848
	[AbilityId(AbilityId.life_stealer_infest)]
	public class Infest : RangedAbility
	{
		// Token: 0x06000E5F RID: 3679 RVA: 0x00006527 File Offset: 0x00004727
		public Infest(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x0000720E File Offset: 0x0000540E
		public override float CastRange
		{
			get
			{
				return base.CastRange + 100f;
			}
		}
	}
}
