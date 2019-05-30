using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.Warlock
{
	// Token: 0x02000053 RID: 83
	[HeroId(HeroId.npc_dota_hero_warlock)]
	internal class WarlockBase : BaseHero
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x00002E76 File Offset: 0x00001076
		public WarlockBase(IContext9 context) : base(context)
		{
		}
	}
}
