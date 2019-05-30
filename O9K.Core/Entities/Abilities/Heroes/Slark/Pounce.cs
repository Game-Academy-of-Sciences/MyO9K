using System;
using System.Collections.Generic;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.Core.Entities.Abilities.Heroes.Slark
{
	// Token: 0x020002CD RID: 717
	[AbilityId(AbilityId.slark_pounce)]
	public class Pounce : LineAbility, IBlink, IActiveAbility
	{
		// Token: 0x06000C9A RID: 3226 RVA: 0x00025B70 File Offset: 0x00023D70
		public Pounce(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "pounce_radius");
			this.SpeedData = new SpecialData(baseAbility, "pounce_speed");
			this.castRangeData = new SpecialData(baseAbility, "pounce_distance");
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0000B56B File Offset: 0x0000976B
		public override bool HasAreaOfEffect { get; }

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0000B573 File Offset: 0x00009773
		public BlinkType BlinkType { get; } = 1;

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0000B57B File Offset: 0x0000977B
		public override float CastRange
		{
			get
			{
				return this.castRangeData.GetValue(this.Level);
			}
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00025BC0 File Offset: 0x00023DC0
		public override bool CanHit(Unit9 target)
		{
			if (target.IsMagicImmune && ((target.IsEnemy(base.Owner) && !this.CanHitSpellImmuneEnemy) || (target.IsAlly(base.Owner) && !this.CanHitSpellImmuneAlly)))
			{
				return false;
			}
			PredictionInput9 predictionInput = this.GetPredictionInput(target, null);
			PredictionOutput9 predictionOutput = this.GetPredictionOutput(predictionInput);
			return predictionOutput.HitChance > HitChance.Impossible && base.Owner.GetAngle(predictionOutput.CastPosition, false) <= 0.1f;
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00006EBB File Offset: 0x000050BB
		public override float GetHitTime(Vector3 position)
		{
			return this.GetCastDelay(position) + this.ActivationDelay + this.Range / this.Speed;
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0000B58E File Offset: 0x0000978E
		public override PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
		{
			PredictionInput9 predictionInput = base.GetPredictionInput(target, aoeTargets);
			predictionInput.CastRange -= 100f;
			predictionInput.Range -= 100f;
			return predictionInput;
		}

		// Token: 0x0400067F RID: 1663
		private readonly SpecialData castRangeData;
	}
}
