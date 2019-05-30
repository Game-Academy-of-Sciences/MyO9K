using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000173 RID: 371
	[AbilityId(AbilityId.item_infused_raindrop)]
	public class InfusedRaindrop : PassiveAbility, IHasDamageBlock
	{
		// Token: 0x06000775 RID: 1909 RVA: 0x00006FCC File Offset: 0x000051CC
		public InfusedRaindrop(Ability baseAbility) : base(baseAbility)
		{
			this.blockData = new SpecialData(baseAbility, "magic_damage_block");
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x00006FFF File Offset: 0x000051FF
		public bool BlocksDamageAfterReduction { get; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x00007007 File Offset: 0x00005207
		public DamageType BlockDamageType { get; } = 2;

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0000700F File Offset: 0x0000520F
		public string BlockModifierName { get; } = "modifier_item_infused_raindrop";

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00007017 File Offset: 0x00005217
		public bool IsDamageBlockPermanent { get; } = 1;

		// Token: 0x0600077A RID: 1914 RVA: 0x0000701F File Offset: 0x0000521F
		public float BlockValue(Unit9 target)
		{
			if (!this.CanBeCasted(true))
			{
				return 0f;
			}
			return this.blockData.GetValue(this.Level);
		}

		// Token: 0x04000356 RID: 854
		private readonly SpecialData blockData;
	}
}
