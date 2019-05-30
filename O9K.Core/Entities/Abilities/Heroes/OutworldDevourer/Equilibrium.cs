using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.OutworldDevourer
{
	// Token: 0x02000309 RID: 777
	[AbilityId(AbilityId.obsidian_destroyer_equilibrium)]
	public class Equilibrium : ActiveAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000D6F RID: 3439 RVA: 0x0000BEFF File Offset: 0x0000A0FF
		public Equilibrium(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x0000BF1A File Offset: 0x0000A11A
		public string BuffModifierName { get; } = "modifier_obsidian_destroyer_equilibrium";

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0000BF22 File Offset: 0x0000A122
		public bool BuffsAlly { get; }

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x0000BF2A File Offset: 0x0000A12A
		public bool BuffsOwner { get; } = 1;
	}
}
