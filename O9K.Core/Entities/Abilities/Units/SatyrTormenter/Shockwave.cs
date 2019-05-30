using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Units.SatyrTormenter
{
	// Token: 0x020000DE RID: 222
	[AbilityId(AbilityId.satyr_hellcaller_shockwave)]
	public class Shockwave : ConeAbility, INuke, IActiveAbility
	{
		// Token: 0x06000666 RID: 1638 RVA: 0x00021848 File Offset: 0x0001FA48
		public Shockwave(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.RadiusData = new SpecialData(baseAbility, "radius_start");
			this.EndRadiusData = new SpecialData(baseAbility, "radius_end");
			this.RangeData = new SpecialData(baseAbility, "distance");
		}
	}
}
