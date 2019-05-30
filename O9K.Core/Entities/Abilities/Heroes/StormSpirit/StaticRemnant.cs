using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.StormSpirit
{
	// Token: 0x020002BB RID: 699
	[AbilityId(AbilityId.storm_spirit_static_remnant)]
	public class StaticRemnant : AreaOfEffectAbility, INuke, IActiveAbility
	{
		// Token: 0x06000C63 RID: 3171 RVA: 0x0000B259 File Offset: 0x00009459
		public StaticRemnant(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "static_remnant_radius");
			this.ActivationDelayData = new SpecialData(baseAbility, "static_remnant_delay");
			this.DamageData = new SpecialData(baseAbility, "static_remnant_damage");
		}
	}
}
