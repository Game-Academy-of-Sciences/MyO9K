using System;
using System.Linq;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;

namespace O9K.AIO.Heroes.EmberSpirit.Abilities
{
	// Token: 0x0200017F RID: 383
	internal class ActivateFireRemnant : BlinkAbility
	{
		// Token: 0x060007DD RID: 2013 RVA: 0x00002F6B File Offset: 0x0000116B
		public ActivateFireRemnant(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00024008 File Offset: 0x00022208
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return EntityManager9.Units.Any((Unit9 x) => x.IsAlly(this.Owner) && x.Name == "npc_dota_ember_spirit_remnant" && x.Distance(targetManager.Target.GetPredictedPosition(this.Owner.Distance(x) / this.Ability.Speed)) < this.Ability.Radius);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00024040 File Offset: 0x00022240
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			return EntityManager9.Units.Any((Unit9 x) => x.IsAlly(this.Owner) && x.Name == "npc_dota_ember_spirit_remnant" && x.Distance(targetManager.Target) < 800f) && base.ForceUseAbility(targetManager, comboSleeper);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00005F9D File Offset: 0x0000419D
		public override bool ShouldCast(TargetManager targetManager)
		{
			return !base.Owner.IsInvulnerable;
		}
	}
}
