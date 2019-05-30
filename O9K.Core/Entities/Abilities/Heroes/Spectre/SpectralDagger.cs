using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Spectre
{
	// Token: 0x020002C6 RID: 710
	[AbilityId(AbilityId.spectre_spectral_dagger)]
	public class SpectralDagger : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000C85 RID: 3205 RVA: 0x0000B468 File Offset: 0x00009668
		public SpectralDagger(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}
	}
}
