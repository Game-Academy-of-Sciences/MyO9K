using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Medusa
{
	// Token: 0x02000333 RID: 819
	[AbilityId(AbilityId.medusa_mystic_snake)]
	public class MysticSnake : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000E0F RID: 3599 RVA: 0x0000C722 File Offset: 0x0000A922
		public MysticSnake(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "snake_damage");
			this.SpeedData = new SpecialData(baseAbility, "initial_speed");
		}
	}
}
