using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.ShadowShaman
{
	// Token: 0x020001DE RID: 478
	[AbilityId(AbilityId.shadow_shaman_ether_shock)]
	public class EtherShock : RangedAreaOfEffectAbility, INuke, IActiveAbility
	{
		// Token: 0x06000983 RID: 2435 RVA: 0x000088FF File Offset: 0x00006AFF
		public EtherShock(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "start_radius");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}
	}
}
