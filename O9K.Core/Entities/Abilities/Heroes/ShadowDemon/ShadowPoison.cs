using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.ShadowDemon
{
	// Token: 0x020001E1 RID: 481
	[AbilityId(AbilityId.shadow_demon_shadow_poison)]
	public class ShadowPoison : LineAbility, IHarass, IActiveAbility
	{
		// Token: 0x0600098B RID: 2443 RVA: 0x000089A4 File Offset: 0x00006BA4
		public ShadowPoison(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.SpeedData = new SpecialData(baseAbility, "speed");
		}
	}
}
