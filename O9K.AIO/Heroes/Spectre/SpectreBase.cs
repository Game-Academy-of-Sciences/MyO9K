using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.Spectre
{
	// Token: 0x0200009C RID: 156
	[HeroId(HeroId.npc_dota_hero_spectre)]
	internal class SpectreBase : BaseHero
	{
		// Token: 0x0600030E RID: 782 RVA: 0x00002E76 File Offset: 0x00001076
		public SpectreBase(IContext9 context) : base(context)
		{
		}
	}
}
