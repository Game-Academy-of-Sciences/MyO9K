using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Omniknight
{
	// Token: 0x02000310 RID: 784
	[AbilityId(AbilityId.omniknight_repel)]
	public class HeavenlyGrace : RangedAbility, IShield, IActiveAbility
	{
		// Token: 0x06000D96 RID: 3478 RVA: 0x0000C039 File Offset: 0x0000A239
		public HeavenlyGrace(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x0000C05B File Offset: 0x0000A25B
		public string ShieldModifierName { get; } = "modifier_omniknight_repel";

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0000C063 File Offset: 0x0000A263
		public UnitState AppliesUnitState { get; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x0000C06B File Offset: 0x0000A26B
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0000C073 File Offset: 0x0000A273
		public bool ShieldsOwner { get; } = 1;
	}
}
