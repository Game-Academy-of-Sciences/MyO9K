using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.NaturesProphet
{
	// Token: 0x020001FB RID: 507
	[AbilityId(AbilityId.furion_wrath_of_nature)]
	public class WrathOfNature : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x060009DA RID: 2522 RVA: 0x00008E29 File Offset: 0x00007029
		public WrathOfNature(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.scepterDamageData = new SpecialData(baseAbility, "damage_scepter");
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00008E5F File Offset: 0x0000705F
		public override float CastRange { get; } = 9999999f;

		// Token: 0x060009DC RID: 2524 RVA: 0x00023770 File Offset: 0x00021970
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			if (base.Owner.HasAghanimsScepter)
			{
				Damage damage = new Damage();
				DamageType damageType = this.DamageType;
				damage[damageType] = this.scepterDamageData.GetValue(this.Level);
				return damage;
			}
			return base.GetRawDamage(unit, remainingHealth);
		}

		// Token: 0x040004FA RID: 1274
		private readonly SpecialData scepterDamageData;
	}
}
