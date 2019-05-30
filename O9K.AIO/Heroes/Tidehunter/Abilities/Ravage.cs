using System;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;

namespace O9K.AIO.Heroes.Tidehunter.Abilities
{
	// Token: 0x02000089 RID: 137
	internal class Ravage : DisableAbility
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x00003482 File Offset: 0x00001682
		public Ravage(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00003B9F File Offset: 0x00001D9F
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return base.CanHit(targetManager, comboMenu) && base.Ability.CanHit(targetManager.Target, targetManager.EnemyHeroes, this.TargetsToHit(comboMenu));
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x000034BC File Offset: 0x000016BC
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new UsableAbilityHitCountMenu(base.Ability, simplifiedName);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x000034CA File Offset: 0x000016CA
		public int TargetsToHit(IComboModeMenu comboMenu)
		{
			return comboMenu.GetAbilitySettingsMenu<UsableAbilityHitCountMenu>(this).HitCount;
		}
	}
}
