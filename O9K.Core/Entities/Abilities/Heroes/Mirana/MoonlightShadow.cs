using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Mirana
{
	// Token: 0x02000205 RID: 517
	[AbilityId(AbilityId.mirana_invis)]
	public class MoonlightShadow : AreaOfEffectAbility
	{
		// Token: 0x060009F9 RID: 2553 RVA: 0x00008FD1 File Offset: 0x000071D1
		public MoonlightShadow(Ability ability) : base(ability)
		{
			base.IsInvisibility = true;
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x00008FEC File Offset: 0x000071EC
		public override float Radius { get; } = 9999999f;
	}
}
