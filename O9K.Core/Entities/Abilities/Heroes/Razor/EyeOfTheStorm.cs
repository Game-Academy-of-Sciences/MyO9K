using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Razor
{
	// Token: 0x020002EB RID: 747
	[AbilityId(AbilityId.razor_eye_of_the_storm)]
	public class EyeOfTheStorm : AreaOfEffectAbility
	{
		// Token: 0x06000D02 RID: 3330 RVA: 0x00006612 File Offset: 0x00004812
		public EyeOfTheStorm(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}
	}
}
