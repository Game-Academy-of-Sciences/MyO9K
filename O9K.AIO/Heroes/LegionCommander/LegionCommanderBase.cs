using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.LegionCommander
{
	// Token: 0x02000123 RID: 291
	[HeroId(HeroId.npc_dota_hero_legion_commander)]
	internal class LegionCommanderBase : BaseHero
	{
		// Token: 0x060005CC RID: 1484 RVA: 0x00002E76 File Offset: 0x00001076
		public LegionCommanderBase(IContext9 context) : base(context)
		{
		}
	}
}
