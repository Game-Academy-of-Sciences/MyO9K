using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.CentaurWarrunner
{
	// Token: 0x020003A7 RID: 935
	[AbilityId(AbilityId.centaur_hoof_stomp)]
	public class HoofStomp : AreaOfEffectAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000FD5 RID: 4053 RVA: 0x0000DF7E File Offset: 0x0000C17E
		public HoofStomp(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "stomp_damage");
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x0000DFB2 File Offset: 0x0000C1B2
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
