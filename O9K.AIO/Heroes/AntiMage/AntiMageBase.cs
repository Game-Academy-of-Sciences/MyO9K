using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.AntiMage
{
	// Token: 0x020001E9 RID: 489
	[HeroId(HeroId.npc_dota_hero_antimage)]
	internal class AntiMageBase : BaseHero
	{
		// Token: 0x060009B6 RID: 2486 RVA: 0x00002E76 File Offset: 0x00001076
		public AntiMageBase(IContext9 context) : base(context)
		{
		}
	}
}
