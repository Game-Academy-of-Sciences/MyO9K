using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200017E RID: 382
	[AbilityId(AbilityId.item_blade_mail)]
	public class BladeMail : ActiveAbility, IShield, IActiveAbility
	{
		// Token: 0x060007A9 RID: 1961 RVA: 0x000072CE File Offset: 0x000054CE
		public BladeMail(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x000072E9 File Offset: 0x000054E9
		public UnitState AppliesUnitState { get; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x000072F1 File Offset: 0x000054F1
		public string ShieldModifierName { get; } = "modifier_item_blade_mail_reflect";

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x000072F9 File Offset: 0x000054F9
		public bool ShieldsAlly { get; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x00007301 File Offset: 0x00005501
		public bool ShieldsOwner { get; } = 1;
	}
}
