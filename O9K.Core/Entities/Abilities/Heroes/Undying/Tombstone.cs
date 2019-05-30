using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Undying
{
	// Token: 0x020001A6 RID: 422
	[AbilityId(AbilityId.undying_tombstone)]
	public class Tombstone : CircleAbility, IDebuff, IActiveAbility
	{
		// Token: 0x0600088F RID: 2191 RVA: 0x00007DDD File Offset: 0x00005FDD
		public Tombstone(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x00007DF7 File Offset: 0x00005FF7
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x00007DFF File Offset: 0x00005FFF
		public string DebuffModifierName { get; set; }
	}
}
