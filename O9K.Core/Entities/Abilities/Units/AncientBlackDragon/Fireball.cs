using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Units.AncientBlackDragon
{
	// Token: 0x02000100 RID: 256
	[AbilityId(AbilityId.black_dragon_fireball)]
	public class Fireball : CircleAbility, IHarass, IActiveAbility
	{
		// Token: 0x06000697 RID: 1687 RVA: 0x0000672B File Offset: 0x0000492B
		public Fireball(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}
	}
}
