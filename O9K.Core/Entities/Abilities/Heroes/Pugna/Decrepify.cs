using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Pugna
{
	// Token: 0x020001ED RID: 493
	[AbilityId(AbilityId.pugna_decrepify)]
	public class Decrepify : RangedAbility, IDebuff, IDisable, IShield, IHasDamageAmplify, IActiveAbility
	{
		// Token: 0x060009AA RID: 2474 RVA: 0x00023638 File Offset: 0x00021838
		public Decrepify(Ability baseAbility) : base(baseAbility)
		{
			this.amplifierData = new SpecialData(baseAbility, "bonus_spell_damage_pct");
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00008BA0 File Offset: 0x00006DA0
		public UnitState AppliesUnitState { get; } = 6L;

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x00008BA8 File Offset: 0x00006DA8
		public DamageType AmplifierDamageType { get; } = 2;

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00008BB0 File Offset: 0x00006DB0
		public string AmplifierModifierName { get; } = "modifier_pugna_decrepify";

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x00008BB8 File Offset: 0x00006DB8
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00008BC0 File Offset: 0x00006DC0
		public bool IsAmplifierAddedToStats { get; } = 1;

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x00008BC8 File Offset: 0x00006DC8
		public bool IsAmplifierPermanent { get; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00008BD0 File Offset: 0x00006DD0
		public string ShieldModifierName { get; } = "modifier_pugna_decrepify";

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x00008BD8 File Offset: 0x00006DD8
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00008BE0 File Offset: 0x00006DE0
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x00008BE8 File Offset: 0x00006DE8
		public string DebuffModifierName { get; } = "modifier_pugna_decrepify";

		// Token: 0x060009B5 RID: 2485 RVA: 0x00008BF0 File Offset: 0x00006DF0
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return this.amplifierData.GetValue(this.Level) / -100f;
		}

		// Token: 0x040004DC RID: 1244
		private readonly SpecialData amplifierData;
	}
}
