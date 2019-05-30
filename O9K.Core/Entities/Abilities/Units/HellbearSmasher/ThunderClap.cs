using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Units.HellbearSmasher
{
	// Token: 0x020000F1 RID: 241
	[AbilityId(AbilityId.polar_furbolg_ursa_warrior_thunder_clap)]
	public class ThunderClap : AreaOfEffectAbility, INuke, IActiveAbility
	{
		// Token: 0x0600067F RID: 1663 RVA: 0x00006612 File Offset: 0x00004812
		public ThunderClap(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}
	}
}
