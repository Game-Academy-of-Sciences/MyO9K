using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Techies
{
	// Token: 0x020001D4 RID: 468
	[AbilityId(AbilityId.techies_stasis_trap)]
	public class StasisTrap : CircleAbility, IAppliesImmobility
	{
		// Token: 0x0600095B RID: 2395 RVA: 0x00008720 File Offset: 0x00006920
		public StasisTrap(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "activation_radius");
			this.ActivationDelayData = new SpecialData(baseAbility, "activation_time");
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x00008756 File Offset: 0x00006956
		public string ImmobilityModifierName { get; } = "modifier_techies_stasis_trap_stunned";
	}
}
