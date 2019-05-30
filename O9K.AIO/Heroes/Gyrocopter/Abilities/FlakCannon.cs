using System;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;

namespace O9K.AIO.Heroes.Gyrocopter.Abilities
{
	// Token: 0x02000136 RID: 310
	internal class FlakCannon : UntargetableAbility
	{
		// Token: 0x0600062E RID: 1582 RVA: 0x00003F2C File Offset: 0x0000212C
		public FlakCannon(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00005347 File Offset: 0x00003547
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return base.CanHit(targetManager, comboMenu) && base.Ability.CanHit(targetManager.Target, targetManager.EnemyHeroes, this.TargetsToHit(comboMenu));
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000034BC File Offset: 0x000016BC
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new UsableAbilityHitCountMenu(base.Ability, simplifiedName);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000034CA File Offset: 0x000016CA
		public int TargetsToHit(IComboModeMenu comboMenu)
		{
			return comboMenu.GetAbilitySettingsMenu<UsableAbilityHitCountMenu>(this).HitCount;
		}
	}
}
