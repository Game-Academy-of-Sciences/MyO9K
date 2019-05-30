using System;
using Ensage;
using O9K.AIO.KillStealer.Abilities.Base;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.AIO.KillStealer.Abilities
{
	// Token: 0x02000036 RID: 54
	[AbilityId(AbilityId.skywrath_mage_mystic_flare)]
	internal class MysticFlare : KillStealAbility
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00002E17 File Offset: 0x00001017
		public MysticFlare(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00002E20 File Offset: 0x00001020
		public override bool ShouldCast(Unit9 target)
		{
			return target.GetImmobilityDuration() >= 1f;
		}
	}
}
