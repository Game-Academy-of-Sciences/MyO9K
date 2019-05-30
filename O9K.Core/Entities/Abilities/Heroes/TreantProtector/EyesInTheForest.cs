using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.TreantProtector
{
	// Token: 0x0200029D RID: 669
	[AbilityId(AbilityId.treant_eyes_in_the_forest)]
	public class EyesInTheForest : RangedAbility
	{
		// Token: 0x06000BDE RID: 3038 RVA: 0x00006527 File Offset: 0x00004727
		public EyesInTheForest(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0000AAEA File Offset: 0x00008CEA
		public override bool TargetsEnemy { get; }
	}
}
