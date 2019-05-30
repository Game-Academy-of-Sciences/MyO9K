using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Rubick
{
	// Token: 0x020002DD RID: 733
	[AbilityId(AbilityId.rubick_arcane_supremacy)]
	public class ArcaneSupremacy : PassiveAbility, IHasDamageAmplify
	{
		// Token: 0x06000CCA RID: 3274 RVA: 0x0000B800 File Offset: 0x00009A00
		public ArcaneSupremacy(Ability baseAbility) : base(baseAbility)
		{
			this.amplifierData = new SpecialData(baseAbility, "spell_amp");
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x0000B83A File Offset: 0x00009A3A
		public DamageType AmplifierDamageType { get; } = 2;

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0000B842 File Offset: 0x00009A42
		public string AmplifierModifierName { get; } = "modifier_rubick_arcane_supremacy";

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x0000B84A File Offset: 0x00009A4A
		public AmplifiesDamage AmplifiesDamage { get; } = 2;

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x0000B852 File Offset: 0x00009A52
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0000B85A File Offset: 0x00009A5A
		public bool IsAmplifierPermanent { get; } = 1;

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0000B862 File Offset: 0x00009A62
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return this.amplifierData.GetValue(this.Level) / 100f;
		}

		// Token: 0x0400069A RID: 1690
		private readonly SpecialData amplifierData;
	}
}
