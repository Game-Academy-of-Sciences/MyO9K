using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.Venomancer
{
	// Token: 0x02000057 RID: 87
	[HeroId(HeroId.npc_dota_hero_venomancer)]
	internal class VenomancerBase : BaseHero
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x00002E76 File Offset: 0x00001076
		public VenomancerBase(IContext9 context) : base(context)
		{
		}
	}
}
