using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Pudge.Abilities
{
	// Token: 0x020000D3 RID: 211
	internal class Rot : UsableAbility
	{
		// Token: 0x06000448 RID: 1096 RVA: 0x000043ED File Offset: 0x000025ED
		public Rot(ActiveAbility ability) : base(ability)
		{
			this.rot = (ToggleAbility)ability;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00004402 File Offset: 0x00002602
		public bool IsEnabled
		{
			get
			{
				return this.rot.Enabled;
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000440F File Offset: 0x0000260F
		public bool AutoToggle(TargetManager targetManager)
		{
			if (this.rot.Enabled)
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

		// Token: 0x0600044B RID: 1099 RVA: 0x00003880 File Offset: 0x00001A80
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			return false;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001757C File Offset: 0x0001577C
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			return this.rot.CanHit(target) && base.Ability.GetDamage(target) > 0 && !target.IsInvulnerable;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00004445 File Offset: 0x00002645
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			this.rot.Enabled = !this.rot.Enabled;
			base.Sleeper.Sleep(0.1f);
			return true;
		}

		// Token: 0x04000263 RID: 611
		private readonly ToggleAbility rot;
	}
}
