using System;
using System.Collections.Generic;
using System.Linq;
using O9K.AIO.Abilities.Items;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Brewmaster.Abilities
{
	// Token: 0x020001DB RID: 475
	internal class Cyclone : EulsScepterOfDivinity
	{
		// Token: 0x06000981 RID: 2433 RVA: 0x00005650 File Offset: 0x00003850
		public Cyclone(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00029A80 File Offset: 0x00027C80
		public override bool ShouldCast(TargetManager targetManager)
		{
			if (!base.ShouldCast(targetManager))
			{
				return false;
			}
			List<Unit9> list = (from x in targetManager.AllyUnits
			where !x.Equals(this.Owner) && x.Name.Contains("npc_dota_brewmaster")
			select x).ToList<Unit9>();
			return list.Count == 0 || list.Any((Unit9 x) => x.Distance(targetManager.Target) > 800f);
		}
	}
}
