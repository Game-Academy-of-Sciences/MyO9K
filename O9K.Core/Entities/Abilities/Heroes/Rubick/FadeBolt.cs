using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Rubick
{
	// Token: 0x020002E2 RID: 738
	[AbilityId(AbilityId.rubick_fade_bolt)]
	public class FadeBolt : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000CDD RID: 3293 RVA: 0x0000A97B File Offset: 0x00008B7B
		public FadeBolt(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
		}
	}
}
