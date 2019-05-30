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
	// Token: 0x02000152 RID: 338
	[AbilityId(AbilityId.item_invis_sword)]
	public class ShadowBlade : ActiveAbility, ISpeedBuff, IBuff, IActiveAbility
	{
		// Token: 0x06000719 RID: 1817 RVA: 0x0002196C File Offset: 0x0001FB6C
		public ShadowBlade(Ability baseAbility) : base(baseAbility)
		{
			base.IsInvisibility = true;
			this.ActivationDelayData = new SpecialData(baseAbility, "windwalk_fade_time");
			this.bonusMoveSpeedData = new SpecialData(baseAbility, "windwalk_movement_speed");
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00006B7C File Offset: 0x00004D7C
		public string BuffModifierName { get; } = "modifier_item_invisibility_edge_windwalk";

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x00006B84 File Offset: 0x00004D84
		public bool BuffsAlly { get; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00006B8C File Offset: 0x00004D8C
		public bool BuffsOwner { get; } = 1;

		// Token: 0x0600071D RID: 1821 RVA: 0x00006B94 File Offset: 0x00004D94
		public float GetSpeedBuff(Unit9 unit)
		{
			return unit.Speed * this.bonusMoveSpeedData.GetValue(this.Level) / 100f;
		}

		// Token: 0x04000325 RID: 805
		private readonly SpecialData bonusMoveSpeedData;
	}
}
