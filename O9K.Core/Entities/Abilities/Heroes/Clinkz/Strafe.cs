using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Clinkz
{
	// Token: 0x02000255 RID: 597
	[AbilityId(AbilityId.clinkz_strafe)]
	public class Strafe : ActiveAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000AE7 RID: 2791 RVA: 0x00009DC1 File Offset: 0x00007FC1
		public Strafe(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x00009DDC File Offset: 0x00007FDC
		public string BuffModifierName { get; } = "modifier_clinkz_strafe";

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00009DE4 File Offset: 0x00007FE4
		public bool BuffsAlly { get; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00009DEC File Offset: 0x00007FEC
		public bool BuffsOwner { get; } = 1;
	}
}
