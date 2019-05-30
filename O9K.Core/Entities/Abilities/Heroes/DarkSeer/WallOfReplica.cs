using System;
using System.Collections.Generic;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.Core.Entities.Abilities.Heroes.DarkSeer
{
	// Token: 0x0200024A RID: 586
	[AbilityId(AbilityId.dark_seer_wall_of_replica)]
	public class WallOfReplica : LineAbility
	{
		// Token: 0x06000ABA RID: 2746 RVA: 0x00009ADB File Offset: 0x00007CDB
		public WallOfReplica(Ability baseAbility) : base(baseAbility)
		{
			this.RangeData = new SpecialData(baseAbility, "width");
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x00009B00 File Offset: 0x00007D00
		public override float Radius { get; } = 200f;

		// Token: 0x06000ABC RID: 2748 RVA: 0x00009B08 File Offset: 0x00007D08
		public override PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
		{
			PredictionInput9 predictionInput = base.GetPredictionInput(target, aoeTargets);
			predictionInput.UseBlink = true;
			predictionInput.AreaOfEffect = false;
			return predictionInput;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x000247F8 File Offset: 0x000229F8
		public override bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false)
		{
			PredictionInput9 predictionInput = this.GetPredictionInput(target, null);
			PredictionOutput9 predictionOutput = this.GetPredictionOutput(predictionInput);
			return predictionOutput.HitChance >= minimumChance && this.UseAbility(predictionOutput.BlinkLinePosition, predictionOutput.CastPosition, queue, bypass);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00024838 File Offset: 0x00022A38
		public override bool UseAbility(Unit9 mainTarget, List<Unit9> aoeTargets, HitChance minimumChance, int minAOETargets = 0, bool queue = false, bool bypass = false)
		{
			PredictionInput9 predictionInput = this.GetPredictionInput(mainTarget, aoeTargets);
			PredictionOutput9 predictionOutput = this.GetPredictionOutput(predictionInput);
			return predictionOutput.AoeTargetsHit.Count >= minAOETargets && predictionOutput.HitChance >= minimumChance && this.UseAbility(predictionOutput.BlinkLinePosition, predictionOutput.CastPosition, queue, bypass);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00024888 File Offset: 0x00022A88
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

		// Token: 0x06000AC0 RID: 2752 RVA: 0x000248DC File Offset: 0x00022ADC
		public override bool UseAbility(Vector3 position, bool queue = false, bool bypass = false)
		{
			float distance = Math.Max(base.Owner.GetAttackRange(null, 0f), base.Owner.Distance(position) - this.CastRange);
			Vector3 startPosition = position.Extend2D(base.Owner.Position, distance);
			return this.UseAbility(startPosition, position, queue, bypass);
		}
	}
}
