using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.SpiritBreaker
{
	// Token: 0x020002BF RID: 703
	[AbilityId(AbilityId.spirit_breaker_bulldoze)]
	public class Bulldoze : ActiveAbility, ISpeedBuff, IBuff, IActiveAbility
	{
		// Token: 0x06000C6C RID: 3180 RVA: 0x0000B2ED File Offset: 0x000094ED
		public Bulldoze(Ability baseAbility) : base(baseAbility)
		{
			this.bonusMoveSpeedData = new SpecialData(baseAbility, "movement_speed");
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x0000B319 File Offset: 0x00009519
		public string BuffModifierName { get; } = "modifier_spirit_breaker_bulldoze";

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0000B321 File Offset: 0x00009521
		public bool BuffsAlly { get; }

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x0000B329 File Offset: 0x00009529
		public bool BuffsOwner { get; } = 1;

		// Token: 0x06000C70 RID: 3184 RVA: 0x0000B331 File Offset: 0x00009531
		public float GetSpeedBuff(Unit9 unit)
		{
			return unit.Speed * this.bonusMoveSpeedData.GetValue(this.Level) / 100f;
		}

		// Token: 0x04000663 RID: 1635
		private readonly SpecialData bonusMoveSpeedData;
	}
}
