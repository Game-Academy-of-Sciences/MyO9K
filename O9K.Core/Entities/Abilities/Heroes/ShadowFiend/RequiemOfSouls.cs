using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.ShadowFiend
{
	// Token: 0x020002D6 RID: 726
	[AbilityId(AbilityId.nevermore_requiem)]
	public class RequiemOfSouls : AreaOfEffectAbility
	{
		// Token: 0x06000CB8 RID: 3256 RVA: 0x0000B6F4 File Offset: 0x000098F4
		public RequiemOfSouls(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "requiem_radius");
		}
	}
}
