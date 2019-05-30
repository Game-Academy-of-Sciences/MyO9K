using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Units.CentaurConqueror
{
	// Token: 0x020000F6 RID: 246
	[AbilityId(AbilityId.centaur_khan_war_stomp)]
	public class WarStomp : AreaOfEffectAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000686 RID: 1670 RVA: 0x0000668C File Offset: 0x0000488C
		public WarStomp(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x000066AF File Offset: 0x000048AF
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
