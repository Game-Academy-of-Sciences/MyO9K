using System;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Entity;

namespace O9K.Core.Entities.Heroes.Unique
{
	// Token: 0x020000CD RID: 205
	[HeroId(HeroId.npc_dota_hero_morphling)]
	public class Morphling : Hero9
	{
		// Token: 0x0600062F RID: 1583 RVA: 0x000061C6 File Offset: 0x000043C6
		public Morphling(Hero baseHero) : base(baseHero)
		{
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00006256 File Offset: 0x00004456
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0000625E File Offset: 0x0000445E
		public bool IsMorphed { get; private set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00006267 File Offset: 0x00004467
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x0000626F File Offset: 0x0000446F
		public Unit9 MorphedHero { get; private set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00006278 File Offset: 0x00004478
		internal override float BaseAttackRange
		{
			get
			{
				if (this.IsMorphed)
				{
					Unit9 morphedHero = this.MorphedHero;
					if (morphedHero != null && morphedHero.IsValid)
					{
						return this.MorphedHero.BaseAttackRange;
					}
				}
				return base.BaseAttackRange;
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x000062A8 File Offset: 0x000044A8
		internal void Morphed(bool added)
		{
			base.IsRanged = base.BaseUnit.IsRanged;
			this.IsMorphed = added;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000212A0 File Offset: 0x0001F4A0
		internal void Replicated(bool added)
		{
			if (!added)
			{
				this.IsMorphed = false;
				this.MorphedHero = null;
				return;
			}
			Modifier modifier = base.BaseModifiers.FirstOrDefault((Modifier x) => x.Name == "modifier_morphling_replicate_manager");
			if (modifier == null)
			{
				return;
			}
			string heroName = modifier.TextureName;
			this.MorphedHero = EntityManager9.Heroes.FirstOrDefault((Hero9 x) => x.Name == heroName && x.Id != this.Id && !x.IsIllusion);
		}
	}
}
