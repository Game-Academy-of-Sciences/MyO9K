using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.PhantomLancer
{
	// Token: 0x020000DE RID: 222
	[HeroId(HeroId.npc_dota_hero_phantom_lancer)]
	internal class PhantomLancerBase : BaseHero
	{
		// Token: 0x06000481 RID: 1153 RVA: 0x00002E76 File Offset: 0x00001076
		public PhantomLancerBase(IContext9 context) : base(context)
		{
		}
	}
}
