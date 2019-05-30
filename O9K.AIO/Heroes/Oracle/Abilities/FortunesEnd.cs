using System;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.Core.Entities.Abilities.Base;

namespace O9K.AIO.Heroes.Oracle.Abilities
{
	// Token: 0x0200004C RID: 76
	internal class FortunesEnd : NukeAbility
	{
		// Token: 0x060001AE RID: 430 RVA: 0x000032F0 File Offset: 0x000014F0
		public FortunesEnd(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000032F9 File Offset: 0x000014F9
		public bool FullChannelTime(ComboModeMenu comboModeMenu)
		{
			return comboModeMenu.GetAbilitySettingsMenu<FortunesEndMenu>(this).FullChannelTime;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000330C File Offset: 0x0000150C
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new FortunesEndMenu(base.Ability, simplifiedName);
		}
	}
}
