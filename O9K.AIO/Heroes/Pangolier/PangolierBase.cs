using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.Pangolier
{
	// Token: 0x020000E2 RID: 226
	[HeroId(HeroId.npc_dota_hero_pangolier)]
	internal class PangolierBase : BaseHero
	{
		// Token: 0x06000496 RID: 1174 RVA: 0x00002E76 File Offset: 0x00001076
		public PangolierBase(IContext9 context) : base(context)
		{
		}
	}
}
