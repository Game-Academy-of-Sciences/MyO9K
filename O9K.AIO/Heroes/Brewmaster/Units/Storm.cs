using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Brewmaster.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Brewmaster.Units
{
	// Token: 0x020001D8 RID: 472
	[UnitName("npc_dota_brewmaster_storm_1")]
	[UnitName("npc_dota_brewmaster_storm_2")]
	[UnitName("npc_dota_brewmaster_storm_3")]
	internal class Storm : ControllableUnit
	{
		// Token: 0x06000973 RID: 2419 RVA: 0x0002985C File Offset: 0x00027A5C
		public Storm(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu) : base(owner, abilitySleeper, orbwalkSleeper, menu)
		{
			base.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
			{
				{
					AbilityId.brewmaster_storm_cyclone,
					(ActiveAbility x) => this.cyclone = new Cyclone(x)
				},
				{
					AbilityId.brewmaster_storm_dispel_magic,
					(ActiveAbility x) => this.dispel = new Dispel(x)
				},
				{
					AbilityId.brewmaster_storm_wind_walk,
					(ActiveAbility x) => this.windWalk = new WindWalk(x)
				},
				{
					AbilityId.brewmaster_cinder_brew,
					(ActiveAbility x) => this.cender = new DebuffAbility(x)
				}
			};
			base.MoveComboAbilities.Add(AbilityId.brewmaster_storm_wind_walk, (ActiveAbility _) => this.windWalk);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x000298F8 File Offset: 0x00027AF8
		public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
		{
			AbilityHelper abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
			return abilityHelper.UseAbility(this.cender, true) || abilityHelper.UseAbility(this.windWalk, true) || abilityHelper.UseAbility(this.cyclone, true) || abilityHelper.UseAbility(this.dispel, true);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00029954 File Offset: 0x00027B54
		public bool CycloneTarget(TargetManager targetManager)
		{
			if (this.cyclone == null)
			{
				return false;
			}
			ActiveAbility ability = this.cyclone.Ability;
			return ability.CanBeCasted(true) && ability.CanHit(targetManager.Target) && ability.UseAbility(targetManager.Target, false, false);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00006BD9 File Offset: 0x00004DD9
		protected override bool MoveComboUseBuffs(AbilityHelper abilityHelper)
		{
			return base.MoveComboUseBuffs(abilityHelper) || abilityHelper.UseMoveAbility(this.windWalk);
		}

		// Token: 0x04000500 RID: 1280
		private DebuffAbility cender;

		// Token: 0x04000501 RID: 1281
		private Cyclone cyclone;

		// Token: 0x04000502 RID: 1282
		private AoeAbility dispel;

		// Token: 0x04000503 RID: 1283
		private WindWalk windWalk;
	}
}
