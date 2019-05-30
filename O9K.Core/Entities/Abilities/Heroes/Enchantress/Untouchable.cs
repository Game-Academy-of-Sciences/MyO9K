using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Enchantress
{
	// Token: 0x0200037C RID: 892
	[AbilityId(AbilityId.enchantress_untouchable)]
	public class Untouchable : PassiveAbility
	{
		// Token: 0x06000F4A RID: 3914 RVA: 0x0000D812 File Offset: 0x0000BA12
		public Untouchable(Ability baseAbility) : base(baseAbility)
		{
			this.attackSpeedSlowData = new SpecialData(baseAbility, "slow_attack_speed");
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x0000D82C File Offset: 0x0000BA2C
		public float AttackSpeedSlow
		{
			get
			{
				return this.attackSpeedSlowData.GetValue(this.Level);
			}
		}

		// Token: 0x040007E8 RID: 2024
		private readonly SpecialData attackSpeedSlowData;
	}
}
