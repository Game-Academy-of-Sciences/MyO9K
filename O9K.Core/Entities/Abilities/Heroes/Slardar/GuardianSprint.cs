using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Slardar
{
	// Token: 0x020002D2 RID: 722
	[AbilityId(AbilityId.slardar_sprint)]
	public class GuardianSprint : ActiveAbility, ISpeedBuff, IBuff, IActiveAbility
	{
		// Token: 0x06000CAF RID: 3247 RVA: 0x0000B665 File Offset: 0x00009865
		public GuardianSprint(Ability baseAbility) : base(baseAbility)
		{
			this.bonusMoveSpeedData = new SpecialData(baseAbility, "bonus_speed");
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0000B691 File Offset: 0x00009891
		public string BuffModifierName { get; } = "modifier_slardar_sprint";

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0000B699 File Offset: 0x00009899
		public bool BuffsAlly { get; }

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0000B6A1 File Offset: 0x000098A1
		public bool BuffsOwner { get; } = 1;

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0000B6A9 File Offset: 0x000098A9
		public float GetSpeedBuff(Unit9 unit)
		{
			return unit.Speed * this.bonusMoveSpeedData.GetValue(this.Level) / 100f;
		}

		// Token: 0x0400068B RID: 1675
		private readonly SpecialData bonusMoveSpeedData;
	}
}
