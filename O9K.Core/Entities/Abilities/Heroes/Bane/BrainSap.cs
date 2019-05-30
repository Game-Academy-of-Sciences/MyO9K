using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Bane
{
	// Token: 0x020001B0 RID: 432
	[AbilityId(AbilityId.bane_brain_sap)]
	public class BrainSap : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x060008CA RID: 2250 RVA: 0x00008009 File Offset: 0x00006209
		public BrainSap(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "brain_sap_damage");
		}
	}
}
