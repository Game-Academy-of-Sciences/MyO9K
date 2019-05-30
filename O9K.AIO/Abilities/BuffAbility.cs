using System;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Abilities
{
	// Token: 0x020001FD RID: 509
	internal class BuffAbility : UsableAbility
	{
		// Token: 0x06000A25 RID: 2597 RVA: 0x000071C2 File Offset: 0x000053C2
		public BuffAbility(ActiveAbility ability) : base(ability)
		{
			this.Buff = (IBuff)ability;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x000071D7 File Offset: 0x000053D7
		public IBuff Buff { get; }

		// Token: 0x06000A27 RID: 2599 RVA: 0x0002C114 File Offset: 0x0002A314
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			if (!base.Ability.UseAbility(targetManager.Owner, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Owner);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0002C17C File Offset: 0x0002A37C
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 owner = base.Owner;
			if (owner.IsInvulnerable)
			{
				return false;
			}
			if (owner.Equals(this.Buff.Owner))
			{
				if (!this.Buff.BuffsOwner)
				{
					return false;
				}
			}
			else if (!this.Buff.BuffsAlly)
			{
				return false;
			}
			ToggleAbility toggleAbility;
			return (!owner.IsMagicImmune || this.Buff.PiercesMagicImmunity(owner)) && !owner.HasModifier(this.Buff.BuffModifierName) && ((toggleAbility = (this.Buff as ToggleAbility)) == null || !toggleAbility.Enabled);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002C214 File Offset: 0x0002A414
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(targetManager.Owner, targetManager.AllyHeroes, 1, 0, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Owner);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x000071DF File Offset: 0x000053DF
		public bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, float distance)
		{
			return base.Owner.Distance(targetManager.Target) <= distance && this.UseAbility(targetManager, comboSleeper, true);
		}
	}
}
