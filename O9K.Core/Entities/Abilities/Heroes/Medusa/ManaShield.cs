using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Medusa
{
	// Token: 0x02000331 RID: 817
	[AbilityId(AbilityId.medusa_mana_shield)]
	public class ManaShield : ToggleAbility, IShield, IHasDamageAmplify, IActiveAbility
	{
		// Token: 0x06000E02 RID: 3586 RVA: 0x000271BC File Offset: 0x000253BC
		public ManaShield(Ability baseAbility) : base(baseAbility)
		{
			this.amplifierData = new SpecialData(baseAbility, "absorption_tooltip");
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x0000C694 File Offset: 0x0000A894
		public UnitState AppliesUnitState { get; }

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x0000C69C File Offset: 0x0000A89C
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x0000C6A4 File Offset: 0x0000A8A4
		public string AmplifierModifierName { get; } = "modifier_medusa_mana_shield";

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0000C6AC File Offset: 0x0000A8AC
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		public bool IsAmplifierPermanent { get; }

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x0000C6C4 File Offset: 0x0000A8C4
		public string ShieldModifierName { get; } = "modifier_medusa_mana_shield";

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0000C6CC File Offset: 0x0000A8CC
		public bool ShieldsAlly { get; }

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x06000E0C RID: 3596 RVA: 0x0000C6DC File Offset: 0x0000A8DC
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return this.amplifierData.GetValue(this.Level) / -100f;
		}

		// Token: 0x0400074A RID: 1866
		private readonly SpecialData amplifierData;
	}
}
