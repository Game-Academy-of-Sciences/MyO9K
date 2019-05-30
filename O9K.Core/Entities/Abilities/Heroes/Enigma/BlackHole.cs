using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Enigma
{
	// Token: 0x02000231 RID: 561
	[AbilityId(AbilityId.enigma_black_hole)]
	public class BlackHole : CircleAbility, IDisable, IChanneled, IActiveAbility
	{
		// Token: 0x06000A77 RID: 2679 RVA: 0x00009770 File Offset: 0x00007970
		public BlackHole(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "pull_radius");
			this.ChannelTime = baseAbility.GetChannelTime(0u);
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x000097A7 File Offset: 0x000079A7
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x000097AF File Offset: 0x000079AF
		public float ChannelTime { get; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x000097B7 File Offset: 0x000079B7
		public bool IsActivatesOnChannelStart { get; } = 1;
	}
}
