using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Tinker
{
	// Token: 0x020001CC RID: 460
	[AbilityId(AbilityId.tinker_laser)]
	public class Laser : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000941 RID: 2369 RVA: 0x00008562 File Offset: 0x00006762
		public Laser(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "laser_damage");
		}
	}
}
