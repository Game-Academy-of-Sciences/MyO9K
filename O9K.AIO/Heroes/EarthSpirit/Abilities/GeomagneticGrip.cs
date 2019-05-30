using System;
using System.Linq;
using Ensage.SDK.Geometry;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;
using O9K.Core.Prediction.Data;

namespace O9K.AIO.Heroes.EarthSpirit.Abilities
{
	// Token: 0x0200018E RID: 398
	internal class GeomagneticGrip : NukeAbility
	{
		// Token: 0x0600081C RID: 2076 RVA: 0x000032F0 File Offset: 0x000014F0
		public GeomagneticGrip(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00006094 File Offset: 0x00004294
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return !base.Owner.HasModifier("modifier_earth_spirit_rolling_boulder_caster");
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x000251CC File Offset: 0x000233CC
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			Unit9 target = targetManager.Target;
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, null);
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1)
			{
				return false;
			}
			foreach (Unit9 unit in from x in EntityManager9.Units
			where x.IsUnit && (!x.IsHero || x.IsIllusion) && x.Distance(base.Owner) < base.Ability.CastRange && x.IsAlive
			select x)
			{
				predictionInput.Caster = unit;
				predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
				if (new Polygon.Rectangle(base.Owner.Position, unit.Position, base.Ability.Radius).IsInside(predictionOutput.TargetPosition) && base.Ability.UseAbility(unit, false, false))
				{
					float castDelay = base.Ability.GetCastDelay(targetManager.Target);
					comboSleeper.Sleep(castDelay);
					base.Sleeper.Sleep(castDelay + 0.5f);
					base.OrbwalkSleeper.Sleep(castDelay);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x000252F0 File Offset: 0x000234F0
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Unit9 target = targetManager.Target;
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, null);
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1)
			{
				return false;
			}
			foreach (Unit9 unit in from x in EntityManager9.Units
			where x.Name == "npc_dota_earth_spirit_stone" && x.Distance(base.Owner) < base.Ability.CastRange && x.IsAlive
			select x)
			{
				predictionInput.Caster = unit;
				predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
				if (new Polygon.Rectangle(base.Owner.Position, unit.Position, base.Ability.Radius).IsInside(predictionOutput.TargetPosition) && base.Ability.UseAbility(unit.Position, false, false))
				{
					float castDelay = base.Ability.GetCastDelay(targetManager.Target);
					comboSleeper.Sleep(castDelay);
					base.Sleeper.Sleep(castDelay + 0.5f);
					base.OrbwalkSleeper.Sleep(castDelay);
					return true;
				}
			}
			return false;
		}
	}
}
