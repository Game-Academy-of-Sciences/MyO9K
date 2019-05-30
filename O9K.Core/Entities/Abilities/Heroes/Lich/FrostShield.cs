using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Lich
{
	// Token: 0x02000213 RID: 531
	[AbilityId(AbilityId.lich_frost_shield)]
	public class FrostShield : RangedAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000A24 RID: 2596 RVA: 0x00023BD8 File Offset: 0x00021DD8
		public FrostShield(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x000092A2 File Offset: 0x000074A2
		public string BuffModifierName { get; } = "modifier_lich_frost_shield";

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x000092AA File Offset: 0x000074AA
		public bool BuffsAlly { get; } = 1;

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x000092B2 File Offset: 0x000074B2
		public bool BuffsOwner { get; } = 1;
	}
}
