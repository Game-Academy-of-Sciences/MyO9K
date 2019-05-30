using System;
using System.Collections.Generic;
using System.Linq;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Heroes;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;
using SharpDX;

namespace O9K.AIO.Heroes.Spectre.Abilities
{
	// Token: 0x020000A0 RID: 160
	internal class Reality : UsableAbility
	{
		// Token: 0x06000324 RID: 804 RVA: 0x00003F35 File Offset: 0x00002135
		public Reality(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00003F45 File Offset: 0x00002145
		// (set) Token: 0x06000326 RID: 806 RVA: 0x00003F4D File Offset: 0x0000214D
		public bool RealityUseOnFakeTarget { get; set; } = true;

		// Token: 0x06000327 RID: 807 RVA: 0x00003880 File Offset: 0x00001A80
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			return false;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00012BF8 File Offset: 0x00010DF8
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			float distance = base.Owner.Distance(target);
			List<Hero9> list = (from x in EntityManager9.Heroes
			where x.Name == this.Owner.Name && x.IsAlive && x.IsIllusion && !x.IsInvulnerable && x.IsAlly(this.Owner) && x.HasModifier("modifier_spectre_haunt")
			select x).ToList<Hero9>();
			if (list.Count == 0)
			{
				return false;
			}
			if (this.RealityUseOnFakeTarget)
			{
				this.RealityUseOnFakeTarget = false;
				Unit9 unit = (from x in targetManager.EnemyHeroes
				where !x.Equals(target)
				orderby x.Distance(target)
				select x).FirstOrDefault<Unit9>();
				if (unit != null)
				{
					this.realityTarget = unit.Position;
					return true;
				}
			}
			if (list.All((Hero9 x) => x.Distance(target) + 100f > distance))
			{
				return false;
			}
			this.realityTarget = target.Position;
			return true;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00012CD8 File Offset: 0x00010ED8
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(this.realityTarget, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay();
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.2f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x040001BB RID: 443
		private Vector3 realityTarget;
	}
}
