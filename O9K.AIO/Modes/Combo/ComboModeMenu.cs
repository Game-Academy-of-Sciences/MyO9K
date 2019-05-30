using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.KeyPress;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Modes.Combo
{
	// Token: 0x02000029 RID: 41
	internal class ComboModeMenu : KeyPressModeMenu, IComboModeMenu
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x0000A87C File Offset: 0x00008A7C
		public ComboModeMenu(Menu rootMenu, string displayName) : base(rootMenu, displayName, null)
		{
			this.IsHarassCombo = (displayName == "Harass");
			this.SettingsMenu = new Menu("Settings", "comboSettings" + base.SimplifiedName);
			base.Menu.Add<Menu>(this.SettingsMenu);
			this.Attack = new MenuSwitcher("Attack", "comboAttack" + base.SimplifiedName, true, true);
			this.Attack.Tooltip = "Attack with hero";
			this.SettingsMenu.Add<MenuSwitcher>(this.Attack);
			this.Move = new MenuSwitcher("Move", "comboMove" + base.SimplifiedName, true, true);
			this.Move.Tooltip = "Move with hero";
			this.SettingsMenu.Add<MenuSwitcher>(this.Move);
			this.IgnoreInvisibility = new MenuSwitcher("Ignore invisibility", "comboInvis" + base.SimplifiedName, true, true);
			this.IgnoreInvisibility.Tooltip = "Use abilities when hero is invisible";
			this.SettingsMenu.Add<MenuSwitcher>(this.IgnoreInvisibility);
			this.comboAbilityToggler = new MenuAbilityToggler("Abilities", "abilities" + base.SimplifiedName, null, true, true);
			base.Menu.Add<MenuAbilityToggler>(this.comboAbilityToggler);
			this.comboItemsToggler = new MenuAbilityToggler("Items", "items" + base.SimplifiedName, null, true, true);
			base.Menu.Add<MenuAbilityToggler>(this.comboItemsToggler);
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00002A18 File Offset: 0x00000C18
		public bool IsHarassCombo { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00002A20 File Offset: 0x00000C20
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00002A28 File Offset: 0x00000C28
		public MenuSwitcher Attack { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00002A31 File Offset: 0x00000C31
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00002A39 File Offset: 0x00000C39
		public MenuSwitcher Move { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00002A42 File Offset: 0x00000C42
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00002A4A File Offset: 0x00000C4A
		public MenuSwitcher IgnoreInvisibility { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00002A53 File Offset: 0x00000C53
		protected Menu SettingsMenu { get; }

		// Token: 0x060000E9 RID: 233 RVA: 0x0000AA18 File Offset: 0x00008C18
		public void AddComboAbility(UsableAbility ability)
		{
			this.AddAbilitySettingsMenu(ability);
			if (ability.Ability.IsItem)
			{
				this.AddComboItem(ability.Ability);
				return;
			}
			this.comboAbilityToggler.AddAbility(ability.Ability.Id, null);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000AA68 File Offset: 0x00008C68
		public void AddComboAbility(Ability9 ability)
		{
			if (ability.IsItem)
			{
				this.AddComboItem(ability);
				return;
			}
			this.comboAbilityToggler.AddAbility(ability.Id, null);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00002A5B File Offset: 0x00000C5B
		public T GetAbilitySettingsMenu<T>(UsableAbility ability) where T : UsableAbilityMenu
		{
			return (T)((object)this.abilitySettings[ability.Ability.Id]);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00002A78 File Offset: 0x00000C78
		public bool IsAbilityEnabled(IActiveAbility ability)
		{
			if (ability.IsItem)
			{
				return this.comboItemsToggler.IsEnabled(ability.Name);
			}
			return this.comboAbilityToggler.IsEnabled(ability.Name);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000AAA0 File Offset: 0x00008CA0
		private void AddAbilitySettingsMenu(UsableAbility ability)
		{
			if (this.abilitySettings.ContainsKey(ability.Ability.Id))
			{
				return;
			}
			UsableAbilityMenu abilityMenu = ability.GetAbilityMenu(base.SimplifiedName);
			if (abilityMenu == null)
			{
				return;
			}
			this.SettingsMenu.Add<Menu>(abilityMenu.Menu);
			this.abilitySettings.Add(ability.Ability.Id, abilityMenu);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000AB00 File Offset: 0x00008D00
		private void AddComboItem(Ability9 ability)
		{
			this.comboItemsToggler.AddAbility(ability.Id, null);
		}

		// Token: 0x0400007F RID: 127
		private readonly Dictionary<AbilityId, UsableAbilityMenu> abilitySettings = new Dictionary<AbilityId, UsableAbilityMenu>();

		// Token: 0x04000080 RID: 128
		private readonly MenuAbilityToggler comboAbilityToggler;

		// Token: 0x04000081 RID: 129
		private readonly MenuAbilityToggler comboItemsToggler;
	}
}
