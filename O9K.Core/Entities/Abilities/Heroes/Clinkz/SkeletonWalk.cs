using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Clinkz
{
	// Token: 0x02000254 RID: 596
	[AbilityId(AbilityId.clinkz_wind_walk)]
	public class SkeletonWalk : ActiveAbility, ISpeedBuff, IBuff, IActiveAbility
	{
		// Token: 0x06000AE2 RID: 2786 RVA: 0x00024A04 File Offset: 0x00022C04
		public SkeletonWalk(Ability baseAbility) : base(baseAbility)
		{
			base.IsInvisibility = true;
			this.ActivationDelayData = new SpecialData(baseAbility, "fade_time");
			this.bonusMoveSpeedData = new SpecialData(baseAbility, "move_speed_bonus_pct");
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x00009D89 File Offset: 0x00007F89
		public string BuffModifierName { get; } = "modifier_clinkz_wind_walk";

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00009D91 File Offset: 0x00007F91
		public bool BuffsAlly { get; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x00009D99 File Offset: 0x00007F99
		public bool BuffsOwner { get; } = 1;

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00009DA1 File Offset: 0x00007FA1
		public float GetSpeedBuff(Unit9 unit)
		{
			return unit.Speed * this.bonusMoveSpeedData.GetValue(this.Level) / 100f;
		}

		// Token: 0x04000581 RID: 1409
		private readonly SpecialData bonusMoveSpeedData;
	}
}
