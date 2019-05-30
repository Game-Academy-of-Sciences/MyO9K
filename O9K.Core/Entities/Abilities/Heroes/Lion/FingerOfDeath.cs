using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Lion
{
	// Token: 0x0200020E RID: 526
	[AbilityId(AbilityId.lion_finger_of_death)]
	public class FingerOfDeath : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000A15 RID: 2581 RVA: 0x00023A84 File Offset: 0x00021C84
		public FingerOfDeath(Ability baseAbility) : base(baseAbility)
		{
			this.ActivationDelayData = new SpecialData(baseAbility, "damage_delay");
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.damagePerKillData = new SpecialData(baseAbility, "damage_per_kill");
			this.scepterDamageData = new SpecialData(baseAbility, "damage_scepter");
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00023ADC File Offset: 0x00021CDC
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Modifier modifier = base.Owner.GetModifier("modifier_lion_finger_of_death_kill_counter");
			int num = (modifier != null) ? modifier.StackCount : 0;
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = (float)num * this.damagePerKillData.GetValue(this.Level);
			Damage damage2 = damage;
			if (base.Owner.HasAghanimsScepter)
			{
				Damage damage3 = damage2;
				damageType = this.DamageType;
				damage3[damageType] += this.scepterDamageData.GetValue(this.Level);
			}
			else
			{
				Damage damage3 = damage2;
				damageType = this.DamageType;
				damage3[damageType] += this.DamageData.GetValue(this.Level);
			}
			return damage2;
		}

		// Token: 0x0400051B RID: 1307
		private readonly SpecialData damagePerKillData;

		// Token: 0x0400051C RID: 1308
		private readonly SpecialData scepterDamageData;
	}
}
