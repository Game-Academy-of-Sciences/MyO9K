using System;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.Modes.KeyPress;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Modes.MoveCombo
{
	// Token: 0x02000022 RID: 34
	internal class MoveComboModeMenu : KeyPressModeMenu, IComboModeMenu
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00009F9C File Offset: 0x0000819C
		public MoveComboModeMenu(Menu rootMenu, string displayName) : base(rootMenu, displayName, null)
		{
			this.comboAbilityToggler = new MenuAbilityToggler("Abilities", "abilities" + base.SimplifiedName, null, true, true);
			base.Menu.Add<MenuAbilityToggler>(this.comboAbilityToggler);
			this.comboItemsToggler = new MenuAbilityToggler("Items", "items" + base.SimplifiedName, null, true, true);
			base.Menu.Add<MenuAbilityToggler>(this.comboItemsToggler);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000A01C File Offset: 0x0000821C
		public void AddComboAbility(UsableAbility ability)
		{
			if (ability.Ability.IsItem)
			{
				this.AddComboItem(ability);
				return;
			}
			this.comboAbilityToggler.AddAbility(ability.Ability.Id, null);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000A060 File Offset: 0x00008260
		public T GetAbilitySettingsMenu<T>(UsableAbility ability) where T : UsableAbilityMenu
		{
			return default(T);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000028D3 File Offset: 0x00000AD3
		public bool IsAbilityEnabled(IActiveAbility ability)
		{
			if (ability.IsItem)
			{
				return this.comboItemsToggler.IsEnabled(ability.Name);
			}
			return this.comboAbilityToggler.IsEnabled(ability.Name);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000A078 File Offset: 0x00008278
		private void AddComboItem(UsableAbility ability)
		{
			this.comboItemsToggler.AddAbility(ability.Ability.Id, null);
		}

		// Token: 0x0400006A RID: 106
		private readonly MenuAbilityToggler comboAbilityToggler;

		// Token: 0x0400006B RID: 107
		private readonly MenuAbilityToggler comboItemsToggler;
	}
}
