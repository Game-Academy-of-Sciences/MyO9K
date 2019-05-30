using System;
using O9K.AIO.Abilities;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Riki.Abilities
{
	// Token: 0x020000BA RID: 186
	internal class SmokeScreen : DisableAbility
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x00003482 File Offset: 0x00001682
		public SmokeScreen(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00015488 File Offset: 0x00013688
		protected override bool ChainStun(Unit9 target, bool invulnerability)
		{
			float invulnerabilityDuration = target.GetInvulnerabilityDuration();
			if (invulnerabilityDuration > 0f)
			{
				float num = base.Ability.GetHitTime(target);
				if (target.IsInvulnerable)
				{
					num -= 0.1f;
				}
				return num > invulnerabilityDuration;
			}
			return true;
		}
	}
}
