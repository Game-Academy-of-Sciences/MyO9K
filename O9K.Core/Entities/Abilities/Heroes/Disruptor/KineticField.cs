using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Disruptor
{
	// Token: 0x02000244 RID: 580
	[AbilityId(AbilityId.disruptor_kinetic_field)]
	public class KineticField : CircleAbility
	{
		// Token: 0x06000AB0 RID: 2736 RVA: 0x000099FD File Offset: 0x00007BFD
		public KineticField(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.ActivationDelayData = new SpecialData(baseAbility, "formation_time");
		}
	}
}
