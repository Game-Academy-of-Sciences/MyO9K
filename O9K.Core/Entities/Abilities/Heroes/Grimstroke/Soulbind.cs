using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Grimstroke
{
	// Token: 0x02000376 RID: 886
	[AbilityId(AbilityId.grimstroke_soul_chain)]
	public class Soulbind : RangedAreaOfEffectAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000F3D RID: 3901 RVA: 0x0000D73F File Offset: 0x0000B93F
		public Soulbind(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "chain_latch_radius");
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x0000D764 File Offset: 0x0000B964
		public string DebuffModifierName { get; } = "modifier_grimstroke_soul_chain";
	}
}
