using System;
using System.Collections.Generic;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.Core.Entities.Abilities.Heroes.Pangolier
{
	// Token: 0x02000308 RID: 776
	[AbilityId(AbilityId.pangolier_swashbuckle)]
	public class Swashbuckle : LineAbility, IBlink, INuke, IActiveAbility
	{
		// Token: 0x06000D65 RID: 3429 RVA: 0x00026A7C File Offset: 0x00024C7C
		public Swashbuckle(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "start_radius");
			this.SpeedData = new SpecialData(baseAbility, "dash_speed");
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.RangeData = new SpecialData(baseAbility, "range");
			this.strikesData = new SpecialData(baseAbility, "strikes");
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x0000BEE7 File Offset: 0x0000A0E7
		public override bool CanHitSpellImmuneEnemy { get; } = 1;

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x0000BEEF File Offset: 0x0000A0EF
		public override bool IntelligenceAmplify { get; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0000BEF7 File Offset: 0x0000A0F7
		public BlinkType BlinkType { get; }

		// Token: 0x06000D69 RID: 3433 RVA: 0x00009B08 File Offset: 0x00007D08
		public override PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
		{
			PredictionInput9 predictionInput = base.GetPredictionInput(target, aoeTargets);
			predictionInput.UseBlink = true;
			predictionInput.AreaOfEffect = false;
			return predictionInput;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00026AEC File Offset: 0x00024CEC
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage rawDamage = base.GetRawDamage(unit, remainingHealth);
			Damage onHitEffectDamage = base.Owner.GetOnHitEffectDamage(unit);
			return (rawDamage + onHitEffectDamage) * this.strikesData.GetValue(this.Level);
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00026B2C File Offset: 0x00024D2C
		public override bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false)
		{
			PredictionInput9 predictionInput = this.GetPredictionInput(target, null);
			PredictionOutput9 predictionOutput = this.GetPredictionOutput(predictionInput);
			return predictionOutput.HitChance >= minimumChance && this.UseAbility(predictionOutput.BlinkLinePosition, predictionOutput.CastPosition, queue, bypass);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00026B6C File Offset: 0x00024D6C
		public override bool UseAbility(Unit9 mainTarget, List<Unit9> aoeTargets, HitChance minimumChance, int minAOETargets = 0, bool queue = false, bool bypass = false)
		{
			PredictionInput9 predictionInput = this.GetPredictionInput(mainTarget, aoeTargets);
			PredictionOutput9 predictionOutput = this.GetPredictionOutput(predictionInput);
			return predictionOutput.AoeTargetsHit.Count >= minAOETargets && predictionOutput.HitChance >= minimumChance && this.UseAbility(predictionOutput.BlinkLinePosition, predictionOutput.CastPosition, queue, bypass);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00024888 File Offset: 0x00022A88
		public bool UseAbility(Vector3 startPosition, Vector3 direction, bool queue = false, bool bypass = false)
		{
			if (!base.BaseAbility.TargetPosition(startPosition, queue, bypass) || !base.BaseAbility.TargetPosition(direction, queue, bypass))
			{
				return false;
			}
			bool flag = base.BaseAbility.UseAbility(startPosition, queue, bypass);
			if (flag)
			{
				base.ActionSleeper.Sleep(0.1f);
			}
			return flag;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00026BBC File Offset: 0x00024DBC
		public override bool UseAbility(Vector3 position, bool queue = false, bool bypass = false)
		{
			float distance = Math.Max(base.Owner.GetAttackRange(null, 0f), base.Owner.Distance(position) - this.CastRange);
			Vector3 startPosition = position.Extend2D(base.Owner.Position, distance);
			return this.UseAbility(startPosition, position, queue, bypass);
		}

		// Token: 0x040006EC RID: 1772
		private readonly SpecialData strikesData;
	}
}
