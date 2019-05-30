using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Sven
{
	// Token: 0x020002B6 RID: 694
	[AbilityId(AbilityId.sven_gods_strength)]
	public class GodsStrength : ActiveAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000C4B RID: 3147 RVA: 0x0000B0D0 File Offset: 0x000092D0
		public GodsStrength(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x0000B0EB File Offset: 0x000092EB
		public string BuffModifierName { get; } = "modifier_sven_gods_strength";

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x0000B0F3 File Offset: 0x000092F3
		public bool BuffsAlly { get; }

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x0000B0FB File Offset: 0x000092FB
		public bool BuffsOwner { get; } = 1;
	}
}
