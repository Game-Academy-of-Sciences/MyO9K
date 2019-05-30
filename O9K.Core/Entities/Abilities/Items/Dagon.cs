using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000184 RID: 388
	[AbilityId(AbilityId.item_dagon)]
	[AbilityId(AbilityId.item_dagon_2)]
	[AbilityId(AbilityId.item_dagon_3)]
	[AbilityId(AbilityId.item_dagon_4)]
	[AbilityId(AbilityId.item_dagon_5)]
	public class Dagon : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x060007C7 RID: 1991 RVA: 0x0000749A File Offset: 0x0000569A
		public Dagon(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
			base.Name = "item_dagon_5";
			base.Id = AbilityId.item_dagon_5;
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x000074D1 File Offset: 0x000056D1
		public override DamageType DamageType { get; } = 2;
	}
}
