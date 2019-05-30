using System;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;

namespace O9K.AIO.Heroes.CrystalMaiden.Abilities
{
	// Token: 0x020001D2 RID: 466
	internal class FreezingField : NukeAbility
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x000032F0 File Offset: 0x000014F0
		public FreezingField(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00006ABE File Offset: 0x00004CBE
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return base.CanHit(targetManager, comboMenu) && base.Ability.CanHit(targetManager.Target, targetManager.EnemyHeroes, this.TargetsToHit(comboMenu));
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x000034BC File Offset: 0x000016BC
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new UsableAbilityHitCountMenu(base.Ability, simplifiedName);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x000034CA File Offset: 0x000016CA
		public int TargetsToHit(IComboModeMenu comboMenu)
		{
			return comboMenu.GetAbilitySettingsMenu<UsableAbilityHitCountMenu>(this).HitCount;
		}
	}
}
