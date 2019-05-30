using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.MonkeyKing
{
	// Token: 0x02000329 RID: 809
	[AbilityId(AbilityId.monkey_king_tree_dance)]
	public class TreeDance : RangedAbility, IBlink, IActiveAbility
	{
		// Token: 0x06000DEE RID: 3566 RVA: 0x0000C513 File Offset: 0x0000A713
		public TreeDance(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "leap_speed");
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x0000C534 File Offset: 0x0000A734
		public override bool TargetsEnemy { get; }

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0000C53C File Offset: 0x0000A73C
		public BlinkType BlinkType { get; } = 2;
	}
}
