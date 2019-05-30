using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Puck
{
	// Token: 0x020001F0 RID: 496
	[AbilityId(AbilityId.puck_dream_coil)]
	public class DreamCoil : CircleAbility, IDisable, IActiveAbility
	{
		// Token: 0x060009BA RID: 2490 RVA: 0x00008C47 File Offset: 0x00006E47
		public DreamCoil(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "coil_radius");
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x00007F8A File Offset: 0x0000618A
		public override bool CanHitSpellImmuneEnemy
		{
			get
			{
				return base.Owner.HasAghanimsScepter;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x00008C6A File Offset: 0x00006E6A
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
