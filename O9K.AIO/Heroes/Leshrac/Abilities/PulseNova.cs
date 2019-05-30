using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Leshrac.Abilities
{
	// Token: 0x02000121 RID: 289
	internal class PulseNova : UsableAbility
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x00005012 File Offset: 0x00003212
		public PulseNova(ActiveAbility ability) : base(ability)
		{
			this.pulseNova = (ToggleAbility)ability;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00005027 File Offset: 0x00003227
		public bool AutoToggle(TargetManager targetManager)
		{
			if (this.pulseNova.Enabled)
			{
				if (!this.ShouldCast(targetManager))
				{
					return this.UseAbility(null, null, false);
				}
			}
			else if (this.ShouldCast(targetManager))
			{
				return this.UseAbility(null, null, false);
			}
			return false;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00003880 File Offset: 0x00001A80
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			return false;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001CC7C File Offset: 0x0001AE7C
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			return this.pulseNova.CanHit(target) && base.Ability.GetDamage(target) > 0 && (!target.IsReflectingDamage || base.Owner.IsMagicImmune) && !target.IsInvulnerable;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000505D File Offset: 0x0000325D
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			this.pulseNova.Enabled = !this.pulseNova.Enabled;
			base.Sleeper.Sleep(base.Ability.GetCastDelay() + 0.5f);
			return true;
		}

		// Token: 0x04000331 RID: 817
		private readonly ToggleAbility pulseNova;
	}
}
