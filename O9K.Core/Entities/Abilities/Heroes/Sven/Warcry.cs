using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Sven
{
	// Token: 0x020002B8 RID: 696
	[AbilityId(AbilityId.sven_warcry)]
	public class Warcry : AreaOfEffectAbility, ISpeedBuff, IBuff, IActiveAbility
	{
		// Token: 0x06000C52 RID: 3154 RVA: 0x00025754 File Offset: 0x00023954
		public Warcry(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.bonusMoveSpeedData = new SpecialData(baseAbility, "movespeed");
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0000B165 File Offset: 0x00009365
		public string BuffModifierName { get; } = "modifier_sven_warcry";

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x0000B16D File Offset: 0x0000936D
		public bool BuffsAlly { get; } = 1;

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x0000B175 File Offset: 0x00009375
		public bool BuffsOwner { get; } = 1;

		// Token: 0x06000C56 RID: 3158 RVA: 0x0000B17D File Offset: 0x0000937D
		public float GetSpeedBuff(Unit9 unit)
		{
			return unit.Speed * this.bonusMoveSpeedData.GetValue(this.Level) / 100f;
		}

		// Token: 0x04000656 RID: 1622
		private readonly SpecialData bonusMoveSpeedData;
	}
}
