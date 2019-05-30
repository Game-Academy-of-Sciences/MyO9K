using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Debuffs.Unique
{
	// Token: 0x020001BC RID: 444
	[AbilityId(AbilityId.item_ethereal_blade)]
	internal class EtherealBladeDebuff : OldDebuffAbility
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x00006786 File Offset: 0x00004986
		public EtherealBladeDebuff(IDebuff ability) : base(ability)
		{
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00027E70 File Offset: 0x00026070
		public override bool Use(Unit9 target)
		{
			if (!base.Ability.UseAbility(target, false, false))
			{
				return false;
			}
			base.OrbwalkSleeper.Sleep(base.Ability.Owner.Handle, base.Ability.GetHitTime(target));
			base.AbilitySleeper.Sleep(base.Ability.Handle, base.Ability.GetHitTime(target) + 0.5f);
			return true;
		}
	}
}
