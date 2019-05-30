using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Techies
{
	// Token: 0x020001D3 RID: 467
	[AbilityId(AbilityId.techies_remote_mines)]
	public class RemoteMines : CircleAbility
	{
		// Token: 0x06000959 RID: 2393 RVA: 0x00022FC4 File Offset: 0x000211C4
		public RemoteMines(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.ActivationDelayData = new SpecialData(baseAbility, "detonate_delay");
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.scepterDamageData = new SpecialData(baseAbility, "damage_scepter");
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0002301C File Offset: 0x0002121C
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

		// Token: 0x040004B0 RID: 1200
		private readonly SpecialData scepterDamageData;
	}
}
