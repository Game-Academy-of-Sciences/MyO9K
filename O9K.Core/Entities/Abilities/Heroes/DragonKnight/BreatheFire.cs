using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.DragonKnight
{
	// Token: 0x02000391 RID: 913
	[AbilityId(AbilityId.dragon_knight_breathe_fire)]
	public class BreatheFire : ConeAbility, INuke, IActiveAbility
	{
		// Token: 0x06000F8F RID: 3983 RVA: 0x000247A0 File Offset: 0x000229A0
		public BreatheFire(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "start_radius");
			this.EndRadiusData = new SpecialData(baseAbility, "end_radius");
			this.RangeData = new SpecialData(baseAbility, "range");
			this.SpeedData = new SpecialData(baseAbility, "speed");
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x0000DB77 File Offset: 0x0000BD77
		public override bool BreaksLinkens { get; }

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0000DB7F File Offset: 0x0000BD7F
		public override bool UnitTargetCast { get; }
	}
}
