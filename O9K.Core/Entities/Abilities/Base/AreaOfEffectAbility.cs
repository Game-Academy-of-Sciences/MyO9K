using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Units;
using O9K.Core.Prediction.Data;

namespace O9K.Core.Entities.Abilities.Base
{
	// Token: 0x020003DD RID: 989
	public abstract class AreaOfEffectAbility : ActiveAbility, IHasRadius
	{
		// Token: 0x0600107C RID: 4220 RVA: 0x0000E7EC File Offset: 0x0000C9EC
		protected AreaOfEffectAbility(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x0000E7FC File Offset: 0x0000C9FC
		public override float Range
		{
			get
			{
				return this.CastRange + this.Radius;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override SkillShotType SkillShotType { get; } = 1;

		// Token: 0x0600107F RID: 4223 RVA: 0x00023C28 File Offset: 0x00021E28
		public override bool CanHit(Unit9 target)
		{
			if (target.IsMagicImmune && ((target.IsEnemy(base.Owner) && !this.CanHitSpellImmuneEnemy) || (target.IsAlly(base.Owner) && !this.CanHitSpellImmuneAlly)))
			{
				return false;
			}
			PredictionInput9 predictionInput = this.GetPredictionInput(target, null);
			return this.GetPredictionOutput(predictionInput).HitChance > HitChance.Impossible;
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00028EAC File Offset: 0x000270AC
		public override bool CanHit(Unit9 mainTarget, List<Unit9> aoeTargets, int minCount)
		{
			PredictionInput9 predictionInput = this.GetPredictionInput(mainTarget, aoeTargets);
			PredictionOutput9 predictionOutput = this.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance <= HitChance.Impossible)
			{
				return false;
			}
			int num = aoeTargets.Count((Unit9 x) => x.IsMagicImmune && ((x.IsEnemy(base.Owner) && !this.CanHitSpellImmuneEnemy) || (x.IsAlly(base.Owner) && !this.CanHitSpellImmuneAlly)));
			return predictionOutput.AoeTargetsHit.Count - num >= minCount;
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00028EFC File Offset: 0x000270FC
		public override bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false)
		{
			PredictionInput9 predictionInput = this.GetPredictionInput(target, null);
			return this.GetPredictionOutput(predictionInput).HitChance >= minimumChance && this.UseAbility(queue, bypass);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00028F2C File Offset: 0x0002712C
		public override bool UseAbility(Unit9 mainTarget, List<Unit9> aoeTargets, HitChance minimumChance, int minAOETargets = 0, bool queue = false, bool bypass = false)
		{
			PredictionInput9 predictionInput = this.GetPredictionInput(mainTarget, aoeTargets);
			PredictionOutput9 predictionOutput = this.GetPredictionOutput(predictionInput);
			return predictionOutput.AoeTargetsHit.Count >= minAOETargets && predictionOutput.HitChance >= minimumChance && this.UseAbility(queue, bypass);
		}
	}
}
