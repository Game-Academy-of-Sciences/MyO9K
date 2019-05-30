using System;
using System.Collections.Generic;
using System.Linq;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Brewmaster.Abilities
{
	// Token: 0x020001DD RID: 477
	internal class Dispel : AoeAbility
	{
		// Token: 0x06000986 RID: 2438 RVA: 0x0000356A File Offset: 0x0000176A
		public Dispel(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00029AF4 File Offset: 0x00027CF4
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			if (target.IsMagicImmune && !base.Ability.PiercesMagicImmunity(target))
			{
				return false;
			}
			if (!target.HasModifier("modifier_brewmaster_storm_cyclone"))
			{
				return false;
			}
			List<Unit9> list = (from x in targetManager.AllyUnits
			where !x.Equals(this.Owner) && x.Name.Contains("npc_dota_brewmaster")
			select x).ToList<Unit9>();
			return list.Count != 0 && list.All((Unit9 x) => x.Distance(targetManager.Target) < 400f);
		}
	}
}
