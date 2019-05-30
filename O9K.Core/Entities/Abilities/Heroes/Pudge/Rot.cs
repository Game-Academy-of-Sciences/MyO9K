using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;

namespace O9K.Core.Entities.Abilities.Heroes.Pudge
{
	// Token: 0x020002EF RID: 751
	[AbilityId(AbilityId.pudge_rot)]
	public class Rot : ToggleAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000D09 RID: 3337 RVA: 0x0000BA86 File Offset: 0x00009C86
		public Rot(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "rot_damage");
			this.RadiusData = new SpecialData(baseAbility, "rot_radius");
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0000BAC3 File Offset: 0x00009CC3
		public override SkillShotType SkillShotType { get; } = 1;

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x0000BACB File Offset: 0x00009CCB
		public string DebuffModifierName { get; } = "modifier_pudge_rot";

		// Token: 0x06000D0C RID: 3340 RVA: 0x00023C28 File Offset: 0x00021E28
		public override bool CanHit(Unit9 target)
		{
			if (target.IsMagicImmune && ((target.IsEnemy(base.Owner) && !this.CanHitSpellImmuneEnemy) || (target.IsAlly(base.Owner) && !this.CanHitSpellImmuneAlly)))
			{
				return false;
			}
			PredictionInput9 predictionInput = this.GetPredictionInput(target, null);
			return this.GetPredictionOutput(predictionInput).HitChance > HitChance.Impossible;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x000262CC File Offset: 0x000244CC
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

		// Token: 0x06000D0E RID: 3342 RVA: 0x00023CD8 File Offset: 0x00021ED8
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
