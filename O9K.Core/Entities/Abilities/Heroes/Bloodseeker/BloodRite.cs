using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Bloodseeker
{
	// Token: 0x020003C1 RID: 961
	[AbilityId(AbilityId.bloodseeker_blood_bath)]
	public class BloodRite : CircleAbility, IDisable, IActiveAbility
	{
		// Token: 0x06001010 RID: 4112 RVA: 0x000289C0 File Offset: 0x00026BC0
		public BloodRite(Ability baseAbility) : base(baseAbility)
		{
			this.ActivationDelayData = new SpecialData(baseAbility, "delay");
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x0000E240 File Offset: 0x0000C440
		public UnitState AppliesUnitState { get; } = 8L;
	}
}
