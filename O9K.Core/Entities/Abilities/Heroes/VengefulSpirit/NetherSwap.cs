using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.VengefulSpirit
{
	// Token: 0x02000285 RID: 645
	[AbilityId(AbilityId.vengefulspirit_nether_swap)]
	public class NetherSwap : RangedAbility, IShield, IActiveAbility
	{
		// Token: 0x06000B8C RID: 2956 RVA: 0x0000A68D File Offset: 0x0000888D
		public NetherSwap(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0000A6A8 File Offset: 0x000088A8
		public UnitState AppliesUnitState { get; }

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x0000A6B0 File Offset: 0x000088B0
		public string ShieldModifierName { get; } = string.Empty;

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0000A6B8 File Offset: 0x000088B8
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0000A6C0 File Offset: 0x000088C0
		public bool ShieldsOwner { get; }
	}
}
