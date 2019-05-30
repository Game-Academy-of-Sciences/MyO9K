using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Dazzle
{
	// Token: 0x020001AB RID: 427
	[AbilityId(AbilityId.dazzle_poison_touch)]
	public class PoisonTouch : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x060008AF RID: 2223 RVA: 0x00007F18 File Offset: 0x00006118
		public PoisonTouch(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00007F32 File Offset: 0x00006132
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x00007F3A File Offset: 0x0000613A
		public string DebuffModifierName { get; set; }
	}
}
