using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Pugna
{
	// Token: 0x020001EC RID: 492
	[AbilityId(AbilityId.pugna_nether_blast)]
	public class NetherBlast : CircleAbility, INuke, IActiveAbility
	{
		// Token: 0x060009A9 RID: 2473 RVA: 0x00008B64 File Offset: 0x00006D64
		public NetherBlast(Ability baseAbility) : base(baseAbility)
		{
			this.ActivationDelayData = new SpecialData(baseAbility, "delay");
			this.DamageData = new SpecialData(baseAbility, "blast_damage");
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}
	}
}
