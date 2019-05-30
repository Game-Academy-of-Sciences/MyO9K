using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Range;

namespace O9K.Core.Entities.Abilities.Heroes.TrollWarlord
{
	// Token: 0x02000298 RID: 664
	[AbilityId(AbilityId.troll_warlord_berserkers_rage)]
	public class BerserkersRage : ToggleAbility, ISpeedBuff, IBuff, IHasRangeIncrease, IActiveAbility
	{
		// Token: 0x06000BC6 RID: 3014 RVA: 0x00025228 File Offset: 0x00023428
		public BerserkersRage(Ability baseAbility) : base(baseAbility)
		{
			this.attackRange = new SpecialData(baseAbility, "bonus_range");
			this.bonusMoveSpeedData = new SpecialData(baseAbility, "bonus_move_speed");
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x0000A9D4 File Offset: 0x00008BD4
		public bool IsRangeIncreasePermanent { get; }

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0000A9DC File Offset: 0x00008BDC
		public float RangeIncrease
		{
			get
			{
				return -this.attackRange.GetValue(this.Level);
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0000A9F0 File Offset: 0x00008BF0
		public RangeIncreaseType RangeIncreaseType { get; } = 2;

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x0000A9F8 File Offset: 0x00008BF8
		public string RangeModifierName { get; } = "modifier_troll_warlord_berserkers_rage";

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x0000AA00 File Offset: 0x00008C00
		public string BuffModifierName { get; } = "modifier_troll_warlord_berserkers_rage";

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0000AA08 File Offset: 0x00008C08
		public bool BuffsAlly { get; }

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x0000AA10 File Offset: 0x00008C10
		public bool BuffsOwner { get; } = 1;

		// Token: 0x06000BCE RID: 3022 RVA: 0x0000AA18 File Offset: 0x00008C18
		public float GetSpeedBuff(Unit9 unit)
		{
			return this.bonusMoveSpeedData.GetValue(this.Level);
		}

		// Token: 0x04000605 RID: 1541
		private readonly SpecialData attackRange;

		// Token: 0x04000606 RID: 1542
		private readonly SpecialData bonusMoveSpeedData;
	}
}
