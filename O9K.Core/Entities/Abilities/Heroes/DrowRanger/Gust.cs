using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.DrowRanger
{
	// Token: 0x0200038E RID: 910
	[AbilityId(AbilityId.drow_ranger_wave_of_silence)]
	public class Gust : LineAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000F87 RID: 3975 RVA: 0x0000DAE5 File Offset: 0x0000BCE5
		public Gust(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "wave_width");
			this.SpeedData = new SpecialData(baseAbility, "wave_speed");
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x0000DB18 File Offset: 0x0000BD18
		public UnitState AppliesUnitState { get; } = 8L;
	}
}
