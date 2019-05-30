using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Spectre
{
	// Token: 0x020002C5 RID: 709
	[AbilityId(AbilityId.spectre_reality)]
	public class Reality : RangedAbility
	{
		// Token: 0x06000C82 RID: 3202 RVA: 0x0000B43D File Offset: 0x0000963D
		public Reality(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0000B458 File Offset: 0x00009658
		public override bool CanHitSpellImmuneEnemy { get; } = 1;

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0000B460 File Offset: 0x00009660
		public override float CastRange { get; } = 9999999f;
	}
}
