using System;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;

namespace O9K.AIO.Heroes.QueenOfPain.Abilities
{
	// Token: 0x020000C6 RID: 198
	internal class SonicWave : NukeAbility
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x000032F0 File Offset: 0x000014F0
		public SonicWave(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00004224 File Offset: 0x00002424
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return base.CanHit(targetManager, comboMenu) && base.Ability.CanHit(targetManager.Target, targetManager.EnemyHeroes, this.TargetsToHit(comboMenu));
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000034BC File Offset: 0x000016BC
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new UsableAbilityHitCountMenu(base.Ability, simplifiedName);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000034CA File Offset: 0x000016CA
		public int TargetsToHit(IComboModeMenu comboMenu)
		{
			return comboMenu.GetAbilitySettingsMenu<UsableAbilityHitCountMenu>(this).HitCount;
		}
	}
}
