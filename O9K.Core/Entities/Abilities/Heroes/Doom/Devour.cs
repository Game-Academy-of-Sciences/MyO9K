using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Doom
{
	// Token: 0x02000394 RID: 916
	[AbilityId(AbilityId.doom_bringer_devour)]
	public class Devour : RangedAbility
	{
		// Token: 0x06000F96 RID: 3990 RVA: 0x00006527 File Offset: 0x00004727
		public Devour(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x0000DBF5 File Offset: 0x0000BDF5
		public override bool TargetsEnemy { get; }
	}
}
