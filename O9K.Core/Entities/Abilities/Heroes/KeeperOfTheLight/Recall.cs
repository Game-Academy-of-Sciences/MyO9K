using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.KeeperOfTheLight
{
	// Token: 0x0200021E RID: 542
	[AbilityId(AbilityId.keeper_of_the_light_recall)]
	public class Recall : RangedAbility
	{
		// Token: 0x06000A51 RID: 2641 RVA: 0x0000949E File Offset: 0x0000769E
		public Recall(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x000094B2 File Offset: 0x000076B2
		public override float CastRange { get; } = 9999999f;
	}
}
