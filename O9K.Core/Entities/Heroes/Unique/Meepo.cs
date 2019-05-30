using System;
using System.Linq;
using Ensage;
using Ensage.Abilities;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Heroes.Unique
{
	// Token: 0x020000CB RID: 203
	[HeroId(HeroId.npc_dota_hero_meepo)]
	public class Meepo : Hero9
	{
		// Token: 0x06000629 RID: 1577 RVA: 0x0002122C File Offset: 0x0001F42C
		public Meepo(Hero baseHero) : base(baseHero)
		{
			if (base.IsIllusion)
			{
				return;
			}
			DividedWeStand dividedWeStand = (DividedWeStand)base.BaseUnit.Spellbook.Spells.FirstOrDefault((Ability x) => x.Id == AbilityId.meepo_divided_we_stand);
			if (dividedWeStand == null)
			{
				return;
			}
			this.MeepoIndex = dividedWeStand.UnitIndex;
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x00006228 File Offset: 0x00004428
		public bool IsMainMeepo
		{
			get
			{
				return this.MeepoIndex == 0;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00006233 File Offset: 0x00004433
		public int MeepoIndex { get; } = -1;
	}
}
