using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200017F RID: 383
	[AbilityId(AbilityId.item_bloodthorn)]
	public class Bloodthorn : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x060007AE RID: 1966 RVA: 0x00007309 File Offset: 0x00005509
		public Bloodthorn(Ability baseAbility) : base(baseAbility)
		{
			this.amplifierData = new SpecialData(baseAbility, "silence_damage_percent");
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x00007344 File Offset: 0x00005544
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0000734C File Offset: 0x0000554C
		public string AmplifierModifierName { get; } = "modifier_bloodthorn_debuff";

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00007354 File Offset: 0x00005554
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0000735C File Offset: 0x0000555C
		public UnitState AppliesUnitState { get; } = 8L;

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00007364 File Offset: 0x00005564
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x0000736C File Offset: 0x0000556C
		public bool IsAmplifierPermanent { get; }

		// Token: 0x060007B5 RID: 1973 RVA: 0x00007374 File Offset: 0x00005574
		public float AmplifierValue(Unit9 target)
		{
			return this.amplifierData.GetValue(this.Level) / 100f;
		}

		// Token: 0x04000381 RID: 897
		private readonly SpecialData amplifierData;
	}
}
