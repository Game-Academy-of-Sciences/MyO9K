using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;

namespace O9K.Core.Entities.Abilities.Base
{
	// Token: 0x020003DC RID: 988
	public abstract class PredictionAbility : RangedAbility, IHasRadius
	{
		// Token: 0x06001075 RID: 4213 RVA: 0x00006527 File Offset: 0x00004727
		protected PredictionAbility(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0000E7C6 File Offset: 0x0000C9C6
		public override float Range
		{
			get
			{
				SpecialData rangeData = this.RangeData;
				return ((rangeData != null) ? rangeData.GetValue(this.Level) : this.CastRange) + this.Radius;
			}
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x00023C28 File Offset: 0x00021E28
		public override bool CanHit(Unit9 target)
		{
			if (target.IsMagicImmune && ((target.IsEnemy(base.Owner) && !this.CanHitSpellImmuneEnemy) || (target.IsAlly(base.Owner) && !this.CanHitSpellImmuneAlly)))
			{
				return false;
			}
			PredictionInput9 predictionInput = this.GetPredictionInput(target, null);
			return this.GetPredictionOutput(predictionInput).HitChance > HitChance.Impossible;
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00028DD8 File Offset: 0x00026FD8
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

		// Token: 0x06001079 RID: 4217 RVA: 0x00028E28 File Offset: 0x00027028
		public override bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false)
		{
			PredictionInput9 predictionInput = this.GetPredictionInput(target, null);
			PredictionOutput9 predictionOutput = this.GetPredictionOutput(predictionInput);
			return predictionOutput.HitChance >= minimumChance && this.UseAbility(predictionOutput.CastPosition, queue, bypass);
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00028E60 File Offset: 0x00027060
		public override bool UseAbility(Unit9 mainTarget, List<Unit9> aoeTargets, HitChance minimumChance, int minAOETargets = 0, bool queue = false, bool bypass = false)
		{
			PredictionInput9 predictionInput = this.GetPredictionInput(mainTarget, aoeTargets);
			PredictionOutput9 predictionOutput = this.GetPredictionOutput(predictionInput);
			return predictionOutput.AoeTargetsHit.Count >= minAOETargets && predictionOutput.HitChance >= minimumChance && this.UseAbility(predictionOutput.CastPosition, queue, bypass);
		}
	}
}
