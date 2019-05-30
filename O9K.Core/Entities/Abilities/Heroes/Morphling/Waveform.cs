using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Morphling
{
	// Token: 0x02000203 RID: 515
	[AbilityId(AbilityId.morphling_waveform)]
	public class Waveform : LineAbility, IBlink, INuke, IActiveAbility
	{
		// Token: 0x060009F3 RID: 2547 RVA: 0x00008F51 File Offset: 0x00007151
		public Waveform(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "width");
			this.SpeedData = new SpecialData(baseAbility, "speed");
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00008F7C File Offset: 0x0000717C
		public BlinkType BlinkType { get; }
	}
}
