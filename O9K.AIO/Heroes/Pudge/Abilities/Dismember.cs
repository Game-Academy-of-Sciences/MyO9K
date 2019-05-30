using System;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Pudge.Abilities
{
	// Token: 0x020000D1 RID: 209
	internal class Dismember : DisableAbility
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x00003482 File Offset: 0x00001682
		public Dismember(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00017370 File Offset: 0x00015570
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			Unit9 target = targetManager.Target;
			return (targetManager.Target.HasModifier("modifier_pudge_meat_hook") && target.Distance(base.Owner) < 500f) || base.CanHit(targetManager, comboMenu);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000173B4 File Offset: 0x000155B4
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			return (target.IsVisible && targetManager.Target.HasModifier("modifier_pudge_meat_hook") && target.Distance(base.Owner) < 500f) || base.ShouldCast(targetManager);
		}
	}
}
