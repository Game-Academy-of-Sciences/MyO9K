using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.Windranger
{
	// Token: 0x02000039 RID: 57
	[HeroId(HeroId.npc_dota_hero_windrunner)]
	internal class WindrangerBase : BaseHero
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00002E76 File Offset: 0x00001076
		public WindrangerBase(IContext9 context) : base(context)
		{
		}
	}
}
