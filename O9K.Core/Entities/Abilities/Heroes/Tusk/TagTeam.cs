using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Tusk
{
	// Token: 0x02000293 RID: 659
	[AbilityId(AbilityId.tusk_tag_team)]
	public class TagTeam : AreaOfEffectAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000BBA RID: 3002 RVA: 0x0000A912 File Offset: 0x00008B12
		public TagTeam(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0000A937 File Offset: 0x00008B37
		public string DebuffModifierName { get; } = "modifier_tusk_tag_team";
	}
}
