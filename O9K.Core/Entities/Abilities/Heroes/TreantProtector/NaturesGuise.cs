using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.TreantProtector
{
	// Token: 0x0200029C RID: 668
	[AbilityId(AbilityId.treant_natures_guise)]
	public class NaturesGuise : PassiveAbility
	{
		// Token: 0x06000BDD RID: 3037 RVA: 0x0000AADA File Offset: 0x00008CDA
		public NaturesGuise(Ability baseAbility) : base(baseAbility)
		{
			base.IsInvisibility = true;
		}
	}
}
