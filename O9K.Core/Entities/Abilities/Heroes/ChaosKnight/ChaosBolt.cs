using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.ChaosKnight
{
	// Token: 0x020003A3 RID: 931
	[AbilityId(AbilityId.chaos_knight_chaos_bolt)]
	public class ChaosBolt : RangedAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000FC4 RID: 4036 RVA: 0x00028678 File Offset: 0x00026878
		public ChaosBolt(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "chaos_bolt_speed");
			this.DamageData = new SpecialData(baseAbility, "damage_min");
			this.maxDamageData = new SpecialData(baseAbility, "damage_max");
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x0000DEC4 File Offset: 0x0000C0C4
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x06000FC6 RID: 4038 RVA: 0x000286C8 File Offset: 0x000268C8
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = (float)((int)((this.DamageData.GetValue(this.Level) + this.maxDamageData.GetValue(this.Level)) / 2f));
			return damage;
		}

		// Token: 0x0400082D RID: 2093
		private readonly SpecialData maxDamageData;
	}
}
