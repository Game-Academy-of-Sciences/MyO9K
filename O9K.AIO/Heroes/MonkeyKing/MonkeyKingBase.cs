using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.MonkeyKing
{
	// Token: 0x020000F9 RID: 249
	[HeroId(HeroId.npc_dota_hero_monkey_king)]
	internal class MonkeyKingBase : BaseHero
	{
		// Token: 0x060004F5 RID: 1269 RVA: 0x00002E76 File Offset: 0x00001076
		public MonkeyKingBase(IContext9 context) : base(context)
		{
		}
	}
}
