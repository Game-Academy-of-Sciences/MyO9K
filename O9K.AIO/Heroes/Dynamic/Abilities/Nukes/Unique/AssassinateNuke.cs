using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Nukes.Unique
{
	// Token: 0x020001A8 RID: 424
	[AbilityId(AbilityId.sniper_assassinate)]
	internal class AssassinateNuke : OldNukeAbility
	{
		// Token: 0x06000892 RID: 2194 RVA: 0x000064AE File Offset: 0x000046AE
		public AssassinateNuke(INuke ability) : base(ability)
		{
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x000064B7 File Offset: 0x000046B7
		public override bool ShouldCast(Unit9 target)
		{
			return base.ShouldCast(target) && base.Owner.Distance(target) > 1000f;
		}
	}
}
