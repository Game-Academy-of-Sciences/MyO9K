using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Brewmaster
{
	// Token: 0x020003B3 RID: 947
	[AbilityId(AbilityId.brewmaster_thunder_clap)]
	public class ThunderClap : AreaOfEffectAbility, INuke, IActiveAbility
	{
		// Token: 0x06000FF2 RID: 4082 RVA: 0x00006555 File Offset: 0x00004755
		public ThunderClap(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}
	}
}
