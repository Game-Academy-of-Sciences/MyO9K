using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.TrollWarlord
{
	// Token: 0x02000299 RID: 665
	[AbilityId(AbilityId.troll_warlord_whirling_axes_melee)]
	public class WhirlingAxesMelee : AreaOfEffectAbility, INuke, IActiveAbility
	{
		// Token: 0x06000BCF RID: 3023 RVA: 0x0000AA2B File Offset: 0x00008C2B
		public WhirlingAxesMelee(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "max_range");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}
	}
}
