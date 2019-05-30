using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Entity;
using O9K.Hud.Modules.Units.Abilities.HudEntities.Abilities;
using SharpDX;

namespace O9K.Hud.Modules.Units.Abilities.HudEntities.Units
{
	// Token: 0x02000022 RID: 34
	internal class HudUnit
	{
		// Token: 0x060000AC RID: 172 RVA: 0x0000263C File Offset: 0x0000083C
		public HudUnit(Unit9 unit)
		{
			this.Unit = unit;
			this.IsAlly = (EntityManager9.Owner.Team == unit.Team);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00002679 File Offset: 0x00000879
		public bool IsAlly { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00007828 File Offset: 0x00005A28
		public IEnumerable<HudAbility> Abilities
		{
			get
			{
				return from x in this.abilities
				where x.ShouldDisplay
				orderby x.Ability.BaseAbility.AbilitySlot
				select x;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00002681 File Offset: 0x00000881
		public IEnumerable<HudAbility> Items
		{
			get
			{
				return from x in this.items
				where x.ShouldDisplay
				select x;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000026AD File Offset: 0x000008AD
		public Unit9 Unit { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000026B5 File Offset: 0x000008B5
		public Vector2 HealthBarPosition
		{
			get
			{
				return this.Unit.HealthBarPosition;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000026C2 File Offset: 0x000008C2
		public Vector2 HealthBarSize
		{
			get
			{
				return this.Unit.HealthBarSize;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000026CF File Offset: 0x000008CF
		public bool IsValid
		{
			get
			{
				return this.Unit.IsValid && this.Unit.IsVisible && this.Unit.IsAlive;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00007884 File Offset: 0x00005A84
		public void AddAbility(Ability9 ability)
		{
			HudAbility item = new HudAbility(ability);
			if (ability.IsItem)
			{
				this.items.Add(item);
				return;
			}
			this.abilities.Add(item);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000078BC File Offset: 0x00005ABC
		public void RemoveAbility(Ability9 entity)
		{
			if (entity.IsItem)
			{
				HudAbility hudAbility = this.items.Find((HudAbility x) => x.Ability.Handle == entity.Handle);
				if (hudAbility != null)
				{
					this.items.Remove(hudAbility);
					return;
				}
			}
			else
			{
				HudAbility hudAbility2 = this.abilities.Find((HudAbility x) => x.Ability.Handle == entity.Handle);
				if (hudAbility2 != null)
				{
					this.abilities.Remove(hudAbility2);
				}
			}
		}

		// Token: 0x0400006D RID: 109
		private readonly List<HudAbility> abilities = new List<HudAbility>();

		// Token: 0x0400006E RID: 110
		private readonly List<HudAbility> items = new List<HudAbility>();
	}
}
