using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Brewmaster.Units
{
	// Token: 0x020001D7 RID: 471
	[UnitName("npc_dota_brewmaster_earth_1")]
	[UnitName("npc_dota_brewmaster_earth_2")]
	[UnitName("npc_dota_brewmaster_earth_3")]
	internal class Earth : ControllableUnit
	{
		// Token: 0x0600096C RID: 2412 RVA: 0x0002975C File Offset: 0x0002795C
		public Earth(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.brewmaster_earth_hurl_boulder,
					(ActiveAbility x) => this.boulder = new DisableAbility(x)
				},
				{
					AbilityId.brewmaster_thunder_clap,
					(ActiveAbility x) => this.clap = new NukeAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.brewmaster_earth_hurl_boulder, (ActiveAbility _) => this.boulder);
			base.MoveComboAbilities.Add(AbilityId.brewmaster_thunder_clap, (ActiveAbility _) => this.clap);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x000297E8 File Offset: 0x000279E8
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.boulder, true) || abilityHelper.UseAbility(this.clap, true);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00006B99 File Offset: 0x00004D99
		protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseDisables(abilityHelper) || abilityHelper.UseAbility(this.boulder, true) || abilityHelper.UseAbility(this.clap, true);
		}

		// Token: 0x040004FE RID: 1278
		private DisableAbility boulder;

		// Token: 0x040004FF RID: 1279
		private NukeAbility clap;
	}
}
