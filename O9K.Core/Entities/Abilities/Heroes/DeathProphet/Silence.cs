using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.DeathProphet
{
	// Token: 0x02000249 RID: 585
	[AbilityId(AbilityId.death_prophet_silence)]
	public class Silence : CircleAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000AB8 RID: 2744 RVA: 0x00009AB1 File Offset: 0x00007CB1
		public Silence(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x00009AD3 File Offset: 0x00007CD3
		public UnitState AppliesUnitState { get; } = 8L;
	}
}
