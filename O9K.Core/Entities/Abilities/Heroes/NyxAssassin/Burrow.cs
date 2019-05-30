using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.NyxAssassin
{
	// Token: 0x02000316 RID: 790
	[AbilityId(AbilityId.nyx_assassin_burrow)]
	public class Burrow : ActiveAbility, IHasDamageAmplify
	{
		// Token: 0x06000DA5 RID: 3493 RVA: 0x0000C10D File Offset: 0x0000A30D
		public Burrow(Ability baseAbility) : base(baseAbility)
		{
			this.amplifierData = new SpecialData(baseAbility, "damage_reduction");
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x0000C140 File Offset: 0x0000A340
		public override bool TargetsEnemy { get; }

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x0000C148 File Offset: 0x0000A348
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x0000C150 File Offset: 0x0000A350
		public string AmplifierModifierName { get; } = "modifier_nyx_assassin_burrow";

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x0000C158 File Offset: 0x0000A358
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x0000C160 File Offset: 0x0000A360
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0000C168 File Offset: 0x0000A368
		public bool IsAmplifierPermanent { get; }

		// Token: 0x06000DAC RID: 3500 RVA: 0x0000C170 File Offset: 0x0000A370
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return this.amplifierData.GetValue(this.Level) / -100f;
		}

		// Token: 0x04000712 RID: 1810
		private readonly SpecialData amplifierData;
	}
}
