using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Units.MudGolem
{
	// Token: 0x020000EB RID: 235
	[AbilityId(AbilityId.mud_golem_hurl_boulder)]
	public class HurlBoulder : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000678 RID: 1656 RVA: 0x000065D6 File Offset: 0x000047D6
		public HurlBoulder(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0000660A File Offset: 0x0000480A
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
