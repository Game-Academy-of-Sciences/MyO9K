using System;
using Ensage;
using O9K.AIO.Modes.Combo;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Nukes.Unique
{
	// Token: 0x020001AA RID: 426
	[AbilityId(AbilityId.nyx_assassin_mana_burn)]
	internal class ManaBurnNuke : OldNukeAbility
	{
		// Token: 0x06000896 RID: 2198 RVA: 0x000064AE File Offset: 0x000046AE
		public ManaBurnNuke(INuke ability) : base(ability)
		{
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x000064D7 File Offset: 0x000046D7
		public override bool CanBeCasted(Unit9 target, ComboModeMenu menu, bool canHitCheck = true)
		{
			return base.CanBeCasted(target, menu, canHitCheck) && target.Mana >= 100f;
		}
	}
}
