using System;
using System.Linq;
using Ensage;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;

namespace O9K.AIO.Abilities.Items
{
	// Token: 0x02000211 RID: 529
	internal class Nullifier : DisableAbility
	{
		// Token: 0x06000A79 RID: 2681 RVA: 0x00003482 File Offset: 0x00001682
		public Nullifier(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0002D1F8 File Offset: 0x0002B3F8
		public override bool ShouldCast(TargetManager targetManager)
		{
			if (!base.ShouldCast(targetManager))
			{
				return false;
			}
			return !targetManager.Target.Abilities.Any((Ability9 x) => x.Id == AbilityId.item_aeon_disk && x.IsReady);
		}
	}
}
