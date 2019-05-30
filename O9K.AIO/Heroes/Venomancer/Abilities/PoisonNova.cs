using System;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;

namespace O9K.AIO.Heroes.Venomancer.Abilities
{
	// Token: 0x0200005B RID: 91
	internal class PoisonNova : DebuffAbility
	{
		// Token: 0x060001EB RID: 491 RVA: 0x000034DD File Offset: 0x000016DD
		public PoisonNova(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00003573 File Offset: 0x00001773
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return base.CanHit(targetManager, comboMenu) && base.Ability.CanHit(targetManager.Target, targetManager.EnemyHeroes, this.TargetsToHit(comboMenu));
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000034BC File Offset: 0x000016BC
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new UsableAbilityHitCountMenu(base.Ability, simplifiedName);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000034CA File Offset: 0x000016CA
		public int TargetsToHit(IComboModeMenu comboMenu)
		{
			return comboMenu.GetAbilitySettingsMenu<UsableAbilityHitCountMenu>(this).HitCount;
		}
	}
}
