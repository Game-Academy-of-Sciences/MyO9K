using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Riki
{
	// Token: 0x020002E7 RID: 743
	[AbilityId(AbilityId.riki_smoke_screen)]
	public class SmokeScreen : CircleAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000CEF RID: 3311 RVA: 0x0000B9C9 File Offset: 0x00009BC9
		public SmokeScreen(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0000B9EB File Offset: 0x00009BEB
		public UnitState AppliesUnitState { get; } = 8L;
	}
}
