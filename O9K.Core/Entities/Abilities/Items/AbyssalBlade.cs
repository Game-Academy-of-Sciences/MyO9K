using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200017A RID: 378
	[AbilityId(AbilityId.item_abyssal_blade)]
	public class AbyssalBlade : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x000071E5 File Offset: 0x000053E5
		public AbyssalBlade(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x000071FE File Offset: 0x000053FE
		public override bool CanHitSpellImmuneEnemy { get; } = 1;

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x00007206 File Offset: 0x00005406
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x0000720E File Offset: 0x0000540E
		public override float CastRange
		{
			get
			{
				return base.CastRange + 100f;
			}
		}
	}
}
