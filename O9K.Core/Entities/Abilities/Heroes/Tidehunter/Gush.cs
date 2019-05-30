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

namespace O9K.Core.Entities.Abilities.Heroes.Tidehunter
{
	// Token: 0x020002AE RID: 686
	[AbilityId(AbilityId.tidehunter_gush)]
	public class Gush : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000C19 RID: 3097 RVA: 0x000255C8 File Offset: 0x000237C8
		public Gush(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "aoe_scepter");
			this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
			this.DamageData = new SpecialData(baseAbility, "gush_damage");
			this.scepterSpeedData = new SpecialData(baseAbility, "speed_scepter");
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x00007F8A File Offset: 0x0000618A
		public override bool PositionCast
		{
			get
			{
				return base.Owner.HasAghanimsScepter;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0000AE10 File Offset: 0x00009010
		public override float Radius
		{
			get
			{
				if (base.Owner.HasAghanimsScepter)
				{
					return this.RadiusData.GetValue(this.Level);
				}
				return 0f;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0000AE36 File Offset: 0x00009036
		public override float Speed
		{
			get
			{
				if (base.Owner.HasAghanimsScepter)
				{
					return this.scepterSpeedData.GetValue(this.Level);
				}
				return base.Speed;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x00007F9C File Offset: 0x0000619C
		public override bool UnitTargetCast
		{
			get
			{
				return !this.PositionCast;
			}
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00025620 File Offset: 0x00023820
		public override bool UseAbility(Unit9 mainTarget, List<Unit9> aoeTargets, HitChance minimumChance, int minAOETargets = 0, bool queue = false, bool bypass = false)
		{
			if (!this.PositionCast)
			{
				return this.UseAbility(mainTarget, queue, bypass);
			}
			PredictionInput9 predictionInput = this.GetPredictionInput(mainTarget, aoeTargets);
			PredictionOutput9 predictionOutput = this.GetPredictionOutput(predictionInput);
			return predictionOutput.HitChance >= minimumChance && this.UseAbility(predictionOutput.CastPosition, queue, bypass);
		}

		// Token: 0x04000635 RID: 1589
		private readonly SpecialData scepterSpeedData;
	}
}
