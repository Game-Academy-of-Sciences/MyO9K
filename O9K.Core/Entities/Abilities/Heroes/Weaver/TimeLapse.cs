using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Weaver
{
	// Token: 0x0200026F RID: 623
	[AbilityId(AbilityId.weaver_time_lapse)]
	public class TimeLapse : ActiveAbility, IShield, IActiveAbility
	{
		// Token: 0x06000B4B RID: 2891 RVA: 0x0000A2CF File Offset: 0x000084CF
		public TimeLapse(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0000A2EA File Offset: 0x000084EA
		public UnitState AppliesUnitState { get; }

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x0000A2F2 File Offset: 0x000084F2
		public string ShieldModifierName { get; } = string.Empty;

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0000A2FA File Offset: 0x000084FA
		public bool ShieldsAlly { get; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0000A302 File Offset: 0x00008502
		public bool ShieldsOwner { get; } = 1;
	}
}
