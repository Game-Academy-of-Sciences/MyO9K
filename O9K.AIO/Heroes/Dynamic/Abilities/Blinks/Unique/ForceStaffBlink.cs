using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Blinks.Unique
{
	// Token: 0x020001B8 RID: 440
	[AbilityId(AbilityId.item_force_staff)]
	[AbilityId(AbilityId.item_hurricane_pike)]
	internal class ForceStaffBlink : OldBlinkAbility
	{
		// Token: 0x060008D2 RID: 2258 RVA: 0x00006709 File Offset: 0x00004909
		public ForceStaffBlink(IBlink ability) : base(ability)
		{
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00006712 File Offset: 0x00004912
		public override bool ShouldCast(Unit9 target)
		{
			return target.Equals(base.Ability.Owner);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00027BF4 File Offset: 0x00025DF4
		public override bool Use(Unit9 target)
		{
			if (!base.Ability.UseAbility(target, false, false))
			{
				return false;
			}
			base.OrbwalkSleeper.Sleep(base.Ability.Owner.Handle, base.Ability.GetHitTime(target.Position));
			base.AbilitySleeper.Sleep(base.Ability.Handle, base.Ability.GetHitTime(target) + 0.5f);
			return true;
		}
	}
}
