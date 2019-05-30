using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Warlock
{
	// Token: 0x02000270 RID: 624
	[AbilityId(AbilityId.warlock_rain_of_chaos)]
	public class ChaoticOffering : CircleAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000B50 RID: 2896 RVA: 0x0000A30A File Offset: 0x0000850A
		public ChaoticOffering(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "aoe");
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0000A338 File Offset: 0x00008538
		public override float ActivationDelay { get; } = 0.5f;

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0000A340 File Offset: 0x00008540
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
