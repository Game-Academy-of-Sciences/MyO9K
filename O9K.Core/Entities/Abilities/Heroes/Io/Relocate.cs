using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Io
{
	// Token: 0x02000227 RID: 551
	[AbilityId(AbilityId.wisp_relocate)]
	public class Relocate : RangedAbility
	{
		// Token: 0x06000A69 RID: 2665 RVA: 0x0000963C File Offset: 0x0000783C
		public Relocate(Ability baseAbility) : base(baseAbility)
		{
			this.ActivationDelayData = new SpecialData(baseAbility, "cast_delay");
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00009661 File Offset: 0x00007861
		public override float CastRange { get; } = 9999999f;
	}
}
