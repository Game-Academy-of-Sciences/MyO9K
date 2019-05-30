using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Lifestealer
{
	// Token: 0x02000351 RID: 849
	[AbilityId(AbilityId.life_stealer_open_wounds)]
	public class OpenWounds : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000E61 RID: 3681 RVA: 0x0000CAA1 File Offset: 0x0000ACA1
		public OpenWounds(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x0000CAB5 File Offset: 0x0000ACB5
		public string DebuffModifierName { get; } = "modifier_life_stealer_open_wounds";
	}
}
