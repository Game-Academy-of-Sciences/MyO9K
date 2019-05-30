using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Talents
{
	// Token: 0x02000104 RID: 260
	[AbilityId(AbilityId.special_bonus_spell_block_15)]
	internal class SpellBlockTalent : Talent
	{
		// Token: 0x060006A3 RID: 1699 RVA: 0x00006825 File Offset: 0x00004A25
		public SpellBlockTalent(Ability baseAbility) : base(baseAbility)
		{
			this.spellBlockCooldownData = new SpecialData(baseAbility, "block_cooldown");
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0000683F File Offset: 0x00004A3F
		public float SpellBlockCooldown
		{
			get
			{
				return this.spellBlockCooldownData.GetValue(1u);
			}
		}

		// Token: 0x040002FD RID: 765
		private readonly SpecialData spellBlockCooldownData;
	}
}
