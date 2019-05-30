using System;
using System.Collections.Generic;
using Ensage;
using O9K.Core.Entities.Units;
using O9K.Core.Prediction.Data;

namespace O9K.Core.Entities.Abilities.Base
{
	// Token: 0x020003E8 RID: 1000
	public abstract class RangedAreaOfEffectAbility : PredictionAbility
	{
		// Token: 0x060010FA RID: 4346 RVA: 0x0000EDC4 File Offset: 0x0000CFC4
		protected RangedAreaOfEffectAbility(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x0000EDD4 File Offset: 0x0000CFD4
		public override SkillShotType SkillShotType { get; } = 2;

		// Token: 0x060010FC RID: 4348 RVA: 0x0000EDAB File Offset: 0x0000CFAB
		public override bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false)
		{
			return this.UseAbility(target, queue, bypass);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0000EDB7 File Offset: 0x0000CFB7
		public override bool UseAbility(Unit9 mainTarget, List<Unit9> aoeTargets, HitChance minimumChance, int minAOETargets = 0, bool queue = false, bool bypass = false)
		{
			return this.UseAbility(mainTarget, queue, bypass);
		}
	}
}
