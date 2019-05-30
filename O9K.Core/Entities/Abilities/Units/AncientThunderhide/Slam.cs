using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Units.AncientThunderhide
{
	// Token: 0x020000FA RID: 250
	[AbilityId(AbilityId.big_thunder_lizard_slam)]
	public class Slam : AreaOfEffectAbility, IHarass, IActiveAbility
	{
		// Token: 0x0600068E RID: 1678 RVA: 0x00006612 File Offset: 0x00004812
		public Slam(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}
	}
}
