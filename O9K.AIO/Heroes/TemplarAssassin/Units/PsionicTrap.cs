using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.TemplarAssassin.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.TemplarAssassin.Units
{
	// Token: 0x0200008B RID: 139
	[UnitName("npc_dota_templar_assassin_psionic_trap")]
	internal class PsionicTrap : ControllableUnit
	{
		// Token: 0x060002BC RID: 700 RVA: 0x00003BD0 File Offset: 0x00001DD0
		public PsionicTrap(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.templar_assassin_self_trap,
					(ActiveAbility x) => this.trapExplode = new TrapExplode(x)
				}
			};
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00003BFF File Offset: 0x00001DFF
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			return !comboModeMenu.IsHarassCombo && new AbilityHelper(targetManager, comboModeMenu, this).UseAbility(this.trapExplode, true);
		}

		// Token: 0x0400017D RID: 381
		private TrapExplode trapExplode;
	}
}
