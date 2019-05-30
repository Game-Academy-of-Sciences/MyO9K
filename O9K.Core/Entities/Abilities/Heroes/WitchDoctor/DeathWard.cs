using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.WitchDoctor
{
	// Token: 0x020001BD RID: 445
	[AbilityId(AbilityId.witch_doctor_death_ward)]
	public class DeathWard : CircleAbility, IChanneled, IActiveAbility
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x000081BA File Offset: 0x000063BA
		public DeathWard(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "bounce_radius");
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.ChannelTime = baseAbility.GetChannelTime(0u);
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x000081F9 File Offset: 0x000063F9
		public float ChannelTime { get; }

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00008201 File Offset: 0x00006401
		public bool IsActivatesOnChannelStart { get; } = 1;
	}
}
