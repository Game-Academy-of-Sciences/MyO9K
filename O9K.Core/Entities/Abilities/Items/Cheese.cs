using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000182 RID: 386
	[AbilityId(AbilityId.item_cheese)]
	public class Cheese : ActiveAbility, IHealthRestore, IManaRestore, IActiveAbility
	{
		// Token: 0x060007BC RID: 1980 RVA: 0x000073E0 File Offset: 0x000055E0
		public Cheese(Ability baseAbility) : base(baseAbility)
		{
			this.healthRestoreData = new SpecialData(baseAbility, "health_restore");
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00007413 File Offset: 0x00005613
		public bool InstantHealthRestore { get; } = 1;

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0000741B File Offset: 0x0000561B
		public string HealModifierName { get; } = string.Empty;

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x00007423 File Offset: 0x00005623
		public bool RestoresAlly { get; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0000742B File Offset: 0x0000562B
		public bool RestoresOwner { get; } = 1;

		// Token: 0x060007C1 RID: 1985 RVA: 0x00007433 File Offset: 0x00005633
		public int HealthRestoreValue(Unit9 unit)
		{
			return (int)this.healthRestoreData.GetValue(this.Level);
		}

		// Token: 0x0400038C RID: 908
		private readonly SpecialData healthRestoreData;
	}
}
