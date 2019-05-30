using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.NaturesProphet
{
	// Token: 0x020001F9 RID: 505
	[AbilityId(AbilityId.furion_teleportation)]
	public class Teleportation : RangedAbility
	{
		// Token: 0x060009D6 RID: 2518 RVA: 0x00008DF4 File Offset: 0x00006FF4
		public Teleportation(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x00008E08 File Offset: 0x00007008
		public override float CastRange { get; } = 9999999f;
	}
}
