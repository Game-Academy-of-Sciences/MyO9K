using System;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;
using SharpDX;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Blinks
{
	// Token: 0x020001B4 RID: 436
	internal class OldBlinkAbility : OldUsableAbility
	{
		// Token: 0x060008B4 RID: 2228 RVA: 0x0000660B File Offset: 0x0000480B
		public OldBlinkAbility(IBlink ability) : base(ability)
		{
			this.Blink = ability;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0000661B File Offset: 0x0000481B
		public IBlink Blink { get; }

		// Token: 0x060008B6 RID: 2230 RVA: 0x00006623 File Offset: 0x00004823
		public override bool ShouldCast(Unit9 target)
		{
			return (!base.Ability.UnitTargetCast || target.IsVisible) && !target.HasModifier("modifier_pudge_meat_hook");
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x000273D0 File Offset: 0x000255D0
		public override bool Use(Unit9 target)
		{
			if (!base.Ability.UseAbility(target, false, false))
			{
				return false;
			}
			base.OrbwalkSleeper.Sleep(base.Ability.Owner.Handle, base.Ability.GetHitTime(target.Position));
			base.AbilitySleeper.Sleep(base.Ability.Handle, base.Ability.GetHitTime(target.Position) + 0.5f);
			return true;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0002744C File Offset: 0x0002564C
		public bool Use(Vector3 position)
		{
			if (!base.Ability.UseAbility(position, false, false))
			{
				return false;
			}
			base.OrbwalkSleeper.Sleep(base.Ability.Owner.Handle, base.Ability.GetHitTime(position) + 0.1f);
			base.AbilitySleeper.Sleep(base.Ability.Handle, base.Ability.GetHitTime(position) + 0.5f);
			return true;
		}
	}
}
