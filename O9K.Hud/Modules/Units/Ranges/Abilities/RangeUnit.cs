using System;
using System.Collections.Generic;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;

namespace O9K.Hud.Modules.Units.Ranges.Abilities
{
	// Token: 0x02000012 RID: 18
	internal class RangeUnit : IDisposable
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00006078 File Offset: 0x00004278
		public RangeUnit(Unit9 unit, Menu menu)
		{
			this.unit = unit;
			this.Handle = unit.Handle;
			this.heroMenu = menu.GetOrAdd<Menu>(new Menu(unit.DisplayName, unit.DefaultName + unit.IsMyHero.ToString())).SetTexture(unit.DefaultName);
			this.toggler = this.heroMenu.GetOrAdd<Menu>(new Menu("Enabled", "abilities")).GetOrAdd<MenuAbilityToggler>(new MenuAbilityToggler(string.Empty, "enabled", null, false, false));
			this.toggler.ValueChange += this.TogglerOnValueChange;
			this.AddAttackRange();
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000023FE File Offset: 0x000005FE
		public uint Handle { get; }

		// Token: 0x06000068 RID: 104 RVA: 0x00006138 File Offset: 0x00004338
		public void AddAbility(Ability9 ability)
		{
			AbilityRange abilityRange = new AbilityRange(ability);
			this.abilities.Add(abilityRange);
			bool flag;
			if (!this.toggler.AllAbilities.TryGetValue(ability.Name, out flag))
			{
				this.toggler.AddAbility(ability.Name, null);
				return;
			}
			if (flag)
			{
				abilityRange.Enable(this.heroMenu);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000619C File Offset: 0x0000439C
		public void Dispose()
		{
			foreach (IRange range in this.abilities)
			{
				range.Dispose();
			}
			this.toggler.ValueChange -= this.TogglerOnValueChange;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00006204 File Offset: 0x00004404
		public void RemoveAbility(Ability9 ability)
		{
			IRange range = this.abilities.Find((IRange x) => x.Handle == ability.Handle);
			if (range == null)
			{
				return;
			}
			range.Dispose();
			this.abilities.Remove(range);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00006250 File Offset: 0x00004450
		public void UpdateRanges()
		{
			foreach (IRange range in this.abilities)
			{
				if (range.IsEnabled)
				{
					range.UpdateRange();
				}
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000062AC File Offset: 0x000044AC
		private void AddAttackRange()
		{
			AttackRange item = new AttackRange(this.unit);
			this.abilities.Add(item);
			this.toggler.AddAbility("attack", null);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000062EC File Offset: 0x000044EC
		private void TogglerOnValueChange(object sender, AbilityEventArgs e)
		{
			IRange range = this.abilities.Find((IRange x) => x.Name == e.Ability);
			if (range == null)
			{
				return;
			}
			if (e.NewValue)
			{
				range.Enable(this.heroMenu);
				return;
			}
			range.Dispose();
		}

		// Token: 0x0400003A RID: 58
		private readonly List<IRange> abilities = new List<IRange>();

		// Token: 0x0400003B RID: 59
		private readonly Menu heroMenu;

		// Token: 0x0400003C RID: 60
		private readonly MenuAbilityToggler toggler;

		// Token: 0x0400003D RID: 61
		private readonly Unit9 unit;
	}
}
