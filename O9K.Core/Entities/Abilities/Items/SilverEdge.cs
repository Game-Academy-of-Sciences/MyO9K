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
	// Token: 0x02000153 RID: 339
	[AbilityId(AbilityId.item_silver_edge)]
	public class SilverEdge : ActiveAbility, ISpeedBuff, IBuff, IActiveAbility
	{
		// Token: 0x0600071E RID: 1822 RVA: 0x000219BC File Offset: 0x0001FBBC
		public SilverEdge(Ability baseAbility) : base(baseAbility)
		{
			base.IsInvisibility = true;
			this.ActivationDelayData = new SpecialData(baseAbility, "windwalk_fade_time");
			this.bonusMoveSpeedData = new SpecialData(baseAbility, "windwalk_movement_speed");
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00006BB4 File Offset: 0x00004DB4
		public string BuffModifierName { get; } = "modifier_item_silver_edge_windwalk";

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x00006BBC File Offset: 0x00004DBC
		public bool BuffsAlly { get; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x00006BC4 File Offset: 0x00004DC4
		public bool BuffsOwner { get; } = 1;

		// Token: 0x06000722 RID: 1826 RVA: 0x00006BCC File Offset: 0x00004DCC
		public float GetSpeedBuff(Unit9 unit)
		{
			return unit.Speed * this.bonusMoveSpeedData.GetValue(this.Level) / 100f;
		}

		// Token: 0x04000329 RID: 809
		private readonly SpecialData bonusMoveSpeedData;
	}
}
