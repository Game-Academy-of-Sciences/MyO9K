using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Brewmaster.Spirits
{
	// Token: 0x020003BA RID: 954
	[AbilityId(AbilityId.brewmaster_storm_wind_walk)]
	public class WindWalk : ActiveAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000FFC RID: 4092 RVA: 0x0000E14C File Offset: 0x0000C34C
		public WindWalk(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x0000E167 File Offset: 0x0000C367
		public string BuffModifierName { get; } = "modifier_brewmaster_storm_wind_walk";

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x0000E16F File Offset: 0x0000C36F
		public bool BuffsAlly { get; }

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x0000E177 File Offset: 0x0000C377
		public bool BuffsOwner { get; } = 1;
	}
}
