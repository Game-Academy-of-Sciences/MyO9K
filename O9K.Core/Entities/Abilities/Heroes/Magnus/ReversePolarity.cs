using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Magnus
{
	// Token: 0x0200020A RID: 522
	[AbilityId(AbilityId.magnataur_reverse_polarity)]
	public class ReversePolarity : AreaOfEffectAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000A07 RID: 2567 RVA: 0x000090CF File Offset: 0x000072CF
		public ReversePolarity(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "pull_radius");
			this.DamageData = new SpecialData(baseAbility, "polarity_damage");
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x00009103 File Offset: 0x00007303
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
