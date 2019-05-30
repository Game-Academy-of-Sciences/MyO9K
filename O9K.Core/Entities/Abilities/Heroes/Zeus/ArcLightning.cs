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
	// Token: 0x020001B7 RID: 439
	[AbilityId(AbilityId.zuus_arc_lightning)]
	public class ArcLightning : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x00008137 File Offset: 0x00006337
		public ArcLightning(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "arc_damage");
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x000228E0 File Offset: 0x00020AE0
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

		// Token: 0x060008E3 RID: 2275 RVA: 0x00022920 File Offset: 0x00020B20
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

		// Token: 0x04000467 RID: 1127
		private StaticField staticField;
	}
}
