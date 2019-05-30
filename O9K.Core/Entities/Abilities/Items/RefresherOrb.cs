using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000178 RID: 376
	[AbilityId(AbilityId.item_refresher)]
	[AbilityId(AbilityId.item_refresher_shard)]
	public class RefresherOrb : ActiveAbility
	{
		// Token: 0x06000794 RID: 1940 RVA: 0x00006683 File Offset: 0x00004883
		public RefresherOrb(Ability baseAbility) : base(baseAbility)
		{
		}
	}
}
