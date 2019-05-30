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
	// Token: 0x020001BB RID: 443
	[AbilityId(AbilityId.zuus_thundergods_wrath)]
	public class ThundergodsWrath : AreaOfEffectAbility, INuke, IActiveAbility
	{
		// Token: 0x060008ED RID: 2285 RVA: 0x0000818D File Offset: 0x0000638D
		public ThundergodsWrath(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x000081B2 File Offset: 0x000063B2
		public override float Radius { get; } = 9999999f;

		// Token: 0x060008EF RID: 2287 RVA: 0x00022B4C File Offset: 0x00020D4C
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

		// Token: 0x060008F0 RID: 2288 RVA: 0x00022B8C File Offset: 0x00020D8C
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

		// Token: 0x0400046C RID: 1132
		private StaticField staticField;
	}
}
