using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Units.AncientProwlerShaman
{
	// Token: 0x020000FD RID: 253
	[AbilityId(AbilityId.spawnlord_master_freeze)]
	public class Petrify : OrbAbility, IDisable, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000691 RID: 1681 RVA: 0x000066F1 File Offset: 0x000048F1
		public Petrify(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x0000670D File Offset: 0x0000490D
		public UnitState AppliesUnitState { get; } = 1L;

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00006715 File Offset: 0x00004915
		public override float CastRange
		{
			get
			{
				return base.CastRange + 100f;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00006723 File Offset: 0x00004923
		public string ImmobilityModifierName { get; } = "modifier_spawnlord_master_freeze_root";
	}
}
