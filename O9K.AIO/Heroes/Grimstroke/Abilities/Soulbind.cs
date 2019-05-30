using System;
using System.Linq;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Grimstroke.Abilities
{
	// Token: 0x0200016D RID: 365
	internal class Soulbind : DebuffAbility
	{
		// Token: 0x06000785 RID: 1925 RVA: 0x000034DD File Offset: 0x000016DD
		public Soulbind(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00022DF0 File Offset: 0x00020FF0
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return base.CanHit(targetManager, comboMenu) && (from x in targetManager.EnemyHeroes
			where !x.Equals(targetManager.Target) && x.Distance(targetManager.Target) < this.Ability.Radius
			select x).Any<Unit9>();
		}
	}
}
