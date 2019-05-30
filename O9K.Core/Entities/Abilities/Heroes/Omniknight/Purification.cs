using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Omniknight
{
	// Token: 0x0200030F RID: 783
	[AbilityId(AbilityId.omniknight_purification)]
	public class Purification : RangedAreaOfEffectAbility, IHealthRestore, INuke, IActiveAbility
	{
		// Token: 0x06000D90 RID: 3472 RVA: 0x00026DFC File Offset: 0x00024FFC
		public Purification(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "heal");
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x0000C019 File Offset: 0x0000A219
		public bool InstantHealthRestore { get; } = 1;

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x0000C021 File Offset: 0x0000A221
		public string HealModifierName { get; } = string.Empty;

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x0000C029 File Offset: 0x0000A229
		public bool RestoresAlly { get; } = 1;

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x0000C031 File Offset: 0x0000A231
		public bool RestoresOwner { get; } = 1;

		// Token: 0x06000D95 RID: 3477 RVA: 0x00007F76 File Offset: 0x00006176
		public int HealthRestoreValue(Unit9 unit)
		{
			return (int)this.DamageData.GetValue(this.Level);
		}
	}
}
