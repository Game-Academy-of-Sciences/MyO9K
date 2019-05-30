using System;
using O9K.AIO.Abilities.Menus;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Heroes.Kunkka.Abilities
{
	// Token: 0x02000132 RID: 306
	internal class XMarkOnlyMenu : UsableAbilityMenu
	{
		// Token: 0x06000619 RID: 1561 RVA: 0x000052BF File Offset: 0x000034BF
		public XMarkOnlyMenu(Ability9 ability, string simplifiedName) : base(ability, simplifiedName)
		{
			this.XMarkOnly = new MenuSwitcher("Use only with X mark", ability.DefaultName + "xMark" + simplifiedName, true, false);
			base.Menu.Add<MenuSwitcher>(this.XMarkOnly);
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x000052FE File Offset: 0x000034FE
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x00005306 File Offset: 0x00003506
		public MenuSwitcher XMarkOnly { get; private set; }
	}
}
