using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Lich
{
	// Token: 0x02000210 RID: 528
	[AbilityId(AbilityId.lich_chain_frost)]
	public class ChainFrost : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000A1C RID: 2588 RVA: 0x000091EC File Offset: 0x000073EC
		public ChainFrost(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.scepterDamageData = new SpecialData(baseAbility, "damage_scepter");
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00023B90 File Offset: 0x00021D90
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

		// Token: 0x0400051F RID: 1311
		private readonly SpecialData scepterDamageData;
	}
}
