using System;
using Ensage;
using O9K.AIO.KillStealer.Abilities.Base;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.AIO.KillStealer.Abilities
{
	// Token: 0x02000037 RID: 55
	[AbilityId(AbilityId.pangolier_swashbuckle)]
	internal class Swashbuckle : KillStealAbility
	{
		// Token: 0x06000140 RID: 320 RVA: 0x00002E17 File Offset: 0x00001017
		public Swashbuckle(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00002E32 File Offset: 0x00001032
		public override bool ShouldCast(Unit9 target)
		{
			return !base.Ability.Owner.HasModifier("modifier_pangolier_gyroshell");
		}
	}
}
