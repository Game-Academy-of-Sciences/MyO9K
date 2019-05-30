using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Heroes.Dynamic.Abilities.Specials;
using O9K.AIO.Modes.Combo;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Disables.Unique
{
	// Token: 0x020001C5 RID: 453
	[AbilityId(AbilityId.item_cyclone)]
	internal class EulsDisable : OldDisableAbility
	{
		// Token: 0x0600090D RID: 2317 RVA: 0x000068AD File Offset: 0x00004AAD
		public EulsDisable(IDisable ability) : base(ability)
		{
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00028820 File Offset: 0x00026A20
		public override bool CanBeCasted(Unit9 target, List<OldDisableAbility> disables, List<OldSpecialAbility> specials, ComboModeMenu menu)
		{
			return this.CanBeCasted(target, menu, true) && disables.Any((OldDisableAbility x) => !x.Disable.UnitTargetCast && x.Disable.Owner.Equals(this.Ability.Owner) && x.CanBeCasted(target, menu, true));
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000068F8 File Offset: 0x00004AF8
		public override bool ShouldCast(Unit9 target)
		{
			return (target.IsInNormalState || target.IsTeleporting || target.IsChanneling) && base.ShouldCast(target);
		}
	}
}
