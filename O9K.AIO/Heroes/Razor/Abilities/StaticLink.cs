using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Razor.Abilities
{
	// Token: 0x020000C2 RID: 194
	internal class StaticLink : DebuffAbility
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x000034DD File Offset: 0x000016DD
		public StaticLink(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00015B70 File Offset: 0x00013D70
		public override bool ShouldCast(TargetManager targetManager)
		{
			if (!base.ShouldCast(targetManager))
			{
				return false;
			}
			Unit9 target = targetManager.Target;
			return target.Distance(base.Owner) <= 400f || target.GetImmobilityDuration() >= 2f;
		}
	}
}
