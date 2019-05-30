using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Units.AncientThunderhide
{
	// Token: 0x020000F9 RID: 249
	[AbilityId(AbilityId.big_thunder_lizard_frenzy)]
	public class Frenzy : RangedAbility, IBuff, IActiveAbility
	{
		// Token: 0x0600068A RID: 1674 RVA: 0x000066B7 File Offset: 0x000048B7
		public Frenzy(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x000066D9 File Offset: 0x000048D9
		public string BuffModifierName { get; } = "modifier_big_thunder_lizard_frenzy";

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x000066E1 File Offset: 0x000048E1
		public bool BuffsAlly { get; } = 1;

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x000066E9 File Offset: 0x000048E9
		public bool BuffsOwner { get; } = 1;
	}
}
