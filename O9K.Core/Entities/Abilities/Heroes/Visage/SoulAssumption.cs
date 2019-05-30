using System;
using Ensage;
using Ensage.SDK.Extensions;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Visage
{
	// Token: 0x02000279 RID: 633
	[AbilityId(AbilityId.visage_soul_assumption)]
	public class SoulAssumption : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000B6F RID: 2927 RVA: 0x00024EEC File Offset: 0x000230EC
		public SoulAssumption(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "bolt_speed");
			this.DamageData = new SpecialData(baseAbility, "soul_base_damage");
			this.bonusDamageData = new SpecialData(baseAbility, "soul_charge_damage");
			this.stackLimit = new SpecialData(baseAbility, "stack_limit");
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0000A4D2 File Offset: 0x000086D2
		public bool MaxCharges
		{
			get
			{
				Modifier modifier = base.Owner.GetModifier("modifier_visage_soul_assumption");
				return (float)((modifier != null) ? modifier.StackCount : 0) >= this.stackLimit.GetValue(this.Level);
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00024F44 File Offset: 0x00023144
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage rawDamage = base.GetRawDamage(unit, remainingHealth);
			float value = this.bonusDamageData.GetValue(this.Level);
			Modifier modifierByName = base.Owner.BaseUnit.GetModifierByName("modifier_visage_soul_assumption");
			int num = (modifierByName != null) ? modifierByName.StackCount : 0;
			Damage damage = rawDamage;
			DamageType damageType = this.DamageType;
			damage[damageType] += (float)num * value;
			return rawDamage;
		}

		// Token: 0x040005D9 RID: 1497
		public SpecialData bonusDamageData;

		// Token: 0x040005DA RID: 1498
		public SpecialData stackLimit;
	}
}
