using System;
using Ensage;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Heroes.Unique
{
	// Token: 0x020000D6 RID: 214
	[HeroId(HeroId.npc_dota_hero_wisp)]
	internal class Io : Hero9
	{
		// Token: 0x06000657 RID: 1623 RVA: 0x000061C6 File Offset: 0x000043C6
		public Io(Hero baseHero) : base(baseHero)
		{
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00004993 File Offset: 0x00002B93
		public override float GetTurnTime(float angleRad)
		{
			return 0f;
		}
	}
}
