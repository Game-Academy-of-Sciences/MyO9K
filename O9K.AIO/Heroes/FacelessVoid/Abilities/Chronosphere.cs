using System;
using System.Collections.Generic;
using System.Linq;
using Ensage.SDK.Geometry;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.FacelessVoid.Abilities
{
	// Token: 0x02000171 RID: 369
	internal class Chronosphere : DisableAbility
	{
		// Token: 0x06000798 RID: 1944 RVA: 0x00003482 File Offset: 0x00001682
		public Chronosphere(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00005CF6 File Offset: 0x00003EF6
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return base.CanHit(targetManager, comboMenu) && base.Ability.CanHit(targetManager.Target, targetManager.EnemyHeroes, this.TargetsToHit(comboMenu));
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x000034BC File Offset: 0x000016BC
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new UsableAbilityHitCountMenu(base.Ability, simplifiedName);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00005D27 File Offset: 0x00003F27
		public override bool ShouldCast(TargetManager targetManager)
		{
			return base.ShouldCast(targetManager) && !base.Owner.HasModifier("modifier_faceless_void_time_walk");
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x000034CA File Offset: 0x000016CA
		public int TargetsToHit(IComboModeMenu comboMenu)
		{
			return comboMenu.GetAbilitySettingsMenu<UsableAbilityHitCountMenu>(this).HitCount;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x000231A8 File Offset: 0x000213A8
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Unit9 target = targetManager.Target;
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, targetManager.EnemyHeroes);
			float inputDelay = predictionInput.Delay;
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			float radius = base.Ability.Radius + 25f;
			if (predictionOutput.HitChance < 1)
			{
				return false;
			}
			Vector3 castPosition = predictionOutput.CastPosition;
			List<Vector3> source = (from x in (from x in targetManager.AllyHeroes
			where !x.Equals(this.Owner)
			select x).ToList<Unit9>()
			select x.GetPredictedPosition(inputDelay)).ToList<Vector3>();
			Dictionary<Vector3, int> dictionary = new Dictionary<Vector3, int>
			{
				{
					castPosition,
					source.Count((Vector3 x) => x.Distance2D(castPosition, false) < radius)
				}
			};
			if (dictionary[castPosition] > 0)
			{
				Vector3 position = base.Owner.Position;
				int num = (int)Math.Ceiling((double)(radius / 50f));
				for (int i = 0; i < num; i++)
				{
					double num2 = 3.1415926535897931 / (double)num * (double)i;
					Vector3 vector;
					vector..ctor((float)Math.Cos(num2), (float)Math.Sin(num2), 0f);
					for (float num3 = 0.25f; num3 <= 1f; num3 += 0.25f)
					{
						Vector3 vector2 = vector * (predictionInput.CastRange * num3);
						Vector3 start = position - vector2;
						Vector3 end = position + vector2;
						if (predictionOutput.AoeTargetsHit.All((PredictionOutput9 x) => x.TargetPosition.Distance2D(start, false) < this.Ability.Radius))
						{
							dictionary[start] = source.Count((Vector3 x) => x.Distance2D(start, false) < radius);
						}
						if (predictionOutput.AoeTargetsHit.All((PredictionOutput9 x) => x.TargetPosition.Distance2D(end, false) < this.Ability.Radius))
						{
							dictionary[end] = source.Count((Vector3 x) => x.Distance2D(end, false) < radius);
						}
					}
				}
				castPosition = (from x in dictionary
				orderby x.Value, x.Key.Distance2D(castPosition, false)
				select x.Key).First<Vector3>();
			}
			if (!base.Ability.UseAbility(castPosition, false, false))
			{
				return false;
			}
			float num4 = base.Ability.GetHitTime(targetManager.Target) + 0.5f;
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			targetManager.Target.SetExpectedUnitState(base.Disable.AppliesUnitState, num4);
			comboSleeper.Sleep(castDelay);
			base.OrbwalkSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(num4);
			return true;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00002E73 File Offset: 0x00001073
		protected override bool ChainStun(Unit9 target, bool invulnerability)
		{
			return true;
		}
	}
}
