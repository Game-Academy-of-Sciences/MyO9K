using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Units.AncientProwlerShaman
{
	// Token: 0x020000FC RID: 252
	[AbilityId(AbilityId.spawnlord_master_stomp)]
	public class Desecrate : AreaOfEffectAbility, INuke, IActiveAbility
	{
		// Token: 0x06000690 RID: 1680 RVA: 0x00006555 File Offset: 0x00004755
		public Desecrate(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}
	}
}
