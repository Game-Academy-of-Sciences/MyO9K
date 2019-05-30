using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Heroes.Dynamic.Abilities.Specials;
using O9K.AIO.Modes.Combo;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Disables.Unique
{
	// Token: 0x020001C2 RID: 450
	[AbilityId(AbilityId.visage_summon_familiars_stone_form)]
	internal class StoneFormDisable : OldDisableAbility
	{
		// Token: 0x06000905 RID: 2309 RVA: 0x000068AD File Offset: 0x00004AAD
		public StoneFormDisable(IDisable ability) : base(ability)
		{
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x000068B6 File Offset: 0x00004AB6
		public override bool CanBeCasted(Unit9 target, List<OldDisableAbility> disables, List<OldSpecialAbility> specials, ComboModeMenu menu)
		{
			return !StoneFormDisable.Sleeper.IsSleeping && base.CanBeCasted(target, disables, specials, menu);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x000068D1 File Offset: 0x00004AD1
		public override bool Use(Unit9 target)
		{
			if (base.Use(target))
			{
				StoneFormDisable.Sleeper.Sleep(1f);
			}
			return false;
		}

		// Token: 0x040004CE RID: 1230
		private static readonly Sleeper Sleeper = new Sleeper();
	}
}
