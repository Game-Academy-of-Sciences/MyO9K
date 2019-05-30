using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.NightStalker
{
	// Token: 0x020001F6 RID: 502
	[AbilityId(AbilityId.night_stalker_crippling_fear)]
	public class CripplingFear : AreaOfEffectAbility, IDisable, IActiveAbility
	{
		// Token: 0x060009CF RID: 2511 RVA: 0x00008D85 File Offset: 0x00006F85
		public CripplingFear(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00008DA7 File Offset: 0x00006FA7
		public UnitState AppliesUnitState { get; } = 8L;
	}
}
