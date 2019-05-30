using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200019D RID: 413
	[AbilityId(AbilityId.item_shivas_guard)]
	public class ShivasGuard : AreaOfEffectAbility, IDebuff, INuke, IActiveAbility
	{
		// Token: 0x0600085C RID: 2140 RVA: 0x000221C8 File Offset: 0x000203C8
		public ShivasGuard(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "blast_radius");
			this.SpeedData = new SpecialData(baseAbility, "blast_speed");
			this.DamageData = new SpecialData(baseAbility, "blast_damage");
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x00007BCB File Offset: 0x00005DCB
		public override DamageType DamageType { get; } = 2;

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x00007BD3 File Offset: 0x00005DD3
		public string DebuffModifierName { get; } = "modifier_item_shivas_guard_blast";
	}
}
