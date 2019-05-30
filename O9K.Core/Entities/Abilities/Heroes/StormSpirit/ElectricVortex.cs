using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.StormSpirit
{
	// Token: 0x020002BC RID: 700
	[AbilityId(AbilityId.storm_spirit_electric_vortex)]
	public class ElectricVortex : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000C64 RID: 3172 RVA: 0x0000B295 File Offset: 0x00009495
		public ElectricVortex(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x0000B2A7 File Offset: 0x000094A7
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00007F8A File Offset: 0x0000618A
		public override bool NoTargetCast
		{
			get
			{
				return base.Owner.HasAghanimsScepter;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0000996E File Offset: 0x00007B6E
		public override bool UnitTargetCast
		{
			get
			{
				return !this.NoTargetCast;
			}
		}
	}
}
