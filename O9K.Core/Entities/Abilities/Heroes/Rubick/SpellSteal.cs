using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Rubick
{
	// Token: 0x020002DF RID: 735
	[AbilityId(AbilityId.rubick_spell_steal)]
	public class SpellSteal : RangedAbility
	{
		// Token: 0x06000CD2 RID: 3282 RVA: 0x00007F18 File Offset: 0x00006118
		public SpellSteal(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
		}
	}
}
