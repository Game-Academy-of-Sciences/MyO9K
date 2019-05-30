using System;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;
using O9K.Core.Managers.Entity;

namespace O9K.Core.Entities.Abilities.Heroes.Zeus
{
	// Token: 0x020001B9 RID: 441
	[AbilityId(AbilityId.zuus_lightning_bolt)]
	public class LightningBolt : RangedAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x060008E6 RID: 2278 RVA: 0x00008162 File Offset: 0x00006362
		public LightningBolt(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "spread_aoe");
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x00008185 File Offset: 0x00006385
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x060008E8 RID: 2280 RVA: 0x000229FC File Offset: 0x00020BFC
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = base.GetRawDamage(unit, remainingHealth);
			StaticField staticField = this.staticField;
			if (staticField != null && staticField.CanBeCasted(true))
			{
				damage += this.staticField.GetRawDamage(unit, remainingHealth);
			}
			return damage;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00022A3C File Offset: 0x00020C3C
		internal Damage GetBaseDamage()
		{
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = this.DamageData.GetValue(this.Level);
			return damage;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00022A70 File Offset: 0x00020C70
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			Ability ability = EntityManager9.BaseAbilities.FirstOrDefault(delegate(Ability x)
			{
				if (x.Id == AbilityId.zuus_static_field)
				{
					Entity owner2 = x.Owner;
					EntityHandle? entityHandle = (owner2 != null) ? new EntityHandle?(owner2.Handle) : null;
					return ((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) == owner.Handle;
				}
				return false;
			});
			if (ability == null)
			{
				return;
			}
			this.staticField = (StaticField)EntityManager9.AddAbility(ability);
		}

		// Token: 0x04000469 RID: 1129
		private StaticField staticField;
	}
}
