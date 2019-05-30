using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Sven
{
	// Token: 0x020002B7 RID: 695
	[AbilityId(AbilityId.sven_storm_bolt)]
	public class StormHammer : RangedAreaOfEffectAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000C4F RID: 3151 RVA: 0x0000B103 File Offset: 0x00009303
		public StormHammer(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "bolt_aoe");
			this.SpeedData = new SpecialData(baseAbility, "bolt_speed");
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0000B137 File Offset: 0x00009337
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x0000B13F File Offset: 0x0000933F
		public bool IsDispelActive
		{
			get
			{
				Ability abilityById = base.Owner.GetAbilityById(AbilityId.special_bonus_unique_sven_3);
				return abilityById != null && abilityById.Level > 0u;
			}
		}
	}
}
