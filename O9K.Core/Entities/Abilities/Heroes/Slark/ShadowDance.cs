using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Slark
{
	// Token: 0x020002CF RID: 719
	[AbilityId(AbilityId.slark_shadow_dance)]
	public class ShadowDance : ActiveAbility, IShield, IActiveAbility
	{
		// Token: 0x06000CA7 RID: 3239 RVA: 0x0000B5FF File Offset: 0x000097FF
		public ShadowDance(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0000B629 File Offset: 0x00009829
		public UnitState AppliesUnitState { get; } = 17179869184L;

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0000B631 File Offset: 0x00009831
		public string ShieldModifierName { get; } = "modifier_slark_shadow_dance";

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0000B639 File Offset: 0x00009839
		public bool ShieldsAlly { get; }

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0000B641 File Offset: 0x00009841
		public bool ShieldsOwner { get; } = 1;
	}
}
