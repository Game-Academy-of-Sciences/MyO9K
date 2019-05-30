using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Brewmaster.Spirits
{
	// Token: 0x020003B7 RID: 951
	[AbilityId(AbilityId.brewmaster_earth_hurl_boulder)]
	public class HurlBoulder : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000FF8 RID: 4088 RVA: 0x0000E110 File Offset: 0x0000C310
		public HurlBoulder(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.SpeedData = new SpecialData(baseAbility, "speed");
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x0000E144 File Offset: 0x0000C344
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
