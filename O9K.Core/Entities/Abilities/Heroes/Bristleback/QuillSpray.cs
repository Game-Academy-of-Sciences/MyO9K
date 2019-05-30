using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Bristleback
{
	// Token: 0x020003AE RID: 942
	[AbilityId(AbilityId.bristleback_quill_spray)]
	public class QuillSpray : AreaOfEffectAbility, INuke, IActiveAbility
	{
		// Token: 0x06000FE6 RID: 4070 RVA: 0x00028890 File Offset: 0x00026A90
		public QuillSpray(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "quill_base_damage");
			this.stackDamageData = new SpecialData(baseAbility, "quill_stack_damage");
			this.maxDamageData = new SpecialData(baseAbility, "max_damage");
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x000288FC File Offset: 0x00026AFC
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			float value = this.DamageData.GetValue(this.Level);
			float value2 = this.stackDamageData.GetValue(this.Level);
			int modifierStacks = unit.GetModifierStacks("modifier_bristleback_quill_spray");
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = Math.Min(value + (float)modifierStacks * value2, this.maxDamageData.GetValue(this.Level));
			return damage;
		}

		// Token: 0x04000844 RID: 2116
		private readonly SpecialData maxDamageData;

		// Token: 0x04000845 RID: 2117
		private readonly SpecialData stackDamageData;
	}
}
