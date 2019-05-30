using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Timbersaw
{
	// Token: 0x020002AB RID: 683
	[AbilityId(AbilityId.shredder_whirling_death)]
	public class WhirlingDeath : AreaOfEffectAbility, INuke, IActiveAbility
	{
		// Token: 0x06000C15 RID: 3093 RVA: 0x0000AD94 File Offset: 0x00008F94
		public WhirlingDeath(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "whirling_radius");
			this.DamageData = new SpecialData(baseAbility, "whirling_damage");
		}
	}
}
