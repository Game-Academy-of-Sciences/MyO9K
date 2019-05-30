using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000180 RID: 384
	[AbilityId(AbilityId.item_buckler)]
	public class Buckler : AreaOfEffectAbility, IShield, IActiveAbility
	{
		// Token: 0x060007B6 RID: 1974 RVA: 0x0000738D File Offset: 0x0000558D
		public Buckler(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "bonus_aoe_radius");
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x000073C0 File Offset: 0x000055C0
		public UnitState AppliesUnitState { get; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x000073C8 File Offset: 0x000055C8
		public string ShieldModifierName { get; } = "modifier_item_buckler_effect";

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x000073D0 File Offset: 0x000055D0
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x000073D8 File Offset: 0x000055D8
		public bool ShieldsOwner { get; } = 1;
	}
}
