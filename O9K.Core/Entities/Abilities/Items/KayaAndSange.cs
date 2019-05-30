using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200012F RID: 303
	[AbilityId(AbilityId.item_kaya_and_sange)]
	public class KayaAndSange : PassiveAbility, IHasDamageAmplify
	{
		// Token: 0x060006E0 RID: 1760 RVA: 0x00006971 File Offset: 0x00004B71
		public KayaAndSange(Ability baseAbility) : base(baseAbility)
		{
			this.amplifierData = new SpecialData(baseAbility, "spell_amp");
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x000069AB File Offset: 0x00004BAB
		public DamageType AmplifierDamageType { get; } = 2;

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x000069B3 File Offset: 0x00004BB3
		public string AmplifierModifierName { get; } = "modifier_item_kaya_and_sange";

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x000069BB File Offset: 0x00004BBB
		public AmplifiesDamage AmplifiesDamage { get; } = 2;

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x000069C3 File Offset: 0x00004BC3
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x000069CB File Offset: 0x00004BCB
		public bool IsAmplifierPermanent { get; } = 1;

		// Token: 0x060006E6 RID: 1766 RVA: 0x000069D3 File Offset: 0x00004BD3
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			if (!this.IsUsable)
			{
				return 0f;
			}
			return this.amplifierData.GetValue(this.Level) / 100f;
		}

		// Token: 0x0400030F RID: 783
		private readonly SpecialData amplifierData;
	}
}
