using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.Morphling
{
	// Token: 0x020000EC RID: 236
	[HeroId(HeroId.npc_dota_hero_morphling)]
	internal class MorphlingBase : BaseHero
	{
		// Token: 0x060004C8 RID: 1224 RVA: 0x00002E76 File Offset: 0x00001076
		public MorphlingBase(IContext9 context) : base(context)
		{
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000047A4 File Offset: 0x000029A4
		public override void CreateUnitManager()
		{
			base.UnitManager = new MorphlingUnitManager(this);
		}
	}
}
