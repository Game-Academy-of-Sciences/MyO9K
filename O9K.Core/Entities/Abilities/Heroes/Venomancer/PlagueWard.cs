using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Venomancer
{
	// Token: 0x02000280 RID: 640
	[AbilityId(AbilityId.venomancer_plague_ward)]
	public class PlagueWard : CircleAbility
	{
		// Token: 0x06000B82 RID: 2946 RVA: 0x0000A5C9 File Offset: 0x000087C9
		public PlagueWard(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x0000A5DD File Offset: 0x000087DD
		public override float Radius { get; } = 600f;
	}
}
