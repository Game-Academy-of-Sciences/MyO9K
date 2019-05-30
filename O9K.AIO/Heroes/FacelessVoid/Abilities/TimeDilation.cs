using System;
using System.Linq;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.FacelessVoid.Abilities
{
	// Token: 0x02000175 RID: 373
	internal class TimeDilation : DebuffAbility
	{
		// Token: 0x060007AD RID: 1965 RVA: 0x000034DD File Offset: 0x000016DD
		public TimeDilation(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x000234B8 File Offset: 0x000216B8
		public override bool ShouldCast(TargetManager targetManager)
		{
			if (!base.ShouldCast(targetManager))
			{
				return false;
			}
			if (targetManager.Target.Abilities.Count((Ability9 x) => x.RemainingCooldown > 0f) >= 2)
			{
				return true;
			}
			return targetManager.EnemyHeroes.SelectMany((Unit9 x) => x.Abilities).Count((Ability9 x) => x.RemainingCooldown > 0f) >= 5;
		}
	}
}
