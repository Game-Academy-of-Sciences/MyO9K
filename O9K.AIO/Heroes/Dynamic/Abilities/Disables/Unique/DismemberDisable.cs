using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Disables.Unique
{
	// Token: 0x020001C4 RID: 452
	[AbilityId(AbilityId.pudge_dismember)]
	internal class DismemberDisable : OldDisableAbility
	{
		// Token: 0x0600090B RID: 2315 RVA: 0x000068AD File Offset: 0x00004AAD
		public DismemberDisable(IDisable ability) : base(ability)
		{
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00002E73 File Offset: 0x00001073
		protected override bool ChainStun(Unit9 target)
		{
			return true;
		}
	}
}
