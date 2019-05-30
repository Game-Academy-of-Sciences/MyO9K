using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Batrider
{
	// Token: 0x0200025A RID: 602
	[AbilityId(AbilityId.batrider_firefly)]
	public class Firefly : ActiveAbility, ISpeedBuff, IBuff, IActiveAbility
	{
		// Token: 0x06000AFE RID: 2814 RVA: 0x00024AA4 File Offset: 0x00022CA4
		public Firefly(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "damage_per_second");
			this.bonusMoveSpeedData = new SpecialData(baseAbility, "movement_speed");
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00009EDA File Offset: 0x000080DA
		public string BuffModifierName { get; } = "modifier_batrider_firefly";

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x00009EE2 File Offset: 0x000080E2
		public bool BuffsAlly { get; }

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00009EEA File Offset: 0x000080EA
		public bool BuffsOwner { get; } = 1;

		// Token: 0x06000B02 RID: 2818 RVA: 0x00009EF2 File Offset: 0x000080F2
		public float GetSpeedBuff(Unit9 unit)
		{
			return unit.Speed * (this.bonusMoveSpeedData.GetValue(this.Level) / 100f);
		}

		// Token: 0x04000597 RID: 1431
		private readonly SpecialData bonusMoveSpeedData;
	}
}
