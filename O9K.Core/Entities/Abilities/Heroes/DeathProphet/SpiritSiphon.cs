using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.DeathProphet
{
	// Token: 0x02000247 RID: 583
	[AbilityId(AbilityId.death_prophet_spirit_siphon)]
	public class SpiritSiphon : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000AB5 RID: 2741 RVA: 0x00009A95 File Offset: 0x00007C95
		public SpiritSiphon(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00009AA9 File Offset: 0x00007CA9
		public string DebuffModifierName { get; } = "modifier_death_prophet_spirit_siphon_slow";
	}
}
