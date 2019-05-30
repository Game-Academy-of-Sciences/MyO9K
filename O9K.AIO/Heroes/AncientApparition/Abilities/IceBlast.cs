using System;
using System.Linq;
using Ensage;
using Ensage.SDK.Geometry;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.AncientApparition;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.AncientApparition.Abilities
{
	// Token: 0x020001F0 RID: 496
	internal class IceBlast : AoeAbility
	{
		// Token: 0x060009D6 RID: 2518 RVA: 0x00006D85 File Offset: 0x00004F85
		public IceBlast(ActiveAbility ability) : base(ability)
		{
			this.iceBlast = (IceBlast)ability;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00006D9A File Offset: 0x00004F9A
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new IceBlastMenu(base.Ability, simplifiedName);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00006DA8 File Offset: 0x00004FA8
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return (!comboMenu.GetAbilitySettingsMenu<IceBlastMenu>(this).StunOnly || targetManager.Target.GetImmobilityDuration() >= 1.5f) && base.CanHit(targetManager, comboMenu);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0002ABFC File Offset: 0x00028DFC
		public bool Release(TargetManager targetManager, Sleeper comboSleeper)
		{
			if (this.iceBlast.IsUsable)
			{
				return false;
			}
			Unit unit = ObjectManager.GetEntitiesFast<Unit>().FirstOrDefault((Unit x) => x.IsValid && x.NetworkName == "CDOTA_BaseNPC" && x.DayVision == 550u && x.Health == 150u && x.Team == base.Owner.Team);
			if (unit == null)
			{
				return true;
			}
			Vector3 position = unit.Position;
			Vector3 v = Vector3Extensions.Extend2D(unit.Position, this.direction, 50f);
			Vector3 predictedPosition = targetManager.Target.GetPredictedPosition(this.iceBlast.GetReleaseFlyTime(unit.Position));
			if (position.Distance2D(predictedPosition, false) > v.Distance2D(predictedPosition, false))
			{
				return false;
			}
			if (!base.Ability.UseAbility(false, false))
			{
				return false;
			}
			base.Sleeper.Sleep(0.3f);
			base.OrbwalkSleeper.Sleep(0.1f);
			comboSleeper.Sleep(0.1f);
			return true;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0002ACC4 File Offset: 0x00028EC4
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!this.iceBlast.IsUsable)
			{
				return false;
			}
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
			predictionInput.Delay += this.iceBlast.GetReleaseFlyTime(targetManager.Target.Position);
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1)
			{
				return false;
			}
			if (!base.Ability.UseAbility(predictionOutput.CastPosition, false, false))
			{
				return false;
			}
			this.direction = Vector3Extensions.Extend2D(base.Owner.Position, predictionOutput.CastPosition, 9999f);
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.1f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0002ADA4 File Offset: 0x00028FA4
		protected override bool ChainStun(Unit9 target, bool invulnerability)
		{
			float num = invulnerability ? target.GetInvulnerabilityDuration() : target.GetImmobilityDuration();
			if (num <= 0f)
			{
				return false;
			}
			float num2 = base.Ability.GetHitTime(target) + this.iceBlast.GetReleaseFlyTime(target.Position);
			if (target.IsInvulnerable)
			{
				num2 -= 0.1f;
			}
			return num2 > num;
		}

		// Token: 0x04000530 RID: 1328
		private Vector3 direction;

		// Token: 0x04000531 RID: 1329
		private readonly IceBlast iceBlast;
	}
}
