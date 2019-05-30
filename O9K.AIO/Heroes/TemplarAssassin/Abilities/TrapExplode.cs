using System;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.TemplarAssassin;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.TemplarAssassin.Abilities
{
	// Token: 0x02000092 RID: 146
	internal class TrapExplode : DebuffAbility
	{
		// Token: 0x060002DE RID: 734 RVA: 0x00003C97 File Offset: 0x00001E97
		public TrapExplode(ActiveAbility ability) : base(ability)
		{
			this.trap = (Trap)ability;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00011C48 File Offset: 0x0000FE48
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			Modifier modifier = target.GetModifier(base.Debuff.DebuffModifierName);
			return (modifier == null || modifier.RemainingTime <= 2.5f) && !target.IsDarkPactProtected && !target.IsInvulnerable && !target.IsRooted && !target.IsStunned && !target.IsHexed && !targetManager.TargetSleeper.IsSleeping && (this.trap.IsFullyCharged || (base.Owner.Distance(target) > base.Ability.Radius * 0.6f && target.GetAngle(base.Owner.Position, false) > 2f));
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00003CAC File Offset: 0x00001EAC
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.UseAbility(targetManager, comboSleeper, aoe))
			{
				return false;
			}
			targetManager.TargetSleeper.Sleep(0.3f);
			return true;
		}

		// Token: 0x04000195 RID: 405
		private readonly Trap trap;
	}
}
