using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Spectre
{
	// Token: 0x020002C3 RID: 707
	[AbilityId(AbilityId.spectre_dispersion)]
	public class Dispersion : PassiveAbility, IHasDamageAmplify
	{
		// Token: 0x06000C79 RID: 3193 RVA: 0x0000B3A6 File Offset: 0x000095A6
		public Dispersion(Ability baseAbility) : base(baseAbility)
		{
			this.amplifierData = new SpecialData(baseAbility, "damage_reflection_pct");
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x0000B3E0 File Offset: 0x000095E0
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x0000B3E8 File Offset: 0x000095E8
		public string AmplifierModifierName { get; } = "modifier_spectre_dispersion";

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x0000B3F0 File Offset: 0x000095F0
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x0000B3F8 File Offset: 0x000095F8
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0000B400 File Offset: 0x00009600
		public bool IsAmplifierPermanent { get; } = 1;

		// Token: 0x06000C7F RID: 3199 RVA: 0x0000B408 File Offset: 0x00009608
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return this.amplifierData.GetValue(this.Level) / -100f;
		}

		// Token: 0x0400066D RID: 1645
		private readonly SpecialData amplifierData;
	}
}
