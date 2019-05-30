using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.ShadowShaman.Abilities
{
	// Token: 0x020000B0 RID: 176
	internal class Wards : AoeAbility
	{
		// Token: 0x06000387 RID: 903 RVA: 0x0000356A File Offset: 0x0000176A
		public Wards(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00014060 File Offset: 0x00012260
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			return target.IsVisible && (!target.IsInvulnerable || this.ChainStun(target, true)) && ((!target.IsRooted && !target.IsStunned && !target.IsHexed) || target.GetImmobilityDuration() > 0f);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000140BC File Offset: 0x000122BC
		protected override bool ChainStun(Unit9 target, bool invulnerability)
		{
			float num = invulnerability ? target.GetInvulnerabilityDuration() : target.GetImmobilityDuration();
			if (num <= 0f)
			{
				return false;
			}
			float num2 = base.Ability.GetHitTime(target) + 0.5f;
			if (target.IsInvulnerable)
			{
				num2 -= 0.1f;
			}
			return num2 > num;
		}
	}
}
