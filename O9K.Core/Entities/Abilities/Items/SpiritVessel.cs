using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200019F RID: 415
	[AbilityId(AbilityId.item_spirit_vessel)]
	public class SpiritVessel : RangedAbility, IDebuff, IHealthRestore, IActiveAbility
	{
		// Token: 0x06000866 RID: 2150 RVA: 0x00007C31 File Offset: 0x00005E31
		public SpiritVessel(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x00007C65 File Offset: 0x00005E65
		public override DamageType DamageType { get; } = 2;

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x00007C6D File Offset: 0x00005E6D
		public override bool BreaksLinkens { get; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x00007C75 File Offset: 0x00005E75
		public bool InstantHealthRestore { get; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x00007C7D File Offset: 0x00005E7D
		public string DebuffModifierName { get; } = "modifier_item_spirit_vessel_damage";

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x00007C85 File Offset: 0x00005E85
		public string HealModifierName { get; } = "modifier_item_spirit_vessel_heal";

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x00007C8D File Offset: 0x00005E8D
		public bool RestoresAlly { get; } = 1;

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x00007C95 File Offset: 0x00005E95
		public bool RestoresOwner { get; } = 1;

		// Token: 0x0600086E RID: 2158 RVA: 0x0000754C File Offset: 0x0000574C
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return base.BaseItem.CurrentCharges > 0u && base.CanBeCasted(checkChanneling);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0000372C File Offset: 0x0000192C
		public int HealthRestoreValue(Unit9 unit)
		{
			return 0;
		}
	}
}
