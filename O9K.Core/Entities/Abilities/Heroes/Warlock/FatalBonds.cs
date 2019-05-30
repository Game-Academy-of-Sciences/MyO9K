using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Warlock
{
	// Token: 0x02000271 RID: 625
	[AbilityId(AbilityId.warlock_fatal_bonds)]
	public class FatalBonds : RangedAreaOfEffectAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000B53 RID: 2899 RVA: 0x0000A348 File Offset: 0x00008548
		public FatalBonds(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "search_aoe");
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0000A36D File Offset: 0x0000856D
		public string DebuffModifierName { get; } = "modifier_warlock_fatal_bonds";
	}
}
