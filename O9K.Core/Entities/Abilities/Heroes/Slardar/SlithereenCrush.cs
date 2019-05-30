using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Slardar
{
	// Token: 0x020002D3 RID: 723
	[AbilityId(AbilityId.slardar_slithereen_crush)]
	public class SlithereenCrush : AreaOfEffectAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000CB4 RID: 3252 RVA: 0x0000B6C9 File Offset: 0x000098C9
		public SlithereenCrush(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "crush_radius");
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0000B6EC File Offset: 0x000098EC
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
