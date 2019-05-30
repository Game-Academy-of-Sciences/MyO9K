using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.VengefulSpirit
{
	// Token: 0x02000286 RID: 646
	[AbilityId(AbilityId.vengefulspirit_wave_of_terror)]
	public class WaveOfTerror : LineAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000B91 RID: 2961 RVA: 0x0000A6C8 File Offset: 0x000088C8
		public WaveOfTerror(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "wave_width");
			this.SpeedData = new SpecialData(baseAbility, "wave_speed");
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0000A6FE File Offset: 0x000088FE
		public string DebuffModifierName { get; } = "modifier_vengefulspirit_wave_of_terror";
	}
}
