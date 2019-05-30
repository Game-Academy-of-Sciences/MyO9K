using System;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.Core.Entities.Abilities.Base.Components.Base;

namespace O9K.AIO.Modes.Combo
{
	// Token: 0x0200002A RID: 42
	internal interface IComboModeMenu
	{
		// Token: 0x060000EF RID: 239
		T GetAbilitySettingsMenu<T>(UsableAbility ability) where T : UsableAbilityMenu;

		// Token: 0x060000F0 RID: 240
		bool IsAbilityEnabled(IActiveAbility ability);
	}
}
