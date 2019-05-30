using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Heroes.LegionCommander
{
	// Token: 0x02000354 RID: 852
	[AbilityId(AbilityId.legion_commander_duel)]
	public class Duel : RangedAbility, IDisable, IHasDamageAmplify, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000E6C RID: 3692 RVA: 0x0000CB2E File Offset: 0x0000AD2E
		public Duel(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x0000CB64 File Offset: 0x0000AD64
		public DamageType AmplifierDamageType { get; } = 7;

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x0000CB6C File Offset: 0x0000AD6C
		public string AmplifierModifierName { get; } = "modifier_legion_commander_duel";

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x0000CB74 File Offset: 0x0000AD74
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x0000CB7C File Offset: 0x0000AD7C
		public bool IsAmplifierAddedToStats { get; }

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0000CB84 File Offset: 0x0000AD84
		public bool IsAmplifierPermanent { get; }

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0000CB8C File Offset: 0x0000AD8C
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x0000CB94 File Offset: 0x0000AD94
		public string ImmobilityModifierName { get; } = "modifier_legion_commander_duel";

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x0000CB9C File Offset: 0x0000AD9C
		protected override float BaseCastRange
		{
			get
			{
				return base.BaseCastRange + 100f;
			}
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0000CBAA File Offset: 0x0000ADAA
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			if (!base.Owner.HasAghanimsScepter)
			{
				return 0f;
			}
			return -1f;
		}
	}
}
