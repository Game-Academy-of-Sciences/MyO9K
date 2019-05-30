using System;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;

namespace O9K.AIO.Heroes.MonkeyKing.Abilities
{
	// Token: 0x02000100 RID: 256
	internal class WukongsCommand : AoeAbility
	{
		// Token: 0x06000517 RID: 1303 RVA: 0x0000356A File Offset: 0x0000176A
		public WukongsCommand(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001A610 File Offset: 0x00018810
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return base.CanHit(targetManager, comboMenu) && base.Owner.Distance(targetManager.Target) <= base.Ability.Radius && base.Ability.CanHit(targetManager.Target, targetManager.EnemyHeroes, this.TargetsToHit(comboMenu));
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000034BC File Offset: 0x000016BC
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new UsableAbilityHitCountMenu(base.Ability, simplifiedName);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000034CA File Offset: 0x000016CA
		public int TargetsToHit(IComboModeMenu comboMenu)
		{
			return comboMenu.GetAbilitySettingsMenu<UsableAbilityHitCountMenu>(this).HitCount;
		}
	}
}
