using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Tusk
{
	// Token: 0x02000294 RID: 660
	[AbilityId(AbilityId.tusk_ice_shards)]
	public class IceShards : LineAbility, INuke, IActiveAbility
	{
		// Token: 0x06000BBC RID: 3004 RVA: 0x0000A93F File Offset: 0x00008B3F
		public IceShards(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "shard_width");
			this.SpeedData = new SpecialData(baseAbility, "shard_speed");
			this.DamageData = new SpecialData(baseAbility, "shard_damage");
		}
	}
}
