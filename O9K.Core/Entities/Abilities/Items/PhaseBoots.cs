using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000176 RID: 374
	[AbilityId(AbilityId.item_phase_boots)]
	public class PhaseBoots : ActiveAbility, ISpeedBuff, IBuff, IActiveAbility
	{
		// Token: 0x06000788 RID: 1928 RVA: 0x000070F4 File Offset: 0x000052F4
		public PhaseBoots(Ability baseAbility) : base(baseAbility)
		{
			this.bonusMoveSpeedData = new SpecialData(baseAbility, "phase_movement_speed");
			this.bonusMoveSpeedRangedData = new SpecialData(baseAbility, "phase_movement_speed_range");
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x00007131 File Offset: 0x00005331
		public string BuffModifierName { get; } = "modifier_item_phase_boots_active";

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00007139 File Offset: 0x00005339
		public bool BuffsAlly { get; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x00007141 File Offset: 0x00005341
		public bool BuffsOwner { get; } = 1;

		// Token: 0x0600078C RID: 1932 RVA: 0x00007149 File Offset: 0x00005349
		public float GetSpeedBuff(Unit9 unit)
		{
			return unit.Speed * (unit.IsRanged ? this.bonusMoveSpeedRangedData.GetValue(this.Level) : this.bonusMoveSpeedData.GetValue(this.Level)) / 100f;
		}

		// Token: 0x04000364 RID: 868
		private readonly SpecialData bonusMoveSpeedData;

		// Token: 0x04000365 RID: 869
		private readonly SpecialData bonusMoveSpeedRangedData;
	}
}
