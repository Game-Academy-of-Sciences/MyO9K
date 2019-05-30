using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Puck
{
	// Token: 0x020001F3 RID: 499
	[AbilityId(AbilityId.puck_waning_rift)]
	public class WaningRift : AreaOfEffectAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x060009C8 RID: 2504 RVA: 0x00008D17 File Offset: 0x00006F17
		public WaningRift(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00008D4A File Offset: 0x00006F4A
		public UnitState AppliesUnitState { get; } = 8L;
	}
}
