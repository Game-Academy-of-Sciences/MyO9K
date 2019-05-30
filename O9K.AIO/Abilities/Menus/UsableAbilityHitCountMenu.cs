using System;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Abilities.Menus
{
	// Token: 0x02000208 RID: 520
	internal class UsableAbilityHitCountMenu : UsableAbilityMenu
	{
		// Token: 0x06000A61 RID: 2657 RVA: 0x0002CBC4 File Offset: 0x0002ADC4
		public UsableAbilityHitCountMenu(Ability9 ability, string simplifiedName) : base(ability, simplifiedName)
		{
			this.HitCount = new MenuSlider("Enemy count", ability.DefaultName + "hitCount" + simplifiedName, 2, 1, 5, false);
			this.HitCount.Tooltip = "Use ability only if it will hit equals/more enemies";
			base.Menu.Add<MenuSlider>(this.HitCount);
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x0000738D File Offset: 0x0000558D
		public MenuSlider HitCount { get; }
	}
}
