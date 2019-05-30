using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Visage
{
	// Token: 0x02000278 RID: 632
	[AbilityId(AbilityId.visage_grave_chill)]
	public class GraveChill : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000B6D RID: 2925 RVA: 0x0000A4B6 File Offset: 0x000086B6
		public GraveChill(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0000A4CA File Offset: 0x000086CA
		public string DebuffModifierName { get; } = "modifier_visage_grave_chill_debuff";
	}
}
