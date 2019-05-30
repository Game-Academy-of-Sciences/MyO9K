using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Pudge.Abilities
{
	// Token: 0x020000CF RID: 207
	internal class BlinkDaggerPudge : BlinkAbility
	{
		// Token: 0x0600043B RID: 1083 RVA: 0x00002F6B File Offset: 0x0000116B
		public BlinkDaggerPudge(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000437F File Offset: 0x0000257F
		public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			return usableAbilities.Find((UsableAbility x) => x.Ability.Id == AbilityId.pudge_dismember) != null;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x000043AB File Offset: 0x000025AB
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			return this.UseAbility(targetManager, comboSleeper, 100f, 25f);
		}
	}
}
