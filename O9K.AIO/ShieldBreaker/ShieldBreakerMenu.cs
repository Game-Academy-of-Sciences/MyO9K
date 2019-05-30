using System;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.ShieldBreaker
{
	// Token: 0x0200001E RID: 30
	internal class ShieldBreakerMenu
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00009E48 File Offset: 0x00008048
		public ShieldBreakerMenu(Menu rootMenu)
		{
			Menu menu = new Menu("Shield breaker");
			this.linkensAbilityToggler = new MenuAbilityToggler("Linken's sphere", "linkensAbilities", null, false, true);
			menu.Add<MenuAbilityToggler>(this.linkensAbilityToggler);
			this.spellShieldAbilityToggler = new MenuAbilityToggler("Spell shield", "spellShieldAbilities", null, false, true);
			menu.Add<MenuAbilityToggler>(this.spellShieldAbilityToggler);
			rootMenu.Add<Menu>(menu);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00009EB8 File Offset: 0x000080B8
		public void AddBreakerAbility(Ability9 ability)
		{
			this.linkensAbilityToggler.AddAbility(ability.Id, null);
			this.spellShieldAbilityToggler.AddAbility(ability.Id, null);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000027AF File Offset: 0x000009AF
		public bool IsLinkensBreakerEnabled(string abilityName)
		{
			return this.linkensAbilityToggler.IsEnabled(abilityName);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000027BD File Offset: 0x000009BD
		public bool IsSpellShieldBreakerEnabled(string abilityName)
		{
			return this.spellShieldAbilityToggler.IsEnabled(abilityName);
		}

		// Token: 0x04000063 RID: 99
		private readonly MenuAbilityToggler linkensAbilityToggler;

		// Token: 0x04000064 RID: 100
		private readonly MenuAbilityToggler spellShieldAbilityToggler;
	}
}
