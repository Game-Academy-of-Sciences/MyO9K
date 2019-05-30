using System;
using O9K.AIO.Abilities.Menus;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Heroes.StormSpirit.Abilities
{
	// Token: 0x0200009B RID: 155
	internal class BallLightningMenu : UsableAbilityMenu
	{
		// Token: 0x0600030B RID: 779 RVA: 0x00012790 File Offset: 0x00010990
		public BallLightningMenu(Ability9 ability, string simplifiedName) : base(ability, simplifiedName)
		{
			this.MaxCastRange = new MenuSlider("Max cast range", ability.DefaultName + "maxRange" + simplifiedName, 1500, 500, 5000, false);
			base.Menu.Add<MenuSlider>(this.MaxCastRange);
			this.MaxDamageCombo = new MenuSwitcher("Max damage", ability.DefaultName + "damage" + simplifiedName, false, false);
			this.MaxDamageCombo.SetTooltip("Will spam ultimate for maximum damage output (not recommended with low mana pool)");
			base.Menu.Add<MenuSwitcher>(this.MaxDamageCombo);
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00003ED7 File Offset: 0x000020D7
		public MenuSlider MaxCastRange { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00003EDF File Offset: 0x000020DF
		public MenuSwitcher MaxDamageCombo { get; }
	}
}
