using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.NaturesProphet
{
	// Token: 0x020001F8 RID: 504
	[AbilityId(AbilityId.furion_force_of_nature)]
	public class NaturesCall : CircleAbility
	{
		// Token: 0x060009D5 RID: 2517 RVA: 0x00008DDA File Offset: 0x00006FDA
		public NaturesCall(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "area_of_effect");
		}
	}
}
