using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Heroes.Windranger.Abilities
{
	// Token: 0x0200003B RID: 59
	internal class BlinkDaggerWindranger : BlinkAbility
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00002F6B File Offset: 0x0000116B
		public BlinkDaggerWindranger(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000C7BC File Offset: 0x0000A9BC
		public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			Shackleshot shackleshot;
			if ((shackleshot = (usableAbilities.FirstOrDefault((UsableAbility x) => x.Ability.Id == AbilityId.windrunner_shackleshot) as Shackleshot)) == null)
			{
				return false;
			}
			this.blinkPosition = shackleshot.GetBlinkPosition(targetManager, base.Ability.CastRange);
			return !this.blinkPosition.IsZero;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00002F74 File Offset: 0x00001174
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(this.blinkPosition, false, false))
			{
				return false;
			}
			base.Sleeper.Sleep(base.Ability.GetCastDelay(targetManager.Target));
			return true;
		}

		// Token: 0x040000CA RID: 202
		private Vector3 blinkPosition;
	}
}
