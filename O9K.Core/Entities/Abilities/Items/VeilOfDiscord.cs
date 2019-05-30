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
	// Token: 0x020001A1 RID: 417
	[AbilityId(AbilityId.item_veil_of_discord)]
	public class VeilOfDiscord : CircleAbility, IDebuff, IHasDamageAmplify, IActiveAbility
	{
		// Token: 0x0600087A RID: 2170 RVA: 0x00022224 File Offset: 0x00020424
		public VeilOfDiscord(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "debuff_radius");
			this.amplifierData = new SpecialData(baseAbility, "resist_debuff");
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x00007D09 File Offset: 0x00005F09
		public DamageType AmplifierDamageType { get; } = 2;

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x00007D11 File Offset: 0x00005F11
		public string AmplifierModifierName { get; } = "modifier_item_veil_of_discord_debuff";

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x00007D19 File Offset: 0x00005F19
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x00007D21 File Offset: 0x00005F21
		public string DebuffModifierName { get; } = "modifier_item_veil_of_discord_debuff";

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x00007D29 File Offset: 0x00005F29
		public bool IsAmplifierAddedToStats { get; } = 1;

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x00007D31 File Offset: 0x00005F31
		public bool IsAmplifierPermanent { get; }

		// Token: 0x06000881 RID: 2177 RVA: 0x00007D39 File Offset: 0x00005F39
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return this.amplifierData.GetValue(this.Level) / -100f;
		}

		// Token: 0x0400041F RID: 1055
		private readonly SpecialData amplifierData;
	}
}
