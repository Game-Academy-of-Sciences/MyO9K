using System;
using System.Linq;
using Ensage.SDK.Extensions;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.Magnus.Abilities
{
	// Token: 0x02000115 RID: 277
	internal class Skewer : UsableAbility
	{
		// Token: 0x06000576 RID: 1398 RVA: 0x00003F23 File Offset: 0x00002123
		public Skewer(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00004DBC File Offset: 0x00002FBC
		public override bool CanBeCasted(TargetManager targetManager, bool channelingCheck, IComboModeMenu comboMenu)
		{
			if (!base.CanBeCasted(targetManager, channelingCheck, comboMenu))
			{
				return false;
			}
			this.ally = this.GetPreferedAlly(targetManager, comboMenu);
			return true;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00002E73 File Offset: 0x00001073
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return true;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00003880 File Offset: 0x00001A80
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			return false;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00004DDA File Offset: 0x00002FDA
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new SkewerMenu(base.Ability, simplifiedName);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0001BC24 File Offset: 0x00019E24
		public Unit9 GetPreferedAlly(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			SkewerMenu menu = comboMenu.GetAbilitySettingsMenu<SkewerMenu>(this);
			Unit9 unit = (from x in targetManager.AllyHeroes
			where !x.Equals(this.Owner) && menu.IsAllyEnabled(x.Name)
			orderby x.Distance(targetManager.Target)
			select x).FirstOrDefault((Unit9 x) => x.Distance(targetManager.Target) < this.Ability.CastRange + 600f);
			if (menu.SkewerToTower && unit == null)
			{
				unit = (from x in EntityManager9.Units
				where x.IsTower && x.IsAlly(this.Owner) && x.IsAlive
				orderby x.Distance(targetManager.Target)
				select x).FirstOrDefault((Unit9 x) => x.Distance(targetManager.Target) < this.Ability.CastRange + 500f);
			}
			return unit;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001BCE8 File Offset: 0x00019EE8
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			return target.IsStunned && target.IsVisible && !target.IsInvulnerable && (!target.IsMagicImmune || base.Ability.PiercesMagicImmunity(target));
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001BD34 File Offset: 0x00019F34
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (this.ally == null)
			{
				return false;
			}
			Unit9 target = targetManager.Target;
			if ((target.Position - this.ally.Position).AngleBetween(base.Owner.Position - target.Position) > 40f)
			{
				base.Owner.BaseUnit.Move(Vector3Extensions.Extend2D(this.ally.Position, target.Position, this.ally.Distance(target) + 100f));
				base.Sleeper.Sleep(0.1f);
				base.OrbwalkSleeper.Sleep(0.1f);
				return true;
			}
			if (!base.Ability.UseAbility(Vector3Extensions.Extend2D(this.ally.Position, base.Owner.Position, 200f), false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001BE54 File Offset: 0x0001A054
		public bool UseAbilityOnTarget(TargetManager targetManager, Sleeper comboSleeper)
		{
			if (!base.Ability.UseAbility(targetManager.Target, 1, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(base.Ability.GetHitTime(targetManager.Target));
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001BEC4 File Offset: 0x0001A0C4
		internal bool UseAbilityIfCondition(TargetManager targetManager, Sleeper comboSleeper, ComboModeMenu menu, ReversePolarity polarity, bool blink, bool force)
		{
			if (blink || force || polarity == null)
			{
				return false;
			}
			PredictionInput9 predictionInput = polarity.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
			predictionInput.Delay += base.Ability.CastPoint + 0.5f;
			predictionInput.Range += base.Ability.CastRange;
			predictionInput.CastRange = base.Ability.CastRange;
			predictionInput.SkillShotType = 4;
			PredictionOutput9 predictionOutput = polarity.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1 || predictionOutput.AoeTargetsHit.Count < polarity.TargetsToHit(menu))
			{
				return false;
			}
			Vector3 castPosition = predictionOutput.CastPosition;
			if (base.Owner.Distance(castPosition) > base.Ability.CastRange)
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(base.Ability.GetHitTime(targetManager.Target));
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return base.Ability.UseAbility(castPosition, false, false);
		}

		// Token: 0x0400030A RID: 778
		private Unit9 ally;
	}
}
