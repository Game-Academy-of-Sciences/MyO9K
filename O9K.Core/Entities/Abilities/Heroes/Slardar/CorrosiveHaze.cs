using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Slardar
{
	// Token: 0x020002D1 RID: 721
	[AbilityId(AbilityId.slardar_amplify_damage)]
	public class CorrosiveHaze : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000CAD RID: 3245 RVA: 0x0000B649 File Offset: 0x00009849
		public CorrosiveHaze(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x0000B65D File Offset: 0x0000985D
		public string DebuffModifierName { get; } = "modifier_slardar_amplify_damage";
	}
}
