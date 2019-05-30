using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Heroes.Terrorblade
{
	// Token: 0x020001D0 RID: 464
	[AbilityId(AbilityId.terrorblade_sunder)]
	public class Sunder : RangedAbility, IHealthRestore, IActiveAbility
	{
		// Token: 0x06000951 RID: 2385 RVA: 0x00008666 File Offset: 0x00006866
		public Sunder(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x00008688 File Offset: 0x00006888
		public bool InstantHealthRestore { get; } = 1;

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00008690 File Offset: 0x00006890
		public string HealModifierName { get; } = string.Empty;

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x00008698 File Offset: 0x00006898
		public bool RestoresAlly { get; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x000086A0 File Offset: 0x000068A0
		public bool RestoresOwner { get; } = 1;

		// Token: 0x06000956 RID: 2390 RVA: 0x0000372C File Offset: 0x0000192C
		public int HealthRestoreValue(Unit9 unit)
		{
			return 0;
		}
	}
}
