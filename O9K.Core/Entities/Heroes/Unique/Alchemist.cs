using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.Alchemist;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Heroes.Unique
{
	// Token: 0x020000CA RID: 202
	[HeroId(HeroId.npc_dota_hero_alchemist)]
	internal class Alchemist : Hero9
	{
		// Token: 0x06000625 RID: 1573 RVA: 0x000061C6 File Offset: 0x000043C6
		public Alchemist(Hero baseHero) : base(baseHero)
		{
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x000061CF File Offset: 0x000043CF
		protected override float BaseAttackTime
		{
			get
			{
				if (this.useRageAttackTime && this.chemicalRage != null)
				{
					return this.chemicalRage.AttackTime;
				}
				return base.BaseAttackTime;
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x000061F9 File Offset: 0x000043F9
		internal override void Ability(Ability9 ability, bool added)
		{
			base.Ability(ability, added);
			if (added && ability.Id == AbilityId.alchemist_chemical_rage)
			{
				this.chemicalRage = (ChemicalRage)ability;
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0000621F File Offset: 0x0000441F
		internal void Raged(bool value)
		{
			this.useRageAttackTime = value;
		}

		// Token: 0x040002CA RID: 714
		private ChemicalRage chemicalRage;

		// Token: 0x040002CB RID: 715
		private bool useRageAttackTime;
	}
}
