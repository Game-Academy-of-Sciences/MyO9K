using System;
using System.Linq;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.EarthSpirit.Abilities
{
	// Token: 0x0200018F RID: 399
	internal class RollingBoulder : NukeAbility
	{
		// Token: 0x06000822 RID: 2082 RVA: 0x000032F0 File Offset: 0x000014F0
		public RollingBoulder(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00002E73 File Offset: 0x00001073
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return true;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00025418 File Offset: 0x00023618
		public bool SimpleUseAbility(Vector3 position)
		{
			if (!base.Ability.UseAbility(position, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(position);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00025464 File Offset: 0x00023664
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			Unit9 target = targetManager.Target;
			float distance = base.Owner.Distance(target);
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, null);
			if (EntityManager9.Units.Any((Unit9 x) => x.Name == "npc_dota_earth_spirit_stone" && x.Distance(this.Owner) < distance && x.Distance(this.Owner) < 350f && x.IsAlive && Vector3Extensions.AngleBetween(this.Owner.Position, x.Position, target.Position) < 75f))
			{
				predictionInput.Speed *= 2f;
				predictionInput.CastRange *= 2f;
				predictionInput.Range *= 2f;
			}
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1)
			{
				return false;
			}
			if (!base.Ability.UseAbility(predictionOutput.CastPosition, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0002556C File Offset: 0x0002376C
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Unit9 target = targetManager.Target;
			float distance = base.Owner.Distance(target);
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, null);
			if (EntityManager9.Units.Any((Unit9 x) => x.Name == "npc_dota_earth_spirit_stone" && x.Distance(this.Owner) < distance && x.Distance(this.Owner) < 350f && x.IsAlive && Vector3Extensions.AngleBetween(this.Owner.Position, x.Position, target.Position) < 75f))
			{
				predictionInput.Speed *= 2f;
				predictionInput.CastRange *= 2f;
				predictionInput.Range *= 2f;
			}
			else if (distance > 350f)
			{
				return false;
			}
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1)
			{
				return false;
			}
			if (!base.Ability.UseAbility(predictionOutput.CastPosition, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
