using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.OgreMagi
{
	// Token: 0x02000314 RID: 788
	[AbilityId(AbilityId.ogre_magi_ignite)]
	public class Ignite : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000DA2 RID: 3490 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
		public Ignite(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x0000C105 File Offset: 0x0000A305
		public string DebuffModifierName { get; } = "modifier_ogre_magi_ignite";
	}
}
