using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200015C RID: 348
	[AbilityId(AbilityId.item_tango)]
	[AbilityId(AbilityId.item_tango_single)]
	public class Tango : RangedAbility
	{
		// Token: 0x0600072F RID: 1839 RVA: 0x00006527 File Offset: 0x00004727
		public Tango(Ability ability) : base(ability)
		{
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x00006C3D File Offset: 0x00004E3D
		public override bool TargetsEnemy { get; }
	}
}
