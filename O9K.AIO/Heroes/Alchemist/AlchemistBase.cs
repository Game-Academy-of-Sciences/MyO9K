using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.Alchemist
{
	// Token: 0x020001F2 RID: 498
	[HeroId(HeroId.npc_dota_hero_alchemist)]
	internal class AlchemistBase : BaseHero
	{
		// Token: 0x060009DF RID: 2527 RVA: 0x00002E76 File Offset: 0x00001076
		public AlchemistBase(IContext9 context) : base(context)
		{
		}
	}
}
