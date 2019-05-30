using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.QueenOfPain
{
	// Token: 0x020001E8 RID: 488
	[AbilityId(AbilityId.queenofpain_scream_of_pain)]
	public class ScreamOfPain : AreaOfEffectAbility, INuke, IActiveAbility
	{
		// Token: 0x060009A0 RID: 2464 RVA: 0x00008ACE File Offset: 0x00006CCE
		public ScreamOfPain(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "area_of_effect");
			this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
		}
	}
}
