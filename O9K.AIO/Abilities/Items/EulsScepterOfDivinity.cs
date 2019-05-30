using System;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Abilities.Items
{
	// Token: 0x0200020D RID: 525
	internal class EulsScepterOfDivinity : DisableAbility
	{
		// Token: 0x06000A6D RID: 2669 RVA: 0x00003482 File Offset: 0x00001682
		public EulsScepterOfDivinity(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002CDD0 File Offset: 0x0002AFD0
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			return (target.IsInNormalState || target.IsTeleporting || target.IsChanneling) && !target.HasModifier(new string[]
			{
				"modifier_lina_laguna_blade",
				"modifier_lion_finger_of_death"
			}) && this.ShouldForceCast(targetManager);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0002CE28 File Offset: 0x0002B028
		public bool ShouldForceCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			return (!base.Ability.UnitTargetCast || target.IsVisible) && (!base.Disable.UnitTargetCast || !target.IsBlockingAbilities) && !target.IsDarkPactProtected && !target.IsInvulnerable;
		}
	}
}
