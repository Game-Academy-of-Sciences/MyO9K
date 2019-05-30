using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.CrystalMaiden
{
	// Token: 0x0200039F RID: 927
	[AbilityId(AbilityId.crystal_maiden_frostbite)]
	public class Frostbite : RangedAbility, IDisable, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000FBD RID: 4029 RVA: 0x0000DE6D File Offset: 0x0000C06D
		public Frostbite(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x0000DE89 File Offset: 0x0000C089
		public UnitState AppliesUnitState { get; } = 1L;

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x0000DE91 File Offset: 0x0000C091
		public string ImmobilityModifierName { get; } = "modifier_crystal_maiden_frostbite";
	}
}
