using System;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Pangolier.Abilities
{
	// Token: 0x020000E7 RID: 231
	internal class ShieldCrash : NukeAbility
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x000032F0 File Offset: 0x000014F0
		public ShieldCrash(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00018B5C File Offset: 0x00016D5C
		public override bool CanBeCasted(TargetManager targetManager, bool channelingCheck, IComboModeMenu comboMenu)
		{
			if (!base.CanBeCasted(targetManager, channelingCheck, comboMenu))
			{
				return false;
			}
			Unit9 target = targetManager.Target;
			return !base.Owner.HasModifier("modifier_pangolier_gyroshell") || !(base.Owner.GetAngle(target.Position, false) < 0.75f & base.Owner.Distance(target) < base.Ability.CastRange);
		}
	}
}
