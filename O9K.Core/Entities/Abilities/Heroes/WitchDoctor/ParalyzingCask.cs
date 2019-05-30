using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.WitchDoctor
{
	// Token: 0x020001BF RID: 447
	[AbilityId(AbilityId.witch_doctor_paralyzing_cask)]
	public class ParalyzingCask : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x060008FE RID: 2302 RVA: 0x00008252 File Offset: 0x00006452
		public ParalyzingCask(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "speed");
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00008275 File Offset: 0x00006475
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
