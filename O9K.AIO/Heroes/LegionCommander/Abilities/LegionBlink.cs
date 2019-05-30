using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Heroes.LegionCommander.Abilities
{
	// Token: 0x02000126 RID: 294
	internal class LegionBlink : BlinkAbility
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x00002F6B File Offset: 0x0000116B
		public LegionBlink(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001D3F0 File Offset: 0x0001B5F0
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			Vector3 vector = Vector3Extensions.Extend2D(targetManager.Target.GetPredictedPosition(0.4f), base.Owner.Position, 100f);
			if (base.Owner.Distance(vector) > base.Ability.CastRange)
			{
				return false;
			}
			if (!base.Ability.UseAbility(vector, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay + 0.1f);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001D3F0 File Offset: 0x0001B5F0
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Vector3 vector = Vector3Extensions.Extend2D(targetManager.Target.GetPredictedPosition(0.4f), base.Owner.Position, 100f);
			if (base.Owner.Distance(vector) > base.Ability.CastRange)
			{
				return false;
			}
			if (!base.Ability.UseAbility(vector, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay + 0.1f);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
