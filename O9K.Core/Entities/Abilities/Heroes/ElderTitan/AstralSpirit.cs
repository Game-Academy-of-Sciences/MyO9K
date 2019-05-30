using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.ElderTitan
{
	// Token: 0x02000389 RID: 905
	[AbilityId(AbilityId.elder_titan_ancestral_spirit)]
	public class AstralSpirit : CircleAbility, INuke, IActiveAbility
	{
		// Token: 0x06000F7C RID: 3964 RVA: 0x0000DA6F File Offset: 0x0000BC6F
		public AstralSpirit(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "pass_damage");
		}
	}
}
