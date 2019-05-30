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

namespace O9K.Core.Entities.Abilities.Heroes.Earthshaker
{
	// Token: 0x02000240 RID: 576
	[AbilityId(AbilityId.earthshaker_fissure)]
	public class Fissure : LineAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000AA8 RID: 2728 RVA: 0x000099A7 File Offset: 0x00007BA7
		public Fissure(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "fissure_radius");
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x000099CA File Offset: 0x00007BCA
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x06000AAA RID: 2730 RVA: 0x00024660 File Offset: 0x00022860
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = base.GetRawDamage(unit, remainingHealth);
			Aftershock aftershock = this.aftershock;
			if (aftershock != null && aftershock.CanBeCasted(true) && base.Owner.Distance(unit) < this.aftershock.Radius)
			{
				damage += this.aftershock.GetRawDamage(unit, null);
			}
			return damage;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000246C4 File Offset: 0x000228C4
		internal override void SetOwner(Unit9 owner)
		{
			base.SetOwner(owner);
			Ability ability = EntityManager9.BaseAbilities.FirstOrDefault(delegate(Ability x)
			{
				if (x.Id == AbilityId.earthshaker_aftershock)
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
			this.aftershock = (Aftershock)EntityManager9.AddAbility(ability);
		}

		// Token: 0x04000567 RID: 1383
		private Aftershock aftershock;
	}
}
