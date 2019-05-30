using System;
using Ensage.SDK.Helpers;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Magnus.Abilities
{
	// Token: 0x02000112 RID: 274
	internal class ReversePolarity : DisableAbility
	{
		// Token: 0x06000566 RID: 1382 RVA: 0x00003482 File Offset: 0x00001682
		public ReversePolarity(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00004D0E File Offset: 0x00002F0E
		public void AddSkewer(Skewer skewer)
		{
			this.skewer = skewer;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00004D17 File Offset: 0x00002F17
		public override bool CanBeCasted(TargetManager targetManager, bool channelingCheck, IComboModeMenu comboMenu)
		{
			if (!base.CanBeCasted(targetManager, channelingCheck, comboMenu))
			{
				return false;
			}
			this.ally = this.skewer.GetPreferedAlly(targetManager, comboMenu);
			return true;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00004D3A File Offset: 0x00002F3A
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return base.CanHit(targetManager, comboMenu) && base.Ability.CanHit(targetManager.Target, targetManager.EnemyHeroes, this.TargetsToHit(comboMenu));
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001B9C8 File Offset: 0x00019BC8
		public bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper, ComboModeMenu comboModeMenu)
		{
			this.ally = this.skewer.GetPreferedAlly(targetManager, comboModeMenu);
			if (this.ally != null)
			{
				base.Owner.BaseUnit.Move(this.ally.Position);
				UpdateManager.BeginInvoke(delegate
				{
					base.Ability.UseAbility(false, false);
				}, 150);
			}
			else
			{
				base.Ability.UseAbility(false, false);
			}
			float num = base.Ability.GetHitTime(targetManager.Target) + 0.5f;
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			targetManager.Target.SetExpectedUnitState(base.Disable.AppliesUnitState, num);
			comboSleeper.Sleep(castDelay);
			base.OrbwalkSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(num);
			return true;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000034BC File Offset: 0x000016BC
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new UsableAbilityHitCountMenu(base.Ability, simplifiedName);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000034CA File Offset: 0x000016CA
		public int TargetsToHit(IComboModeMenu comboMenu)
		{
			return comboMenu.GetAbilitySettingsMenu<UsableAbilityHitCountMenu>(this).HitCount;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001BA9C File Offset: 0x00019C9C
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (this.ally != null)
			{
				base.Owner.BaseUnit.Move(this.ally.Position);
			}
			if (!base.Ability.UseAbility(false, false))
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

		// Token: 0x04000304 RID: 772
		private Unit9 ally;

		// Token: 0x04000305 RID: 773
		private Skewer skewer;
	}
}
