using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Silencer
{
	// Token: 0x020001DA RID: 474
	[AbilityId(AbilityId.silencer_global_silence)]
	public class GlobalSilence : AreaOfEffectAbility, IDisable, IActiveAbility
	{
		// Token: 0x0600097A RID: 2426 RVA: 0x0000886E File Offset: 0x00006A6E
		public GlobalSilence(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0000888A File Offset: 0x00006A8A
		public UnitState AppliesUnitState { get; } = 8L;

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00008892 File Offset: 0x00006A92
		public override float Radius { get; } = 9999999f;
	}
}
