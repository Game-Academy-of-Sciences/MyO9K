using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.DarkSeer
{
	// Token: 0x0200024D RID: 589
	[AbilityId(AbilityId.dark_seer_vacuum)]
	public class Vacuum : CircleAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000ACB RID: 2763 RVA: 0x00009BCB File Offset: 0x00007DCB
		public Vacuum(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x00009BFF File Offset: 0x00007DFF
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
