using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Collision;

namespace O9K.Core.Entities.Abilities.Heroes.Mirana
{
	// Token: 0x02000206 RID: 518
	[AbilityId(AbilityId.mirana_arrow)]
	public class SacredArrow : LineAbility, IDisable, IActiveAbility
	{
		// Token: 0x060009FB RID: 2555 RVA: 0x00008FF4 File Offset: 0x000071F4
		public SacredArrow(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "arrow_width");
			this.SpeedData = new SpecialData(baseAbility, "arrow_speed");
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x00009030 File Offset: 0x00007230
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x00009038 File Offset: 0x00007238
		public override CollisionTypes CollisionTypes { get; } = 20;

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x00009040 File Offset: 0x00007240
		public bool MultipleArrows
		{
			get
			{
				Ability abilityById = base.Owner.GetAbilityById(AbilityId.special_bonus_unique_mirana_2);
				return abilityById != null && abilityById.Level > 0u;
			}
		}
	}
}
