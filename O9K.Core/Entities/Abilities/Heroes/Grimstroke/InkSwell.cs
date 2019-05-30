using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Grimstroke
{
	// Token: 0x02000374 RID: 884
	[AbilityId(AbilityId.grimstroke_spirit_walk)]
	public class InkSwell : RangedAbility, IShield, IActiveAbility
	{
		// Token: 0x06000F36 RID: 3894 RVA: 0x0000D6C2 File Offset: 0x0000B8C2
		public InkSwell(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x0000D6F5 File Offset: 0x0000B8F5
		public string ShieldModifierName { get; } = "modifier_grimstroke_spirit_walk_buff";

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x0000D6FD File Offset: 0x0000B8FD
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x0000D705 File Offset: 0x0000B905
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0000D70D File Offset: 0x0000B90D
		public UnitState AppliesUnitState { get; }
	}
}
