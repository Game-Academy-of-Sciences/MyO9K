using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.WraithKing;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Heroes.Unique
{
	// Token: 0x020000D7 RID: 215
	[HeroId(HeroId.npc_dota_hero_skeleton_king)]
	internal class WraithKing : Hero9
	{
		// Token: 0x06000659 RID: 1625 RVA: 0x000061C6 File Offset: 0x000043C6
		public WraithKing(Hero baseHero) : base(baseHero)
		{
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0000645D File Offset: 0x0000465D
		public override bool CanReincarnate
		{
			get
			{
				if (!base.CanReincarnate)
				{
					Reincarnate reincarnate = this.reincarnate;
					return reincarnate != null && reincarnate.CanBeCasted(true);
				}
				return true;
			}
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0000647B File Offset: 0x0000467B
		internal override void Ability(Ability9 ability, bool added)
		{
			base.Ability(ability, added);
			if (added && ability.Id == AbilityId.skeleton_king_reincarnation)
			{
				this.reincarnate = (Reincarnate)ability;
			}
		}

		// Token: 0x040002E3 RID: 739
		private Reincarnate reincarnate;
	}
}
