using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.AncientApparition
{
	// Token: 0x02000265 RID: 613
	[AbilityId(AbilityId.ancient_apparition_ice_blast_release)]
	public class IceBlastRelease : ActiveAbility
	{
		// Token: 0x06000B23 RID: 2851 RVA: 0x0000A118 File Offset: 0x00008318
		public IceBlastRelease(Ability baseAbility) : base(baseAbility)
		{
			base.IsUltimate = false;
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0000A133 File Offset: 0x00008333
		public override float Speed { get; } = 750f;
	}
}
