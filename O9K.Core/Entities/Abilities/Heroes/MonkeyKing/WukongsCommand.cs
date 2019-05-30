using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.MonkeyKing
{
	// Token: 0x0200032A RID: 810
	[AbilityId(AbilityId.monkey_king_wukongs_command)]
	public class WukongsCommand : CircleAbility
	{
		// Token: 0x06000DF1 RID: 3569 RVA: 0x0000C544 File Offset: 0x0000A744
		public WukongsCommand(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "second_radius");
			this.castRangeData = new SpecialData(baseAbility, "cast_range");
		}

		// Token: 0x0400073F RID: 1855
		private SpecialData castRangeData;
	}
}
