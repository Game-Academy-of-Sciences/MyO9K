using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.MonkeyKing
{
	// Token: 0x0200032B RID: 811
	[AbilityId(AbilityId.monkey_king_boundless_strike)]
	public class BoundlessStrike : LineAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000DF2 RID: 3570 RVA: 0x0000C56F File Offset: 0x0000A76F
		public BoundlessStrike(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "strike_radius");
			this.DamageData = new SpecialData(baseAbility, "strike_crit_mult");
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x0000C5A3 File Offset: 0x0000A7A3
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0000C5AB File Offset: 0x0000A7AB
		public override bool IntelligenceAmplify { get; }

		// Token: 0x06000DF5 RID: 3573 RVA: 0x000251F0 File Offset: 0x000233F0
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			float physCritMultiplier = this.DamageData.GetValue(this.Level) / 100f;
			return base.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, physCritMultiplier, 0f);
		}
	}
}
