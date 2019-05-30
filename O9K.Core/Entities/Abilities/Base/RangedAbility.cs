using System;
using System.Collections.Generic;
using Ensage;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;

namespace O9K.Core.Entities.Abilities.Base
{
	// Token: 0x020003E7 RID: 999
	public abstract class RangedAbility : ActiveAbility
	{
		// Token: 0x060010F3 RID: 4339 RVA: 0x00006683 File Offset: 0x00004883
		protected RangedAbility(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x0000ED7F File Offset: 0x0000CF7F
		public override float CastRange
		{
			get
			{
				return this.BaseCastRange + base.Owner.BonusCastRange;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x0000ED93 File Offset: 0x0000CF93
		protected override float BaseCastRange
		{
			get
			{
				return base.BaseAbility.CastRange;
			}
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x000295A8 File Offset: 0x000277A8
		public override bool CanHit(Unit9 target)
		{
			return (!target.IsMagicImmune || ((!target.IsEnemy(base.Owner) || this.CanHitSpellImmuneEnemy) && (!target.IsAlly(base.Owner) || this.CanHitSpellImmuneAlly))) && base.Owner.Distance(target) <= this.CastRange;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0000EDA2 File Offset: 0x0000CFA2
		public override bool CanHit(Unit9 mainTarget, List<Unit9> aoeTargets, int minCount)
		{
			return this.CanHit(mainTarget);
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0000EDAB File Offset: 0x0000CFAB
		public override bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false)
		{
			return this.UseAbility(target, queue, bypass);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0000EDB7 File Offset: 0x0000CFB7
		public override bool UseAbility(Unit9 mainTarget, List<Unit9> aoeTargets, HitChance minimumChance, int minAOETargets = 0, bool queue = false, bool bypass = false)
		{
			return this.UseAbility(mainTarget, queue, bypass);
		}

		// Token: 0x040008CB RID: 2251
		protected SpecialData RangeData;
	}
}
