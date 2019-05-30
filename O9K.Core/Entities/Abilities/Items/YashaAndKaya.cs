using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000168 RID: 360
	[AbilityId(AbilityId.item_yasha_and_kaya)]
	public class YashaAndKaya : PassiveAbility, IHasDamageAmplify
	{
		// Token: 0x0600073C RID: 1852 RVA: 0x00006C45 File Offset: 0x00004E45
		public YashaAndKaya(Ability baseAbility) : base(baseAbility)
		{
			this.amplifierData = new SpecialData(baseAbility, "spell_amp");
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x00006C7F File Offset: 0x00004E7F
		public DamageType AmplifierDamageType { get; } = 2;

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x00006C87 File Offset: 0x00004E87
		public string AmplifierModifierName { get; } = "modifier_item_yasha_and_kaya";

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00006C8F File Offset: 0x00004E8F
		public AmplifiesDamage AmplifiesDamage { get; } = 2;

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x00006C97 File Offset: 0x00004E97
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x00006C9F File Offset: 0x00004E9F
		public bool IsAmplifierPermanent { get; } = 1;

		// Token: 0x06000742 RID: 1858 RVA: 0x00006CA7 File Offset: 0x00004EA7
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			if (!this.IsUsable)
			{
				return 0f;
			}
			return this.amplifierData.GetValue(this.Level) / 100f;
		}

		// Token: 0x04000332 RID: 818
		private readonly SpecialData amplifierData;
	}
}
