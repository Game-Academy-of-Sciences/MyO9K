using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.EarthSpirit
{
	// Token: 0x02000235 RID: 565
	[AbilityId(AbilityId.earth_spirit_geomagnetic_grip)]
	public class GeomagneticGrip : LineAbility
	{
		// Token: 0x06000A87 RID: 2695 RVA: 0x00009826 File Offset: 0x00007A26
		public GeomagneticGrip(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "rock_damage");
			this.SpeedData = new SpecialData(baseAbility, "speed");
		}
	}
}
