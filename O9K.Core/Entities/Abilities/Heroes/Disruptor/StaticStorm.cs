using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Disruptor
{
	// Token: 0x02000245 RID: 581
	[AbilityId(AbilityId.disruptor_static_storm)]
	public class StaticStorm : CircleAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000AB1 RID: 2737 RVA: 0x00009A28 File Offset: 0x00007C28
		public StaticStorm(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00009A4D File Offset: 0x00007C4D
		public override float ActivationDelay { get; } = 0.25f;

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00009A55 File Offset: 0x00007C55
		public UnitState AppliesUnitState
		{
			get
			{
				if (base.Owner.HasAghanimsScepter)
				{
					return UnitState.Silenced | UnitState.Muted;
				}
				return UnitState.Silenced;
			}
		}
	}
}
