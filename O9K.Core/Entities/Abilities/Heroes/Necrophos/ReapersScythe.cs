using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Necrophos
{
	// Token: 0x0200031E RID: 798
	[AbilityId(AbilityId.necrolyte_reapers_scythe)]
	public class ReapersScythe : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000DCB RID: 3531 RVA: 0x0000C31A File Offset: 0x0000A51A
		public ReapersScythe(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage_per_health");
			this.ActivationDelayData = new SpecialData(baseAbility, "stun_duration");
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0000C345 File Offset: 0x0000A545
		public override bool CanHitSpellImmuneEnemy { get; }

		// Token: 0x06000DCD RID: 3533 RVA: 0x00026F54 File Offset: 0x00025154
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			float num = remainingHealth ?? unit.Health;
			float value = this.DamageData.GetValue(this.Level);
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = (float)((int)((unit.MaximumHealth - num) * value));
			return damage;
		}
	}
}
