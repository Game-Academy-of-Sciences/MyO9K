using System;
using System.Collections.Generic;
using Ensage.SDK.Geometry;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.DarkWillow.Abilities
{
	// Token: 0x02000156 RID: 342
	internal class BrambleMaze : DisableAbility
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x00003482 File Offset: 0x00001682
		public BrambleMaze(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x000207E4 File Offset: 0x0001E9E4
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Unit9 target = targetManager.Target;
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, null);
			Vector3 targetPosition = base.Ability.GetPredictionOutput(predictionInput).TargetPosition;
			Vector3 vector = Vector3.Zero;
			foreach (Vector3 vector2 in this.GetMazePositions(targetPosition))
			{
				Vector3 vector3 = Vector3Extensions.Extend2D(targetPosition, vector2, -vector2.Distance2D(targetPosition, false));
				if (base.Owner.Distance(vector3) <= base.Ability.CastRange)
				{
					vector = vector3;
					break;
				}
			}
			if (vector.IsZero)
			{
				return false;
			}
			if (!base.Ability.UseAbility(vector, false, false))
			{
				return false;
			}
			float num = base.Ability.GetHitTime(targetManager.Target) + 0.5f;
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			targetManager.Target.SetExpectedUnitState(base.Disable.AppliesUnitState, num);
			comboSleeper.Sleep(castDelay);
			base.OrbwalkSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(num);
			return true;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00020918 File Offset: 0x0001EB18
		private IEnumerable<Vector3> GetMazePositions(Vector3 center)
		{
			List<Vector3> list = new List<Vector3>();
			for (int i = 0; i < 2; i++)
			{
				double num = 1.5707963267948966 * (double)i;
				Vector3 vector = new Vector3((float)Math.Cos(num), (float)Math.Sin(num), 0f) * 200f;
				list.Add(center - vector);
				list.Add(center + vector);
			}
			for (int j = 0; j < 2; j++)
			{
				double num2 = 0.78539816339744828 + 1.5707963267948966 * (double)j;
				Vector3 vector2 = new Vector3((float)Math.Cos(num2), (float)Math.Sin(num2), 0f) * 500f;
				list.Add(center - vector2);
				list.Add(center + vector2);
			}
			return list;
		}
	}
}
