using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Weaver
{
	// Token: 0x0200026E RID: 622
	[AbilityId(AbilityId.weaver_the_swarm)]
	public class TheSwarm : LineAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000B48 RID: 2888 RVA: 0x0000A283 File Offset: 0x00008483
		public TheSwarm(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "spawn_radius");
			this.SpeedData = new SpecialData(baseAbility, "speed");
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x0000A2B9 File Offset: 0x000084B9
		public override float Radius
		{
			get
			{
				return base.Radius + 50f;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0000A2C7 File Offset: 0x000084C7
		public string DebuffModifierName { get; } = "modifier_weaver_swarm_debuff";
	}
}
