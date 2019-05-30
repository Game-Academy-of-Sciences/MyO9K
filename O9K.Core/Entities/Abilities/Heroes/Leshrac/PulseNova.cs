using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;

namespace O9K.Core.Entities.Abilities.Heroes.Leshrac
{
	// Token: 0x02000215 RID: 533
	[AbilityId(AbilityId.leshrac_pulse_nova)]
	public class PulseNova : ToggleAbility, IHasRadius
	{
		// Token: 0x06000A29 RID: 2601 RVA: 0x000092BA File Offset: 0x000074BA
		public PulseNova(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x000092EC File Offset: 0x000074EC
		public override SkillShotType SkillShotType { get; } = 1;

		// Token: 0x06000A2B RID: 2603 RVA: 0x00023C28 File Offset: 0x00021E28
		public override bool CanHit(Unit9 target)
		{
			if (target.IsMagicImmune && ((target.IsEnemy(base.Owner) && !this.CanHitSpellImmuneEnemy) || (target.IsAlly(base.Owner) && !this.CanHitSpellImmuneAlly)))
			{
				return false;
			}
			PredictionInput9 predictionInput = this.GetPredictionInput(target, null);
			return this.GetPredictionOutput(predictionInput).HitChance > HitChance.Impossible;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00023C88 File Offset: 0x00021E88
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

		// Token: 0x06000A2D RID: 2605 RVA: 0x00023CD8 File Offset: 0x00021ED8
		public override PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
		{
			PredictionInput9 predictionInput = new PredictionInput9
			{
				Caster = base.Owner,
				Target = target,
				CollisionTypes = this.CollisionTypes,
				Delay = this.CastPoint + this.ActivationDelay + Ability9.InputLag,
				Speed = this.Speed,
				CastRange = this.CastRange,
				Range = this.Range,
				Radius = this.Radius,
				SkillShotType = this.SkillShotType
			};
			if (aoeTargets != null)
			{
				predictionInput.AreaOfEffect = this.HasAreaOfEffect;
				predictionInput.AreaOfEffectTargets = aoeTargets;
			}
			return predictionInput;
		}
	}
}
