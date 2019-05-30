using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200012A RID: 298
	[AbilityId(AbilityId.item_helm_of_the_dominator)]
	public class HelmOfOheDominator : RangedAbility
	{
		// Token: 0x060006DA RID: 1754 RVA: 0x00006527 File Offset: 0x00004727
		public HelmOfOheDominator(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x00006969 File Offset: 0x00004B69
		public override bool TargetsEnemy { get; }
	}
}
