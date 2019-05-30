using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000139 RID: 313
	[AbilityId(AbilityId.item_null_talisman)]
	public class NullTalisman : PassiveAbility, IHasDamageAmplify
	{
		// Token: 0x060006F6 RID: 1782 RVA: 0x00006A76 File Offset: 0x00004C76
		public NullTalisman(Ability baseAbility) : base(baseAbility)
		{
			this.amplifierData = new SpecialData(baseAbility, "bonus_spell_amp");
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00006AB0 File Offset: 0x00004CB0
		public DamageType AmplifierDamageType { get; } = 2;

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00006AB8 File Offset: 0x00004CB8
		public string AmplifierModifierName { get; } = "modifier_item_null_talisman";

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x00006AC0 File Offset: 0x00004CC0
		public AmplifiesDamage AmplifiesDamage { get; } = 2;

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00006AC8 File Offset: 0x00004CC8
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00006AD0 File Offset: 0x00004CD0
		public bool IsAmplifierPermanent { get; } = 1;

		// Token: 0x060006FC RID: 1788 RVA: 0x00006AD8 File Offset: 0x00004CD8
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			if (!this.IsUsable)
			{
				return 0f;
			}
			return this.amplifierData.GetValue(this.Level) / 100f;
		}

		// Token: 0x0400031B RID: 795
		private readonly SpecialData amplifierData;
	}
}
