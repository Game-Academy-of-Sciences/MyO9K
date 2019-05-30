using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Chen
{
	// Token: 0x02000258 RID: 600
	[AbilityId(AbilityId.chen_holy_persuasion)]
	public class HolyPersuasion : RangedAbility, IShield, IActiveAbility
	{
		// Token: 0x06000AF4 RID: 2804 RVA: 0x00009E5D File Offset: 0x0000805D
		public HolyPersuasion(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x00009E78 File Offset: 0x00008078
		public override bool TargetsEnemy { get; }

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00009E80 File Offset: 0x00008080
		public UnitState AppliesUnitState { get; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x00009E88 File Offset: 0x00008088
		public string ShieldModifierName { get; } = "modifier_chen_test_of_faith_teleport";

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00009E90 File Offset: 0x00008090
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x00009E98 File Offset: 0x00008098
		public bool ShieldsOwner { get; }
	}
}
