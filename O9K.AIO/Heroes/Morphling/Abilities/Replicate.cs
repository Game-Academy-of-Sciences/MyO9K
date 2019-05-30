using System;
using O9K.AIO.Abilities;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.Morphling;

namespace O9K.AIO.Heroes.Morphling.Abilities
{
	// Token: 0x020000F7 RID: 247
	internal class Replicate : TargetableAbility
	{
		// Token: 0x060004F1 RID: 1265 RVA: 0x000048FF File Offset: 0x00002AFF
		public Replicate(ActiveAbility ability) : base(ability)
		{
			this.baseReplicate = (Morph)ability;
		}

		// Token: 0x040002BD RID: 701
		private Morph baseReplicate;
	}
}
