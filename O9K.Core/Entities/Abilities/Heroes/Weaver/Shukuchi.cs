using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Weaver
{
	// Token: 0x0200026D RID: 621
	[AbilityId(AbilityId.weaver_shukuchi)]
	public class Shukuchi : ActiveAbility, ISpeedBuff, IBuff, IActiveAbility
	{
		// Token: 0x06000B43 RID: 2883 RVA: 0x00024E18 File Offset: 0x00023018
		public Shukuchi(Ability baseAbility) : base(baseAbility)
		{
			base.IsInvisibility = true;
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.talentSpeedBonusData = new SpecialData(baseAbility.Owner, AbilityId.special_bonus_unique_weaver_2);
			this.ActivationDelayData = new SpecialData(baseAbility, "fade_time");
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0000A250 File Offset: 0x00008450
		public string BuffModifierName { get; } = "modifier_weaver_shukuchi";

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0000A258 File Offset: 0x00008458
		public bool BuffsAlly { get; }

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0000A260 File Offset: 0x00008460
		public bool BuffsOwner { get; } = 1;

		// Token: 0x06000B47 RID: 2887 RVA: 0x0000A268 File Offset: 0x00008468
		public float GetSpeedBuff(Unit9 unit)
		{
			return 550f + this.talentSpeedBonusData.GetValue(0u) - unit.Speed;
		}

		// Token: 0x040005BE RID: 1470
		private readonly SpecialData talentSpeedBonusData;
	}
}
