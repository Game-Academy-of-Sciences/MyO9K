using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Jakiro
{
	// Token: 0x02000223 RID: 547
	[AbilityId(AbilityId.jakiro_liquid_fire)]
	public class LiquidFire : OrbAbility, IHarass, IActiveAbility
	{
		// Token: 0x06000A5D RID: 2653 RVA: 0x00009561 File Offset: 0x00007761
		public LiquidFire(Ability baseAbility) : base(baseAbility)
		{
		}
	}
}
