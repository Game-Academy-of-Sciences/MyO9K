using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Collision;

namespace O9K.Core.Entities.Abilities.Heroes.Clockwerk
{
	// Token: 0x0200024E RID: 590
	[AbilityId(AbilityId.rattletrap_hookshot)]
	public class Hookshot : LineAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000ACD RID: 2765 RVA: 0x00024930 File Offset: 0x00022B30
		public Hookshot(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "stun_radius");
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00009C07 File Offset: 0x00007E07
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00009C0F File Offset: 0x00007E0F
		public override CollisionTypes CollisionTypes { get; } = 30;
	}
}
