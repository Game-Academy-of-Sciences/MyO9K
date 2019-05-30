using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Heroes.Juggernaut.Abililities
{
	// Token: 0x02000166 RID: 358
	internal class HealingWard : UsableAbility
	{
		// Token: 0x06000765 RID: 1893 RVA: 0x00003F23 File Offset: 0x00002123
		public HealingWard(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00003880 File Offset: 0x00001A80
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			return false;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00005C4C File Offset: 0x00003E4C
		public override bool ShouldCast(TargetManager targetManager)
		{
			return base.Owner.HealthPercentage < 30f;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00022834 File Offset: 0x00020A34
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Vector3 position = base.Owner.Position;
			if (!base.Ability.UseAbility(position, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(position);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
